using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using GFSMasterService;
using System.Timers;
using System.IO;
using System.Data.OleDb;

namespace GFSMasterServer
{
    class MasterProgram
    {
        static GFSMasterServiceReference.GFSMasterClient gFSMasterClient;
        static GFSChunkServiceReference.GFSChunkServerClient gFSChunkServerClient;

        [STAThread]
        static void Main(string[] args)
        {
            ServiceHost host = new ServiceHost(typeof(GFSMaster));
            host.Open();
            Console.WriteLine("Master Server已经启动...");

            gFSMasterClient = new GFSMasterServiceReference.GFSMasterClient();
            gFSChunkServerClient = new GFSChunkServiceReference.GFSChunkServerClient();

            //加载备份文件
            LoadBackUp();

            //设置定时器定时备份文件
            Timer timer1 = new Timer();
            timer1.Elapsed += new ElapsedEventHandler(BackUp);
            timer1.Interval = 1000 * 30;
            timer1.AutoReset = true;
            timer1.Enabled = true;

            //设置定时器定时备份操作日志
            Timer timer3 = new Timer();
            timer3.Elapsed += new ElapsedEventHandler(BackUpLogs);
            timer3.Interval = 1000 * 30;
            timer3.AutoReset = true;
            timer3.Enabled = true;

            string command = string.Empty;

            while (true)
            {
                command = Console.ReadLine();

                if (command.Trim().Equals("exit"))
                {
                    host.Close();
                    break;
                }
            }
        }

        /// <summary>
        /// 定时备份文件
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="e"></param>
        public static void BackUp(Object obj, ElapsedEventArgs e)
        {
            FileStream fs = null;
            StreamWriter sw = null;

            string path = AppDomain.CurrentDomain.BaseDirectory;
            path += @"GFS\BackUp\";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            try
            {
                //获取所有文件的信息
                Dictionary<string, List<string>> fileTable = gFSMasterClient.GetFileTable();
                Dictionary<string, List<string>> fileInfo = gFSMasterClient.GetAllFileInfo();
                Dictionary<string, string> chunkTable = gFSMasterClient.GetChunkTable();

                fs = new FileStream(path + "fileTable_BackUp.txt", FileMode.OpenOrCreate, FileAccess.Write);
                sw = new StreamWriter(fs);

                //写入文件属性信息
                foreach (var pairs in fileInfo)
                {
                    //写入文件基本属性
                    string str = pairs.Key + " ";
                    foreach (var value in pairs.Value)
                    {
                        str += (value + " ");
                    }
                    sw.WriteLine(str.Trim());

                    //写入文件ChunkUuid
                    foreach (var chunkUuid in fileTable[pairs.Key])
                    {
                        sw.WriteLine(chunkTable[chunkUuid] + ";" + chunkUuid);
                    }
                    sw.WriteLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now + "：文件备份失败\n" + ex.Message);
            }
            finally
            {
                if (sw != null && fs != null)
                {
                    sw.Close();
                    fs.Close();
                }
            }
            
            Console.WriteLine(DateTime.Now + " : 文件备份成功");
        }

        /// <summary>
        /// 备份操作日志
        /// </summary>
        public static void BackUpLogs(Object obj, ElapsedEventArgs e)
        {
            //删除文件
            Delete();

            FileStream fs = null;
            StreamWriter sw = null;

            string path = AppDomain.CurrentDomain.BaseDirectory;
            path += @"GFS\Logs\";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            try
            {
                //获取操作日志
                List<string> logs = gFSMasterClient.GetLogs();
                if (logs.Count == 0)
                {
                    Console.WriteLine(DateTime.Now + " : 备份操作日志成功");
                    return;
                }

                fs = new FileStream(path + "logs.txt", FileMode.Append, FileAccess.Write);
                sw = new StreamWriter(fs);

                //写入文件属性信息
                foreach (var log in logs)
                {
                    sw.WriteLine(log);
                }
                Console.WriteLine(DateTime.Now + " : 备份操作日志成功");
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now + " : 备份操作日志失败\n" + ex.Message);
            }
            finally
            {
                if (sw != null && fs != null)
                {
                    sw.Close();
                    fs.Close();
                }
            }
        }

        /// <summary>
        /// 定时删除客户端已经删除的文件
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="e"></param>
        public static void Delete()
        {
            Dictionary<string, List<string>> deletedFile = gFSMasterClient.GetDeletedFile();
            if(deletedFile.Count == 0)
            {
                Console.WriteLine(DateTime.Now + " : 无文件要删除");
                return;
            }
            foreach(var pair in deletedFile)
            {
                foreach(var chunkUuid in pair.Value)
                {
                    string chunkServerId = gFSMasterClient.GetDeletedChunkServerId(chunkUuid);
                    gFSChunkServerClient.Delete(chunkServerId, chunkUuid);
                }
            }
            Console.WriteLine(DateTime.Now + " : 文件删除成功");
        }

        /// <summary>
        /// 启动时加载BackUp文件
        /// </summary>
        /// <returns></returns>
        public static bool LoadBackUp()
        {
            Dictionary<string, List<string>> fileTable = new Dictionary<string, List<string>>();
            Dictionary<string, List<string>> fileInfo = new Dictionary<string, List<string>>();
            Dictionary<string, string> chunkTable = new Dictionary<string, string>();

            FileStream fs = null;
            StreamReader sr = null;
            try
            {
                string path = AppDomain.CurrentDomain.BaseDirectory;
                path += @"GFS\BackUp\";
                if (!File.Exists(path + "fileTable_BackUp.txt"))
                {
                    Console.WriteLine("Master第一次启动， 无BackUp文件");
                    return true;
                }
                fs = new FileStream(path + "fileTable_BackUp.txt", FileMode.Open, FileAccess.Read);
                sr = new StreamReader(fs);
                string line = string.Empty;

                //读取BackUp文件
                while ((line = sr.ReadLine()) != null)
                {
                    //读取文件名和文件属性
                    string[] infos = line.Split(' ');
                    fileInfo[infos[0]] = new List<string>() { infos[1], infos[2], infos[3] + " " + infos[4] };

                    //读取chunkUuids
                    line = sr.ReadLine();
                    while (line != "")
                    {
                        if (!fileTable.ContainsKey(infos[0]))
                        {
                            fileTable[infos[0]] = new List<string>();
                        }
                        string[] temp = line.Split(';');
                        chunkTable[temp[1]] = temp[0]; 
                        fileTable[infos[0]].Add(temp[1]);
                        line = sr.ReadLine();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("MasterServer加载BackUp失败：" + e.Message);
                return false;
            }
            finally
            {
                if (sr != null && fs != null)
                {
                    sr.Close();
                    fs.Close();
                }
            }
            //将读取出的信息写入Master
            gFSMasterClient.AddBackUp(fileTable, fileInfo, chunkTable);
            Console.WriteLine("MasterServer加载BackUp成功");
            return true;
        }
    }
}

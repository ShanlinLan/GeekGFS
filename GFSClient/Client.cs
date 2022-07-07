using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace GFSClient
{
    class Client
    {
        private string localSystemPath;    // 本地文件路径

        public Client(string clientId)
        {
            //创建本地文件存放的路径，exe文件夹\GFS\clientId\
            localSystemPath = AppDomain.CurrentDomain.BaseDirectory;
            localSystemPath += @"\GFS\" + clientId + @"\";
            if (!Directory.Exists(localSystemPath))
            {
                Directory.CreateDirectory(localSystemPath);
            }
        }


        /// <summary>
        /// 向ChunkServer写入整个文件
        /// </summary>
        /// <param name="gFSMaster"></param>
        /// <param name="gFSChunkServerClient"></param>
        /// <param name="chunkUuids"></param>
        /// <param name="chunkDtaArray"></param>
        public void WriteChunks(
            GFSMasterServiceReference.GFSMasterClient gFSMaster,
            GFSChunkServiceReference.GFSChunkServerClient gFSChunkServerClient,
            string fileName, int numOfChunks, List<byte[]> chunkDtaArray)
        {
            for (int i = 0; i < numOfChunks; i++)
            {
                //获取每一个Chunk对应的ChunkUuid和ChunkServerId
                KeyValuePair<string, string> res = gFSMaster.Write_GetChunkUuidAndChunkServerId(fileName, i);
                try
                {
                    //将Chunk写入ChunkServer
                    gFSChunkServerClient.Write(res.Value, res.Key, chunkDtaArray[i]);
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
        }

        /// <summary>
        /// 将sourceFile写入GFS中
        /// </summary>
        /// <param name="gFSMaster"></param>
        /// <param name="gFSChunkServerClient"></param>
        /// <param name="chunkUuids"></param>
        /// <param name="chunkDtaArray"></param>
        public bool Write(
            GFSMasterServiceReference.GFSMasterClient gFSMaster,
            GFSChunkServiceReference.GFSChunkServerClient gFSChunkServerClient,
            string sourceFile, string destFile)
        {
            //判断要读取的文件是否存在
            if (!File.Exists(sourceFile))
            {
                Console.WriteLine("本地文件" + sourceFile + "不存在");
                return false;
            }
            try
            {
                //将文件划分为64*1024大小的chunk块
                List<byte[]> chunkDataList = new List<byte[]>();
                FileStream fs = new FileStream(sourceFile, FileMode.Open, FileAccess.Read);
                FileInfo fileInfo = new FileInfo(sourceFile);
                BinaryReader br = new BinaryReader(fs);
                while (br.BaseStream.Position < br.BaseStream.Length)
                {
                    chunkDataList.Add(br.ReadBytes(64 * 1024));
                }
                br.Close();
                fs.Close();

                //如果服务器中已经存在则删除
                if (gFSMaster.Exists(destFile))
                {
                    // delete(fileName);
                }

                //计算文件块数
                int numOfChunks = CalNumOfChunks((int)fileInfo.Length);

                //将文件写入ChunkServer
                WriteChunks(gFSMaster, gFSChunkServerClient, destFile, numOfChunks, chunkDataList);

                //写入文件信息
                List<string> fileInfos = new List<string>();
                fileInfos.Add(numOfChunks.ToString());
                fileInfos.Add(fileInfo.Length.ToString());
                gFSMaster.WriteFileInfo(destFile, fileInfos);
                Console.WriteLine("写文件成功");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 读取文件
        /// </summary>
        /// <param name="gFSMaster"></param>
        /// <param name="gFSChunkServerClient"></param>
        /// <param name="fileName"></param>
        public void Read(GFSMasterServiceReference.GFSMasterClient gFSMaster,
            GFSChunkServiceReference.GFSChunkServerClient gFSChunkServerClient, string fileName)
        {

            //判断文件是否在GFS中
            if (!Exists(gFSMaster, fileName))
            {
                Console.WriteLine(fileName + " not find!");
                return;
            }
            try
            {
                //判断文件夹是否存在
                if (!Directory.Exists(Path.GetDirectoryName(localSystemPath + fileName)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(localSystemPath + fileName));
                }

                //组装从Chunkserver中获取的Bytes
                FileStream fs = new FileStream(localSystemPath + fileName, FileMode.Create, FileAccess.Write);
                BinaryWriter bw = new BinaryWriter(fs);

                //获取文件的块数
                int numOfChunks = Convert.ToInt32(gFSMaster.GetNumOfChunks(fileName));
                for(int i = 0; i < numOfChunks; i++)
                {
                    //从master中获取每一块的chunkUuid和chunkServerId
                    KeyValuePair<string, string> pair = gFSMaster.Read_GetChunkUuidAndChunkServerId(fileName, i);
                    //从chunkserver中读取chunk块
                    byte[] chunk = gFSChunkServerClient.Read(pair.Value, pair.Key);
                    //在本地组装文件
                    bw.Write(chunk);
                }
                Console.WriteLine(fileName + " 存放在：" + Path.GetDirectoryName(localSystemPath + fileName));
                bw.Close();
                fs.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("读文件失败" + e.Message);
            }
        }

        /// <summary>
        /// 追加写入文件
        /// </summary>
        /// <param name="gFSMaster"></param>
        /// <param name="gFSChunkServerClient"></param>
        /// <param name="sourceFile"></param>
        /// <param name="destFile"></param>
        /// <returns></returns>
        public bool WriteAppend(GFSMasterServiceReference.GFSMasterClient gFSMaster,
            GFSChunkServiceReference.GFSChunkServerClient gFSChunkServerClient,
            string sourceFile, string destFile)
        {
            //如果GFS中不存在文件，直接写入
            if (!gFSMaster.Exists(destFile))
            {
                return Write(gFSMaster, gFSChunkServerClient, sourceFile, destFile);
            }
            //判断要读取的文件是否存在
            if (!File.Exists(sourceFile))
            {
                Console.WriteLine("本地文件" + sourceFile + "不存在");
                return false;
            }

            try
            {
                //将文件划分为64*1024大小的chunk块
                List<byte[]> chunkDataList = new List<byte[]>();
                FileStream fs = new FileStream(sourceFile, FileMode.Open, FileAccess.Read);
                FileInfo fileInfo = new FileInfo(sourceFile);
                BinaryReader br = new BinaryReader(fs);
                while (br.BaseStream.Position < br.BaseStream.Length)
                {
                    chunkDataList.Add(br.ReadBytes(64 * 1024));
                }
                br.Close();
                fs.Close();

                //计算文件块数
                int numOfChunks = CalNumOfChunks((int)fileInfo.Length);
                //获取原有文件的长度
                int numOfSourceFile = Convert.ToInt32(gFSMaster.GetNumOfChunks(destFile));


                //将文件写入ChunkServer
                for (int i = 0; i < numOfChunks; i++)
                {
                    //获取每一个Chunk对应的ChunkUuid和ChunkServerId
                    KeyValuePair<string, string> res = gFSMaster.Write_GetChunkUuidAndChunkServerId(destFile, numOfSourceFile + i);
                    try
                    {
                        //将Chunk写入ChunkServer
                        gFSChunkServerClient.Write(res.Value, res.Key, chunkDataList[i]);
                    }
                    catch (Exception e)
                    {
                        throw new Exception(e.Message);
                    }
                }
                //获取原来文件的长度
                string len = gFSMaster.GetLengthOfFile(destFile);

                //写入文件信息
                List<string> fileInfos = new List<string>();
                fileInfos.Add((numOfSourceFile + numOfChunks).ToString());
                fileInfos.Add((fileInfo.Length + Convert.ToInt32(len)).ToString());
                gFSMaster.WriteFileInfo(destFile, fileInfos);
                Console.WriteLine("追写文件成功");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            return true;
        }
        
        /// <summary>
        /// 获取文件列表
        /// </summary>
        /// <param name="gFSMaster"></param>
        /// <returns></returns>
        public Dictionary<string, List<string>> GetFileList(GFSMasterServiceReference.GFSMasterClient gFSMaster)
        {
            return gFSMaster.GetAllFileInfo();
        }

        /// <summary>
        /// 判断文件是否存在GFS中
        /// </summary>
        /// <param name="gFSMaster"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public bool Exists(GFSMasterServiceReference.GFSMasterClient gFSMaster, string fileName)
        {
            return gFSMaster.Exists(fileName);
        }

        /// <summary>
        /// 计算文件可以分为几块
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public int CalNumOfChunks(int size)
        {
            int chunkSize = 1024 * 64;
            int numOfChunks = (size / chunkSize);
            if (size % chunkSize != 0)
            {
                numOfChunks++;
            }
            return numOfChunks;
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="fileName"></param>
        public void Delete(GFSMasterServiceReference.GFSMasterClient gFSMaster, string fileName)
        {
            if (!Exists(gFSMaster, fileName))
            {
                Console.WriteLine("GFS中不存在 " + fileName);
                return;
            }
            gFSMaster.Delete(fileName);
            Console.WriteLine(fileName + "已删除");
        }
    }
}

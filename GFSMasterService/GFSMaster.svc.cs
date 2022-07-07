using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace GFSMasterService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“GFSMaster”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 GFSMaster.svc 或 GFSMaster.svc.cs，然后开始调试。
    public class GFSMaster : IGFSMaster
    {
        private static int numOfChunkServers = 0;
        private static int nowChunkServer = -1;
        //保存所有的chunkserverId
        private static List<string> chunkServerIds = new List<string>();
        //操作日志
        private static List<string> logs = new List<string>();
        //fileName到chunkUuids的映射<fileName：chunkUuids>
        private static Dictionary<string, List<string>> fileTable = new Dictionary<string, List<string>>();
        //chunkUuid到chunkServerId的映射<chunkUuid：chunkServerId>
        private static Dictionary<string, string> chunkTable = new Dictionary<string, string>();
        //fileName到文件属性的映射
        private static Dictionary<string, List<string>> fileInfo = new Dictionary<string, List<string>>();
        //保存客户端已删除的文件,等待周期删除<fileName：chunkUuids>
        private static Dictionary<string, List<string>> deletedFile = new Dictionary<string, List<string>>();
        private static Dictionary<string, string> deletedChunks = new Dictionary<string, string>();

        public GFSMaster()
        {

        }

        /// <summary>
        /// 测试函数
        /// </summary>
        /// <returns></returns>
        public KeyValuePair<int, int> Get()
        {
            return new KeyValuePair<int, int>(numOfChunkServers, nowChunkServer);
        }

        /// <summary>
        /// 获取chunkTable
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> GetChunkTable()
        {
            return chunkTable;
        }

        /// <summary>
        /// 写入一块chunk
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="chunkIndex"></param>
        /// <returns></returns>
        public KeyValuePair<string, string> Write_GetChunkUuidAndChunkServerId(string fileName, int chunkIndex)
        {
            //为chunk分配唯一标识符并加入fileTable
            string chunkUuid = Guid.NewGuid().ToString();
            if (chunkIndex == 0)
            {
                fileTable[fileName] = new List<string>();
            }
            //在fileTable中加入chunkUuid
            fileTable[fileName].Add(chunkUuid);

            //为chunk分配一个chunkserver
            nowChunkServer = (nowChunkServer + 1) % numOfChunkServers;
            string chunkServerId = chunkServerIds[nowChunkServer];
            chunkTable[chunkUuid] = chunkServerId;

            //记录操作日志
            string log = DateTime.Now + " write " + fileName + " " + chunkIndex + " " + chunkServerId + " " + chunkUuid;
            logs.Add(log);
            return new KeyValuePair<string, string>(chunkUuid, chunkServerId);
        }

        /// <summary>
        /// 写入文件属性
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="_fileInfo"></param>
        public void WriteFileInfo(string fileName, List<string> _fileInfo)
        {
            _fileInfo.Add(DateTime.Now.ToString());
            fileInfo[fileName] = _fileInfo;
        }

        /// <summary>
        /// 获取chunkUuid和chunkserverId
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="chunkIndex"></param>
        /// <returns></returns>
        public KeyValuePair<string, string> Read_GetChunkUuidAndChunkServerId(string fileName, int chunkIndex)
        {
            string chunkUuid = fileTable[fileName][chunkIndex];
            return new KeyValuePair<string, string>(chunkUuid, chunkTable[chunkUuid]);
        }

        /// <summary>
        /// 获取文件的块数
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public string GetNumOfChunks(string fileName)
        {
            return fileTable[fileName].Count.ToString();
        }

        /// <summary>
        /// 获取文件的长度
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public string GetLengthOfFile(string fileName)
        {
            return fileInfo[fileName][1].ToString();
        }

        /// <summary>
        /// 获取操作日志
        /// </summary>
        /// <returns></returns>
        public List<string> GetLogs()
        {
            List<string> res = new List<string>(logs);
            logs.Clear();
            return res;
        }

        /// <summary>
        /// 获取所有文件的信息
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, List<string>> GetAllFileInfo()
        {
            return fileInfo;
        }

        /// <summary>
        /// 添加一台chunkserver
        /// </summary>
        /// <param name="chunkServerId"></param>
        public void AddChunkServer(string chunkServerId)
        {
            numOfChunkServers++;
            if (nowChunkServer == -1) nowChunkServer++;
            chunkServerIds.Add(chunkServerId);
            string log = DateTime.Now + " " + "addchunkserver" + " " + chunkServerId;
            logs.Add(log);
        }

        /// <summary>
        /// 判断文件是否存在
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public bool Exists(string fileName)
        {
            return fileTable.ContainsKey(fileName);
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="fileName"></param>
        public void Delete(string fileName)
        {
            List<string> chunkUuids = fileTable[fileName];
            deletedFile[fileName] = chunkUuids;
            foreach(var chunkUuid in chunkUuids)
            {
                deletedChunks[chunkUuid] = chunkTable[chunkUuid];
                chunkTable.Remove(chunkUuid);
            }
            fileTable.Remove(fileName);
            fileInfo.Remove(fileName);
            string log = DateTime.Now + " " + "delete" + " " + fileName;
            logs.Add(log);
        }

        /// <summary>
        /// 获取所有文件的Chunk映射，备份使用
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, List<string>> GetFileTable()
        {
            return fileTable;
        }

        /// <summary>
        /// 获取Client已经删除的文件
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, List<string>> GetDeletedFile()
        {
            Dictionary<string, List<string>> res = new Dictionary<string, List<string>>(deletedFile);
            deletedFile.Clear();
            return res;
        }

        /// <summary>
        /// 获取chunk对应的chunkUuid
        /// </summary>
        /// <param name="chunkUuid"></param>
        /// <returns></returns>
        public string GetChunkServerId(string chunkUuid)
        {
            return chunkTable[chunkUuid];
        }

        public string GetDeletedChunkServerId(string chunkUuid)
        {
            string res = deletedChunks[chunkUuid];
            deletedChunks.Remove(chunkUuid);
            return res;
        }

        /// <summary>
        /// Master启动时加载备份文件
        /// </summary>
        /// <param name="_fileTable"></param>
        /// <param name="_fileInfo"></param>
        public void AddBackUp(Dictionary<string, List<string>> _fileTable, Dictionary<string, List<string>> _fileInfo, Dictionary<string, string> _chunkTable)
        {
            fileTable = _fileTable;
            fileInfo = _fileInfo;
            chunkTable = _chunkTable;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace GFSMasterService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IGFSMaster”。
    [ServiceContract]
    public interface IGFSMaster
    {
        [OperationContract]
        string GetDeletedChunkServerId(string chunkUuid);

        [OperationContract]
        KeyValuePair<int, int> Get();

        [OperationContract]
        List<string> GetLogs();

        [OperationContract]
        Dictionary<string, string> GetChunkTable();

        [OperationContract]
        KeyValuePair<string, string> Write_GetChunkUuidAndChunkServerId(string fileName, int chunkIndex);

        [OperationContract]
        void WriteFileInfo(string fileName, List<string> _fileInfo);

        [OperationContract]
        KeyValuePair<string, string> Read_GetChunkUuidAndChunkServerId(string fileName, int chunkIndex);

        [OperationContract]
        string GetNumOfChunks(string fileName);

        [OperationContract]
        Dictionary<string, List<string>> GetAllFileInfo();

        [OperationContract]
        void AddChunkServer(string chunkServerId);

        [OperationContract]
        bool Exists(string fileName);

        [OperationContract]
        void Delete(string fileName);

        [OperationContract]
        Dictionary<string, List<string>> GetFileTable();

        [OperationContract]
        Dictionary<string, List<string>> GetDeletedFile();

        [OperationContract]
        string GetChunkServerId(string chunkUuid);

        [OperationContract]
        string GetLengthOfFile(string fileName);

        [OperationContract]
        void AddBackUp(Dictionary<string, List<string>> _fileTable, Dictionary<string, List<string>> _fileInfo, Dictionary<string, string> _chunkTable);

    }
}

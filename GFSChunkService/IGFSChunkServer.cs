using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace GFSChunkService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IGFSChunkServer”。
    [ServiceContract]
    public interface IGFSChunkServer
    {
        [OperationContract]
        bool Write(string chunkServerId, string chunkUuid, byte[] chunkData);

        [OperationContract]
        byte[] Read(string chunkServerId, string chunkUuid);

        [OperationContract]
        bool Delete(string chunkServerId, string chunkUuid);
    }
}

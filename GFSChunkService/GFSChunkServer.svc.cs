using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace GFSChunkService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“GFSChunkServer”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 GFSChunkServer.svc 或 GFSChunkServer.svc.cs，然后开始调试。
    public class GFSChunkServer : IGFSChunkServer
    {
        public GFSChunkServer()
        {

        }

        /// <summary>
        /// 提供给Client的写入Chunk接口，一次写入一块
        /// </summary>
        /// <param name="chunkServerId"></param>
        /// <param name="chunkUuid"></param>
        /// <param name="chunkData"></param>
        /// <returns></returns>
        public bool Write(string chunkServerId, string chunkUuid, byte[] chunkData)
        {
            bool res = false;

            // 检查文件夹路径是否存在
            string path = AppDomain.CurrentDomain.BaseDirectory;
            path += @"GFS\DATA\" + chunkServerId + @"\";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            try
            {
                //在chunksever中写入chunk,文件路径为GFS\DATA\chunkServerId\,名称为chunkUuid.gfs
                FileStream fs = new FileStream(path + chunkUuid + ".gfs", FileMode.Create, FileAccess.Write);
                BinaryWriter bw = new BinaryWriter(fs);
                bw.Write(chunkData);
                bw.Close();
                fs.Close();
                res = true;
            }
            catch (Exception e)
            {
                res = false;
                throw new Exception("ChunkServer:" + e.Message); 
            }
            return res;
        }

        /// <summary>
        /// 根据chunkServerId读出一块文件给Client
        /// </summary>
        /// <param name="chunkServerId"></param>
        /// <param name="chunkUuid"></param>
        /// <returns></returns>
        public byte[] Read(string chunkServerId, string chunkUuid)
        {
            byte[] chunkData = new byte[64 * 1024];
            try
            {
                string path = AppDomain.CurrentDomain.BaseDirectory;
                path += @"GFS\DATA\" + chunkServerId + @"\" + chunkUuid + ".gfs";
                FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                chunkData = br.ReadBytes(64 * 1024);
                br.Close();
                fs.Close();
            }
            catch (Exception e)
            {
                throw new Exception("ChunkServer:" + e.Message);
            }
            return chunkData;
        }

        /// <summary>
        /// 删除指定chunkUuid的Chunk块
        /// </summary>
        /// <param name="chunkServerId"></param>
        /// <param name="chunkUuid"></param>
        /// <returns></returns>
        public bool Delete(string chunkServerId, string chunkUuid)
        {
            bool res = false;

            // 检查文件夹路径是否存在
            string path = AppDomain.CurrentDomain.BaseDirectory;
            path += @"GFS\DATA\" + chunkServerId + @"\";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            try
            {
                File.Delete(path + chunkUuid + ".gfs");
                res = true;
            }
            catch (Exception e)
            {
                res = false;
                throw new Exception("ChunkServer:" + e.Message);
            }
            return res;
        }

    }
}

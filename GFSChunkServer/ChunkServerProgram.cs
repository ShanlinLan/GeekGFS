using GFSChunkService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace GFSChunkServer
{
    class ChunkServerProgram
    {
        static GFSMasterServiceReference.GFSMasterClient gFSMasterClient;

        static void Main(string[] args)
        {
            ServiceHost host = new ServiceHost(typeof(GFSChunkService.GFSChunkServer));
            host.Open();
            Console.WriteLine("GFSChunkServer已启动...");
            gFSMasterClient = new GFSMasterServiceReference.GFSMasterClient();

            //模拟20个ChunkServers
            for (int i = 0; i < 20; i++)
            {
                gFSMasterClient.AddChunkServer(i.ToString());
                Console.WriteLine("GFSChunkServer:" + i + "已启动...");
            }
            string command = string.Empty;

            Console.WriteLine("exit：退出");
            while (true)
            {
                command = Console.ReadLine();
                if (command.Trim().ToLower() == "exit")
                {
                    host.Close();
                    break;
                }
                if (command == "t")
                {
                    KeyValuePair<int, int> p= gFSMasterClient.Get();
                    Console.WriteLine(p.Key + ":" + p.Value);
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFSClient
{
    class ClientProgram
    {
        static void Main(string[] args)
        {
            //创建MasterServer 和 ChunkServer的远程调用对象
            GFSChunkServiceReference.GFSChunkServerClient gFSChunkServerClient = new GFSChunkServiceReference.GFSChunkServerClient();
            GFSMasterServiceReference.GFSMasterClient gFSMasterClient = new GFSMasterServiceReference.GFSMasterClient();

            //实例化客户端，参数为客户端的Id
            Client gFSClient = new Client("1");
            string commands = string.Empty;
            string[] command = null;

            Console.WriteLine("Commands:");
            Console.WriteLine("To Get file list : list");
            Console.WriteLine("To UpLoad file : write sourceFilePath destFilePath");
            Console.WriteLine("To Append Write file : writeappend sourceFilePath destFilePath");
            Console.WriteLine("To DownLoad file : read destFilePath");
            Console.WriteLine("To Delete file : delete destFilePath");
            Console.WriteLine("To Exit Client : exit");

            //测试Client的函数
            while (true)
            {
                commands = Console.ReadLine();
                command = commands.Trim().Split(' ');

                if (command.Length == 3 && command[0].ToLower().Equals("write"))
                {
                    gFSClient.Write(gFSMasterClient, gFSChunkServerClient, command[1], command[2]);
                }
                else if (command.Length == 1 && command[0].ToLower().Equals("list"))
                {
                    foreach (var pairs in gFSClient.GetFileList(gFSMasterClient))
                    {
                        Console.Write(pairs.Key + "  ");
                        foreach (var value in pairs.Value)
                        {
                            Console.Write(value + "  ");
                        }
                        Console.WriteLine();
                    }
                    Console.WriteLine();
                }
                else if (command.Length == 3 && command[0].ToLower().Equals("writeappend"))
                {
                    gFSClient.WriteAppend(gFSMasterClient, gFSChunkServerClient, command[1], command[2]);
                }
                else if (command.Length == 2 && command[0].ToLower().Equals("read"))
                {
                    gFSClient.Read(gFSMasterClient, gFSChunkServerClient, command[1]);
                }
                else if (command.Length == 2 && command[0].ToLower().Equals("delete"))
                {
                    gFSClient.Delete(gFSMasterClient, command[1]);
                }
                else if (command.Length == 1 && command[0].ToLower().Equals("exit"))
                {
                    break;
                }
                else if (commands == "test")
                {
                    KeyValuePair<int, int> p = gFSMasterClient.Get();
                    Console.WriteLine(p.Key + ":" + p.Value);
                }
                else
                {
                    Console.WriteLine("Input Command again\n");
                }

            }
        }
    }
}

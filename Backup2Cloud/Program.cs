using Backup2Cloud.Args;
using Backup2Cloud.Conf;
using CommandLine;
using System;

namespace Backup2Cloud
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //防止docker下Console.WindowWidth为0，避免命令行解析时崩溃。
                if (Console.WindowWidth <= 10)
                {
                    Parser.Default.Settings.MaximumDisplayWidth = 80;
                }

                //解析命令行
                Parser.Default.ParseArguments<Options>(args)
                       .WithParsed(Loader.Load);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Environment.Exit(1);
            }
        }
    }
}

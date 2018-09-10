using Backup2Cloud.Args;
using Backup2Cloud.Conf;
using Backup2Cloud.Logging;
using CommandLine;
using System;

namespace Backup2Cloud
{
    class Program
    {
        static void Main(string[] args)
        {
#if !DEBUG
            try
            {
#endif
                //防止docker下Console.WindowWidth为0，避免命令行解析时崩溃。
                if (Console.WindowWidth <= 10)
                {
                    Parser.Default.Settings.MaximumDisplayWidth = 80;
                }

                //解析命令行
                Parser.Default.ParseArguments<ExampleOptions, NormalOptions>(args)
                    .WithParsed<ExampleOptions>(ExamplePrinter.Print)
                    .WithParsed<NormalOptions>(Loader.Load);
#if !DEBUG
            }
            catch (Exception e)
            {
                Log.Fatal(e.ToString());
                Environment.Exit(1);
            }
#endif
#if DEBUG
            Console.Read();
#endif
        }
    }
}

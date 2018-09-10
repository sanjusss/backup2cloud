using CommandLine;

namespace Backup2Cloud.Args
{
    [Verb("run", HelpText = "载入并运行配置。")]
    public class NormalOptions
    {
        [Option('c', "conf", Required = true, HelpText = "设置配置文件地址。")]
        public string Conf { get; set; }
    }
}

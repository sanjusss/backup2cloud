using CommandLine;

namespace Backup2Cloud.Args
{
    [Verb("eg", HelpText = "显示示例配置文件。")]
    public class ExampleOptions
    {
        [Option('s', "save", Required = false, Default = null,  HelpText = "示例配置文件保存地址。")]
        public string Path { get; set; }
    }
}

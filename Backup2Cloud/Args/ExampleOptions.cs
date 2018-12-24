using CommandLine;

namespace Backup2Cloud.Args
{
    [Verb("eg", HelpText = "显示示例配置文件。")]
    public class ExampleOptions
    {
        [Option('s', "save", Required = false, Default = null,  HelpText = "示例配置文件保存地址。")]
        public string Path { get; set; }

        [Option('l', "list", Required = false, Default = null, HelpText = "获取datasource或uploader的名称。可以为 datasource 或 uploader")]

        public string List { get; set; }

        [Option('d', "datasource", Required = false, Default = null, HelpText = "获取datasource的格式，需填写名称。")]
        public string DataSource { get; set; }


        [Option('u', "uploader", Required = false, Default = null, HelpText = "获取uploader的格式，需填写名称。")]
        public string Uploader { get; set; }
    }
}

using CommandLine;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backup2Cloud.Args
{
    public class Options
    {
        [Option('c', "conf", Required = true, HelpText = "设置配置文件地址。")]
        public string Conf { get; set; }
    }
}

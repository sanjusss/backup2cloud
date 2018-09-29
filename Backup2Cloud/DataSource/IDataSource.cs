using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backup2Cloud.DataSource
{
    public interface IDataSource
    {
        [JsonProperty("name")]
        string Name { get; set; }
    }
}

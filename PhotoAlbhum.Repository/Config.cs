using System;
using System.Collections.Specialized;
using System.Configuration;

namespace PhotoAlbhum.Repository
{
    public class Config : IConfig
    {
        public Config()
        {
            var settings = (NameValueCollection)ConfigurationManager.AppSettings;
            this.ApiUrl = settings["url"] ?? String.Empty;
        }
        public string ApiUrl { get; set; }        
    }
}

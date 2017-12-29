using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace SPOSiteCollectionReadyOnly
{
    public class SPOSettings
    {
        public string Url { get; set; }
        public string PolicyName { get; set; }
        public string CredentialTarget { get; set; }

        public static SPOSettings GetSettings(string jsonFile)
        {
            using (StreamReader sr = new StreamReader(jsonFile))
            {
                string json = sr.ReadToEnd();
                return JsonConvert.DeserializeObject<SPOSettings>(json);
            }
        }
    }
}

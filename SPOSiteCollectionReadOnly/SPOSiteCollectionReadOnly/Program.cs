using CredentialManagement;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.InformationPolicy;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace SPOSiteCollectionReadyOnly
{
    class Program
    {
        static void Main(string[] args)
        {
            string json = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "sposettings.json");
            SPOSettings setting = SPOSettings.GetSettings(json);
            using (ClientContext context = GetClientContext(setting))
            {
                Web web = context.Web;
                var sitePolicies = ProjectPolicy.GetProjectPolicies(context, web);
                context.Load(sitePolicies);
                context.ExecuteQuery();


                if (sitePolicies != null && sitePolicies.Count > 0)
                {
                    var policy = sitePolicies.FirstOrDefault(p => p.Name == setting.PolicyName);
                    if (policy != null)
                    {
                        ProjectPolicy.ApplyProjectPolicy(context, web, policy);
                        context.ExecuteQuery();

                        ProjectPolicy.CloseProject(context, web);
                        context.ExecuteQuery();
                    }
                }
            }

        }
        public static ClientContext GetClientContext(SPOSettings setting)
        {
            Credential cred = new Credential() { Target = setting.CredentialTarget };
            ClientContext ret = null;
            if (cred.Load())
            {
                SharePointOnlineCredentials spoCred = new SharePointOnlineCredentials(cred.Username, cred.SecurePassword);
                ret = new ClientContext(setting.Url)
                {
                    Credentials = spoCred
                };
            }
            return ret;
        }
    }
}

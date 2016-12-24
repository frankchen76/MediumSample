using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AppOnlySPOSample
{
    class Program
    {
        static void Main(string[] args)
        {
            Task result = TestSPOGet();
            result.Wait();
            Console.Write("Press enter key to exit...");
            Console.ReadLine();
        }
        private async static Task TestSPOGet()
        {
            //string tenantId = "yourtenant.onmicrosoft.com";
            //string clientId = "your client id";
            //string resourceId = "https://[your SPO tenant].sharepoint.com/"; //NOTE: you need '/' at the end. 
            //string resourceUrl = "https://[your SPO tenant].sharepoint.com/_api/web/Lists/getbytitle('[listname]')"; //this is your list name
            //string authority = String.Format("https://login.windows.net/{0}", tenantId);
            //string certficatePath = @"c:\test.pfx"; //this is your certficate location.
            //string certificatePassword = "xxxx"; // this is your certificate password

            //read Azure Ad setting from a file. please replace below with your own setting file. 
            string settingJson = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "setting.settingjson");
            AzureAdSetting setting = AzureAdSetting.CreateInstance(settingJson);

            //if you need to load from certficate store, use different constructors. 
            string certFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, setting.CertficatePath);
            X509Certificate2 certificate = new X509Certificate2(certFile, setting.CertificatePassword, X509KeyStorageFlags.MachineKeySet);
            AuthenticationContext authenticationContext = new AuthenticationContext(setting.Authority, false);

            ClientAssertionCertificate cac = new ClientAssertionCertificate(setting.ClientId, certificate);

            //get the access token to Outlook using the ClientAssertionCertificate
            var authenticationResult = await authenticationContext.AcquireTokenAsync(setting.ResourceId, cac);
            string token = authenticationResult.AccessToken;

            //initialize HttpClient for REST call
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            client.DefaultRequestHeaders.Add("Accept", "application/json;odata=verbose");

            //setup the client get
            HttpResponseMessage result = await client.GetAsync(setting.ResourceUrl);
            if (result.IsSuccessStatusCode)
            {
                string json = await result.Content.ReadAsStringAsync();
                Console.WriteLine("SharePoint ListItem Created! {0}", json);
            }
            else
            {
                Console.WriteLine("REST call failed. Error: {0}", await result.Content.ReadAsStringAsync());
            }

        }
    }
    public class AzureAdSetting
    {
        public string TenantId { get; set; }//"yourtenant.onmicrosoft.com";
        public string ClientId { get; set; }//"your client id";
        public string ResourceId { get; set; }//"https://outlook.office.com/";
        public string ResourceUrl { get; set; }//"https://outlook.office.com/api/v2.0/users/service@contoso.com/sendmail"; //this is your on-behalf user's UPN
        public string CertficatePath { get; set; }//@"c:\test.pfx"; //this is your certficate location.
        public string CertificatePassword { get; set; }//"xxxx"; // this is your certificate password
        public string SendEmail { get; set; } //the email you want to send to.

        public string Authority { get { return String.Format("https://login.windows.net/{0}", TenantId); } }

        public static AzureAdSetting CreateInstance(string jsonFile)
        {
            using (StreamReader sr = new StreamReader(jsonFile))
            {
                string json = sr.ReadToEnd();
                return JsonConvert.DeserializeObject<AzureAdSetting>(json);
            }
        }
    }
}

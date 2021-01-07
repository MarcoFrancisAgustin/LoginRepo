using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Login
{
    public class BusinessLogic : IBusinessLogic
    {
        public BusinessLogic()
        {

        }

        public void ProcessLogin(string userName, string password)
        {

            var baseAddress = new Uri("https://github.com/");
            var cookieContainer = new CookieContainer();
            using (var handler = new HttpClientHandler() { CookieContainer = cookieContainer })
            using (var client = new HttpClient(handler) { BaseAddress = baseAddress })
            {

                var homePageResult = client.GetAsync("/");
                homePageResult.Result.EnsureSuccessStatusCode();

                var content = new FormUrlEncodedContent(new[]
                {
                new KeyValuePair<string, string>("username", userName),
                new KeyValuePair<string, string>("password", password),
            });
                var loginResult = client.PostAsync("/login", content).Result;
                //loginResult.EnsureSuccessStatusCode();

            }
        }

        public void ProcessRecords()
        {
            startCrawlerAsync();
        }

        private static async Task startCrawlerAsync()
        {
            var url = "https://github.com/settings/security-log";
            var httpClient = new HttpClient();
            var html = await httpClient.GetStringAsync(url);
            var htmlDocument = new HtmlAgilityPack.HtmlDocument();
            htmlDocument.LoadHtml(html);

            var divs = htmlDocument.DocumentNode.Descendants("div")
                .Where(node => node.GetAttributeValue("class", "").Equals("TableObject-item TableObject-item--primary")).ToList();

            foreach (var div in divs)
            {
                var records = new Model.Records()
                {
                    Event = div.Descendants("audit-type").FirstOrDefault().InnerText,
                    Time = div.Descendants("relative-time").FirstOrDefault().InnerText
                };

            }

        }

    }
}


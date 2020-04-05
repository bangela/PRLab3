using ChatBot.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace ChatBot.Core.Services
{
    public class ChatBotService : IChatBotService
    {
        private Uri baseAddress;
        private List<Cookie> cookies;
        private IWebProxy proxy;
        public ChatBotService()
        {
            baseAddress = new Uri("http://demo.vhost.pandorabots.com/pandora/talk?botid=8c3111918e34ddce");
        }

        public async void Initialize(string proxyHost, int proxyPort, string proxyUsername = null, string proxyPassword = null)
        {
            proxy = new WebProxy
            {
                Address = new Uri($"http://{proxyHost}:{proxyPort}"),
                BypassProxyOnLocal = false,
                UseDefaultCredentials = true,
            };
            cookies = await GetCookies(baseAddress.ToString());
        }

        private async Task<List<Cookie>> GetCookies(string url)
        {
            var cookieContainer = new CookieContainer();
            var uri = new Uri(url);
            using (var httpClientHandler = new HttpClientHandler
            {
                CookieContainer = cookieContainer,
                Proxy = proxy
            })
            {
                using (var httpClient = new HttpClient(httpClientHandler))
                {
                    //await httpClient.GetAsync(uri);
                    //await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Options, uri));
                    await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Head, uri));
                    List<Cookie> cookies = cookieContainer.GetCookies(uri).Cast<Cookie>().ToList();
                    return cookies;
                }
            }
        }
        public async Task<string> Send(string msg)
        {
            
            var cookieContainer = new CookieContainer();
            string receive = null;
            using (var handler = new HttpClientHandler() { CookieContainer = cookieContainer, Proxy = proxy })
            using (var client = new HttpClient(handler) { BaseAddress = baseAddress })
            {
                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("input", msg),
                });
                foreach(var cookie in cookies)
                {
                    cookieContainer.Add(baseAddress, new Cookie(cookie.Name, cookie.Value));
                }
                var result = await client.PostAsync("", content);
                result.EnsureSuccessStatusCode();
                var html = await result.Content.ReadAsStringAsync().ConfigureAwait(false);
                receive = GetReceive(html);
            }
            Thread.Sleep(1000);
            return receive;
        }

        private string GetReceive(string html)
        {
            var regex = new Regex(@"(?<=(<font color=""green"">))(\w|\d|\n|[().,\-:;@#$%^&*\[\]""'+–\/\/®°⁰!?{}|`~]| )+?(?=(<\/font>))");
            var match = regex.Match(html);
            return match.Value;
        }
    }
}

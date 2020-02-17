using System;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;

namespace WebCrawler
{
    internal class WebCrawler
    {
        private static readonly Regex UrlTagPattern = new Regex(@"<a.*?href=[""'](?<url>.*?)[""'].*?>(?<name>.*?)</a>", RegexOptions.IgnoreCase);
        private static readonly Regex HrefPattern = new Regex("href\\s*=\\s*(?:\"(?<1>[^\"]*)\"|(?<1>\\S+))", RegexOptions.IgnoreCase);

        private static readonly Queue<Uri> Frontier = new Queue<Uri>();

        public int MaxLinksCount { get; private set; }
        public int GoodLinksCount { get; private set; }

        public Dictionary<string, bool> VisitedUrls { get; private set; }

        public WebCrawler(string[] initialUrls, int maxLinksCount)
        {
            VisitedUrls = new Dictionary<string, bool>();
            this.MaxLinksCount = maxLinksCount;
            this.GoodLinksCount = 0;

            foreach (var urlStr in initialUrls)
            {
                var ub = new UriBuilder(urlStr);
                Frontier.Enqueue(ub.Uri);
            }

            Crawl();
        }

        private void Crawl()
        {
            while (Frontier.Count > 0 && GoodLinksCount < MaxLinksCount)
            {
                var url = Frontier.Dequeue();
                if (!VisitedUrls.ContainsKey(url.ToString()))
                {
                    VisitPage(url);
                }
            }
        }

        private void VisitPage(Uri url)
        {
            try
            {
                VisitedUrls.Add(url.ToString(), true);
                GoodLinksCount++;

                var hostUrl = new UriBuilder(url.Host).Uri;

                var wc = new WebClient();
                var webPage = wc.DownloadString(url);

                var links = UrlTagPattern.Matches(webPage);
                foreach (Match href in links)
                {
                    var newUrl = HrefPattern.Match(href.Value).Groups[1].Value;
                    var absoluteUrl = NormalizeUrl(hostUrl, newUrl);
                    if (absoluteUrl != null && (absoluteUrl.Scheme == Uri.UriSchemeHttp || absoluteUrl.Scheme == Uri.UriSchemeHttps))
                    {
                        Frontier.Enqueue(absoluteUrl);
                    }
                }
            }
            catch
            {
                VisitedUrls[url.ToString()] = false;
                GoodLinksCount--;
            }

        }

        private Uri NormalizeUrl(Uri hostUrl, string url)
        {
            return Uri.TryCreate(hostUrl, url, out Uri absoluteUrl) ? absoluteUrl : null;
        }
    }
}

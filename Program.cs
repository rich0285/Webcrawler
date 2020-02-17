using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace Assignment_2
{
    class Program
    {
        static void Main(string[] args)
        {

          Webcrawler wb = new Webcrawler();  
          wb.Runner();

          
           


          /* Console.Write("Enter url: ");
            string urlStr = Console.ReadLine();

            UriBuilder ub = new UriBuilder(urlStr);
            WebClient wc = new WebClient();
            Dictionary<string, bool> myWeebOfLinks = new Dictionary<string, bool>();
            string webPage = wc.DownloadString(ub.Uri.ToString());
            var urlTagPattern = new Regex(@"<a.*?href=[""'](?<url>.*?)[""'].*?>(?<name>.*?)</a>",
                RegexOptions.IgnoreCase);
            var hrefPattern = new Regex("href\\s*=\\s*(?:\"(?<1>[^\"]*)\"|(?<1>\\S+))", RegexOptions.IgnoreCase);
            var urls = urlTagPattern.Matches(webPage);
            Queue<Uri> frontier = new Queue<Uri>();

            frontier.Enqueue(ub.Uri);


            while (frontier.Any())
            {

                foreach (Match url in urls)
                {

                    string newUrl = hrefPattern.Match(url.Value).Groups[1].Value;

                    bool isNewLink = true;

                    foreach (var key in myWeebOfLinks.Keys)
                    {
                        if (key.Equals(newUrl))
                        {
                            isNewLink = false;
                        }

                    }


                    if (isNewLink == true)
                    {
                        try
                        {

                            Uri.TryCreate(ub.Uri, newUrl, out var uri);

                            myWeebOfLinks.Add(newUrl, true);
                        }
                        catch (Exception e)
                        {
                            myWeebOfLinks.TryAdd(newUrl, false);
                            Console.WriteLine(e);
                            throw;

                        }

                        foreach (var link in myWeebOfLinks)
                        {

                            Console.WriteLine(link.Key);

                        }

                    }
                }
            }*/


        }


    }
}
    
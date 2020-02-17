using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace Assignment_2
{
    public class Webcrawler
    {
        //Uri builder
        static string urlStr;
        UriBuilder ub = new UriBuilder();
        WebClient wc = new WebClient();
        //List/dictionary
        Dictionary<string, bool> Links = new Dictionary<string, bool>();

        //linkNew
        bool _isNewLink = true;

        //Ignore case
        Regex urlTagPattern = new Regex(@"<a.*?href=[""'](?<url>.*?)[""'].*?>(?<name>.*?)</a>",
            RegexOptions.IgnoreCase);
        Regex hrefPattern = new Regex("href\\s*=\\s*(?:\"(?<1>[^\"]*)\"|(?<1>\\S+))", RegexOptions.IgnoreCase);
        //frontier 
        Queue<Uri> frontier = new Queue<Uri>();
        public void Runner()
        {

            Console.WriteLine("Enter Url");
            urlStr = Console.ReadLine();
            ub.Host = urlStr;
            string WebPage = wc.DownloadString(ub.Uri.ToString());
            FindAndCheckLinks(WebPage);
            while(frontier.Any())
            {
                string Link = wc.DownloadString(frontier.Dequeue());
                FindAndCheckLinks(Link);
            }

        }
        public void CheckPattern()
        {
        }
        public void FindAndCheckLinks(string webPage)
        {


            var urls = urlTagPattern.Matches(webPage);
            foreach (Match url in urls)
            {

                string newUrl = hrefPattern.Match(url.Value).Groups[1].Value;



                foreach (var key in Links.Keys)
                {
                    if (key.Equals(newUrl))
                    {
                        _isNewLink = false;
                    }

                }


                if (_isNewLink == true)
                {
                    try
                    {

                        Uri.TryCreate(ub.Uri, newUrl, out var uri);

                        Links.Add(uri.ToString(), true);
                        frontier.Enqueue(uri);
                    }
                    catch (Exception e)
                    {
                        Links.TryAdd(newUrl, false);
                        Console.WriteLine(e);

                    }

                    

                }
                
            }
            foreach (var link in Links)
            {
                if (link.Value)
                {
                    Console.WriteLine(link.Key.ToString());

                }

            }
        }
    }



}

using System;
using System.Runtime.CompilerServices;

namespace WebCrawler
{
    class Program
    {

        static void Main()
        {

            {

                {

                   
                    Console.Write("Enter an URL: ");
                    var urlStr = Console.ReadLine();

                    Console.Write("Number of links: ");
                    int maxLinksCount = Convert.ToInt32(Console.ReadLine());

                    var crawler = new WebCrawler(new[] {urlStr}, maxLinksCount);
                    

                    foreach (var (key, value) in crawler.VisitedUrls)
                    {

                        crawler.AntalLinks ++;
                        Console.WriteLine(crawler.AntalLinks + " {0,-10} {1}", value, key);
                       
                    }


                }
            }
        }
    }
}

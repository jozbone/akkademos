﻿namespace AsyncAwaitDemos
{
    using System;
    using System.Diagnostics;
    using System.Net;
    using System.Threading.Tasks;

    internal class MyDownloadString
    {
        private Stopwatch sw = new Stopwatch();

        public void DoRun()
        {
            const int LargeNumber = 6000000;
            sw.Start();
            int t1 = CountCharacters(1, "http://www.microsoft.com");
            int t2 = CountCharacters(2, "http://www.illustratedcsharp.com");
            CountToALargeNumber(1, LargeNumber);
            CountToALargeNumber(2, LargeNumber);
            CountToALargeNumber(3, LargeNumber);
            CountToALargeNumber(4, LargeNumber);
            Console.WriteLine("Chars in http://www.microsoft.com:{0}", t1);
            Console.WriteLine("Chars in http://www.illustratedcsharp.com:{0}", t2);
        }

        private int CountCharacters(int id, string uriString)
        {
            WebClient wc1 = new WebClient();
            Console.WriteLine("Starting call {0} : {1, 4:N0} ms", id, sw.Elapsed.TotalMilliseconds);
            string result = wc1.DownloadString(new Uri(uriString));
            Console.WriteLine(" Call {0} completed: {1, 4:N0} ms", id, sw.Elapsed.TotalMilliseconds);
            return result.Length;
        }

        private void CountToALargeNumber(int id, int value)
        {
            for (long i = 0; i < value; i++)
                ;

            Console.WriteLine(" End counting {0} : {1, 4:N0} ms", id, sw.Elapsed.TotalMilliseconds);
        }
    }

    internal class MyDownloadStringAsync
    {
        private Stopwatch sw = new Stopwatch();

        public void DoRun()
        {
            const int LargeNumber = 6000000;
            sw.Start();
            Task<int> t1 = CountCharactersAsync(1, "http://www.microsoft.com");
            Task<int> t2 = CountCharactersAsync(2, "http://www.illustratedcsharp.com");
            CountToALargeNumber(1, LargeNumber);
            CountToALargeNumber(2, LargeNumber);
            CountToALargeNumber(3, LargeNumber);
            CountToALargeNumber(4, LargeNumber);
            Console.WriteLine("Chars in http://www.microsoft.com: {0}", t1.Result);
            Console.WriteLine("Chars in http://www.illustratedcsharp.com: {0}", t2.Result);
        }

        private async Task<int> CountCharactersAsync(int id, string site)
        {
            WebClient wc = new WebClient();
            Console.WriteLine("Starting call {0} : {1, 4:N0} ms", id, sw.Elapsed.TotalMilliseconds);
            string result = await wc.DownloadStringTaskAsync(new Uri(site));
            Console.WriteLine(" Call {0} completed: {1, 4:N0} ms", id, sw.Elapsed.TotalMilliseconds);
            return result.Length;
        }

        private void CountToALargeNumber(int id, int value)
        {
            for (long i = 0; i < value; i++)
                ;

            Console.WriteLine(" End counting {0} : {1, 4:N0} ms", id, sw.Elapsed.TotalMilliseconds);
        }
    }

    internal class Program
    {
        private static void Main()
        {
            MyDownloadString ds1 = new MyDownloadString();
            ds1.DoRun();

            var ds2 = new MyDownloadStringAsync();
            ds2.DoRun();
        }
    }

}

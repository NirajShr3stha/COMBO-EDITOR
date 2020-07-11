using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Concurrent;
using System.Drawing;
using Console = Colorful.Console;
using System.Diagnostics;
using System.IO;
using QuartzAuth;

namespace RyugaEditor
{
    static class Checker
    {
        public static ConcurrentQueue<string> accounts = new ConcurrentQueue<string>();
        public static int progress = 0;
        public static ConcurrentQueue<string> results = new ConcurrentQueue<string>();
        [STAThread]
        static void Main(string[] args)
        {
            Console.Title = "CleanCombo | Made for XDWOLF ";
            Console.WriteLine("ENJOY THIS EDITOR LMAO");
            Console.Clear();
            //QuartzAuth.Auth.Init("A59UcaRRfHut6UZBCcCHYFTXUuyBd4V9xopXhKwq", "KfkP7LZVlK4BUIk1uWMtHUPhGQ6Qej", "1.0");
            /*
        ReTryy:
            Visuals.WriteLine("Please, enter your username.", Color.LightBlue);
            string username = Console.ReadLine();
            Visuals.WriteLine("Please, enter your password.", Color.LightBlue);
            string password = Console.ReadLine();
            Visuals.WriteLine("Please, enter your email.", Color.LightBlue);
            string email = Console.ReadLine();
            Visuals.WriteLine("Please, enter your token.", Color.LightBlue);
            string token = Console.ReadLine();

            if (!QuartzAuth.Auth.Register(username, password, email, token))
            {
                Visuals.WriteLine("You have entered wrong details...", Color.IndianRed);
                Thread.Sleep(1500);
                goto ReTryy;
            }*/
            //if (Auth.GetAuth("A59UcaRRfHut6UZBCcCHYFTXUuyBd4V9xopXhKwq", "KfkP7LZVlK4BUIk1uWMtHUPhGQ6Qej", "1.0"))
          //  {
      //          Visuals.WriteLine("Successfully authed!", Color.LimeGreen);
         //   }
     //       else
       //     {
       //         Visuals.WriteLine("Unsuccessfully authed!", Color.IndianRed);
      //          Console.ReadLine();
      //          Environment.Exit(69);
       //     }
        ReLoad:
            try
            {
                var lines = File.ReadAllLines("combos.txt");
                foreach (var line in lines) {
                    if (line.Split(':').Length == 2)
                        accounts.Enqueue(line);
                };
            }
            catch
            {
                Visuals.WriteLine("Please, create combos.txt and put your lines in, then press any key...", Color.IndianRed);
                Console.ReadKey();
                goto ReLoad;
            }
            Visuals.WriteLine("Now editing " + accounts.Count + " lines...", Color.White);
            var arr = accounts.ToArray();
            var stopwatch = Stopwatch.StartNew();
            foreach (var line in accounts)
            {
                if (line.Split(':').Length == 2)
                {
                    Edit(line);
                }
            }
            Console.WriteLine("");
            Visuals.WriteLine("Successfully edited all lines in " + stopwatch.ElapsedMilliseconds+ " milliseconds !", Color.LightGreen);
            Visuals.WriteLine("Randomizing the results...", Color.LightBlue);
            var resultarr = results.ToArray();
            Randomize<string>(resultarr);
            Visuals.WriteLine("Successfully randomized!", Color.LightGreen);
            Visuals.WriteLine("Saving results...", Color.LightBlue);
            File.WriteAllLines("output.txt", resultarr.Distinct<string>());
            Visuals.WriteLine("Successfully saved!", Color.LightGreen);
            Console.ReadLine();
        }
        public static void Edit(string line)
        {
            string mail = line.Split(':')[0];
            string pass = line.Split(':')[1];
            results.Enqueue(line + "!");
            results.Enqueue(line + "123");
            results.Enqueue(line);
            results.Enqueue(line.Replace("a", "4").Replace("A", "4"));
            string res = "";
            foreach (char charr in pass)
            {
                switch (charr)
                {
                    case 'A':
                        res += "@";
                        break;
                    case 't':
                        res += "7";
                        break;
                    case 'e':
                        res += "3";
                        break;
                    default:
                        res += charr;
                        break;
                }
            }
            results.Enqueue(mail + ":" + res);
            results.Enqueue(mail + ":" + res + "!");
            //Console.WriteLine("EDITED: " + line, Color.LimeGreen);
            progress++;
            //Console.Title = "ClearCombo | Progress: " + progress + "/" + accounts.Count + " | Made for Ryuga by t.me/Gaztoof";
            return;
        }
        public static void Randomize<T>(T[] items)
        {
            Random random = new Random();
            for (int i = 0; i < items.Length - 1; i++)
            {
                int num = random.Next(i, items.Length);
                T t = items[i];
                items[i] = items[num];
                items[num] = t;
            }
        }
    }
    class Visuals
    {
        public static void WriteLine(string Content, Color color)
        {
            Console.Write("[", Color.DarkOrange);
            Console.Write(DateTime.Now.ToString("HH:mm:ss"), Color.DeepSkyBlue);
            Console.Write("]", Color.DarkOrange);
            Console.Write(" > ", Color.White);
            Console.WriteLine(Content, color);
        }
    }

}

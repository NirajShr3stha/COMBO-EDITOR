using System;
using System.Drawing;
using System.Threading;

namespace RyugaEditor
{
    class Auth
    {
        public static string username { get; internal set; }
        public static string password { get; internal set; }

        public static bool GetAuth(string programkey, string variablekey, string version)
        {
            QuartzAuth.Auth.Init(programkey, variablekey, version);
        Auth:
            if (Auth.GetHasAccount()) //login
            {
                while (!QuartzAuth.Auth.Login(Auth.GetUserName(), Auth.GetPassword()))
                {
                    Visuals.WriteLine("Login failed...", Color.Red);
                    Thread.Sleep(750);
                }
                Visuals.WriteLine("Welcome, " + QuartzAuth.UserInfo.Username, Color.Cyan);
                return true;
            }
            else // Register
            {
                while (!QuartzAuth.Auth.Register(Auth.GetUserName(), Auth.GetPassword(), Auth.GetEmail(), Auth.GetToken()))
                {
                    Colorful.Console.WriteLine("Register failed", Color.Red);
                    Thread.Sleep(750);
                }
                Colorful.Console.WriteLine("Register Successful, please login.", Color.Green);
                goto Auth;
            }
        }

        public static bool GetHasAccount()
        {
            do
            {
                Colorful.Console.Clear();
                Visuals.WriteLine("Do you want to login or register? [Y = login/n = register]", Color.Cyan);
                var resp = Colorful.Console.ReadLine();
                if (resp.Length == 0)
                    return true;
                switch (resp)
                {
                    case "Y":
                    case "y":
                    case "yes":
                    case "Yes":
                        return true;
                    case "N":
                    case "n":
                    case "No":
                    case "no":
                        return false;
                }
            } while (true);
        }

        public static string GetUserName()
        {
            do
            {
                Colorful.Console.Clear();
                Visuals.WriteLine("Please, enter your username. ", Color.Cyan);
                var resp = Colorful.Console.ReadLine();
                if (resp.Length > 0)
                {
                    Auth.username = resp;
                    return Auth.username;
                }
            } while (true);

        }

        private static Color GetNextRainbow(int i)
        {
            if (i % 7 == 0)
                return Color.Purple;
            else if (i % 6 == 0)
                return Color.Blue;
            else if (i % 5 == 0)
                return Color.LightGreen;
            else if (i % 4 == 0)
                return Color.Yellow;
            else if (i % 3 == 0)
                return Color.Orange;
            else if (i % 2 == 0)
                return Color.Red;
            else
                return Color.White;
        }

        public static string GetPassword()
        {
            string pass = "";
            var index = Colorful.Console.CursorTop;
            Colorful.Console.Clear();
            Visuals.WriteLine("Please, enter your password. ", Color.Cyan);
            do
            {

                ConsoleKeyInfo key = Console.ReadKey(true);
                // Backspace Should Not Work
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    pass += key.KeyChar;
                    Colorful.Console.Write("*", GetNextRainbow(pass.Length));
                }
                else
                {
                    if (key.Key == ConsoleKey.Backspace && pass.Length > 0)
                    {
                        pass = pass.Substring(0, (pass.Length - 1));
                        Colorful.Console.Write("\b \b");
                    }
                    else if (key.Key == ConsoleKey.Enter)
                    {
                        if (pass.Length > 0)
                        {
                            Auth.password = pass;
                            return Auth.password;
                        }
                    }
                }
            } while (true);

        }

        public static string GetEmail()
        {
            do
            {
                Colorful.Console.Clear();
                Visuals.WriteLine("Please, enter your email. ", Color.Cyan);
                var resp = Colorful.Console.ReadLine();
                if (resp.Length > 0)
                    return resp;
            } while (true);
        }

        public static string GetToken()
        {
            do
            {
                Colorful.Console.Clear();
                Visuals.WriteLine("Please, enter your token. ", Color.Cyan);
                var resp = Colorful.Console.ReadLine();
                if (resp.Length > 0)
                    return resp;
            } while (true);
        }
    }
}

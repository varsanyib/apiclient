using APIClient.Internal;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Threading.Channels;

namespace APIClient
{
    class Program
    {
        static APIReq req;
        static bool run = true;
        static void Main(string[] args)
        {
            req = new APIReq();
            ProgramCycle();

        }

        private static void ProgramCycle()
        {
            while (run)
            {
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                Console.Clear();
                AppName();
                if (req.CheckUrl())
                {
                    Console.WriteLine("\nBeállított alapértelmezett elérési útvonal: " + req.GetUrl());
                }
                Menu();
                byte selected = Selector();
                SwitcherAsync(selected).Wait(); //awaitable
                PressKey();
            }
        }

        private static async Task SwitcherAsync(byte selected)
        {
            switch (selected)
            {
                case 1:
                    SetURL();
                    break;
                case 2:
                    await GetReq();
                    break;
                case 3:
                    await PostReq();
                    break;
                case 4:
                    await PutReq();
                    break;
                case 5:
                    await PatchReq();
                    break;
                case 6:
                    await DeleteReq();
                    break;
                case 7:
                    //ReqListView();
                    Console.WriteLine("Nincs implementálva");
                    break;
                case 8:
                    //DelReqList();
                    Console.WriteLine("Nincs implementálva");
                    break;
                case 9:
                    Exit();
                    break;
                default:
                    Console.WriteLine("Hiba történt! Az alkalmazás leáll!");
                    Exit();
                    break;
            }
            
        }
        #region Async API operations
        private static async Task DeleteReq()
        {
            Console.Write("Adja meg a lekérdezés megnevezését: ");
            string nev = Console.ReadLine() ?? "Lekérdezés";
            Console.Write("Adja meg a teljes elérési utat: " + req.GetUrl());
            string route = Console.ReadLine() ?? "";
            APIAnswer a = await req.Get(nev, route);
            await Console.Out.WriteLineAsync(a.GetResult());
        }
        private static async Task PatchReq()
        {
            Console.Write("Adja meg a lekérdezés megnevezését: ");
            string nev = Console.ReadLine() ?? "Lekérdezés";
            Console.Write("Adja meg a teljes elérési utat: " + req.GetUrl());
            string route = Console.ReadLine() ?? "";
            Console.Write("Adja meg, hogy hány darab kulcs-érték párat szeretne hozzáadni a lekérdezéshez: ");
            int x = 0;
            Dictionary<string, string> values = new Dictionary<string, string>();
            string tmp;
            bool success = int.TryParse(Console.ReadLine(), out x);
            if (success)
            {
                for (int i = 0; i < x; i++)
                {
                    Console.Write($"{i + 1}. kulcs:");
                    tmp = Console.ReadLine() ?? "";
                    Console.Write($"{i + 1}. érték:");
                    values.Add(tmp, Console.ReadLine() ?? "");
                    Console.WriteLine();
                }
            }
            APIAnswer a = await req.Patch(nev, values, route);
            await Console.Out.WriteLineAsync(a.GetResult());
        }
        private static async Task PutReq()
        {
            Console.Write("Adja meg a lekérdezés megnevezését: ");
            string nev = Console.ReadLine() ?? "Lekérdezés";
            Console.Write("Adja meg a teljes elérési utat: " + req.GetUrl());
            string route = Console.ReadLine() ?? "";
            Console.Write("Adja meg, hogy hány darab kulcs-érték párat szeretne hozzáadni a lekérdezéshez: ");
            int x = 0;
            Dictionary<string, string> values = new Dictionary<string, string>();
            string tmp;
            bool success = int.TryParse(Console.ReadLine(), out x);
            if (success)
            {
                for (int i = 0; i < x; i++)
                {
                    Console.Write($"{i + 1}. kulcs:");
                    tmp = Console.ReadLine() ?? "";
                    Console.Write($"{i + 1}. érték:");
                    values.Add(tmp, Console.ReadLine() ?? "");
                    Console.WriteLine();
                }
            }
            APIAnswer a = await req.Put(nev, values, route);
            await Console.Out.WriteLineAsync(a.GetResult());
        }
        private static async Task PostReq()
        {
            Console.Write("Adja meg a lekérdezés megnevezését: ");
            string nev = Console.ReadLine() ?? "Lekérdezés";
            Console.Write("Adja meg a teljes elérési utat: " + req.GetUrl());
            string route = Console.ReadLine() ?? "";
            Console.Write("Adja meg, hogy hány darab kulcs-érték párat szeretne hozzáadni a lekérdezéshez: ");
            int x = 0;
            Dictionary<string, string> values = new Dictionary<string, string>();
            string tmp;
            bool success = int.TryParse(Console.ReadLine(), out x);
            if (success)
            {
                for (int i = 0; i < x; i++)
                {
                    Console.Write($"{i+1}. kulcs:");
                    tmp = Console.ReadLine() ?? "";
                    Console.Write($"{i + 1}. érték:");
                    values.Add(tmp, Console.ReadLine() ?? "");
                    Console.WriteLine();
                }
            }
            APIAnswer a = await req.Post(nev, values, route);
            await Console.Out.WriteLineAsync(a.GetResult());
        }

        private static async Task GetReq()
        {
            Console.Write("Adja meg a lekérdezés megnevezését: ");
            string nev = Console.ReadLine() ?? "Lekérdezés";
            Console.Write("Adja meg a teljes elérési út végét: " + req.GetUrl());
            string route = Console.ReadLine() ?? "";
            APIAnswer a = await req.Get(nev, route);
            await Console.Out.WriteLineAsync(a.GetResult());
        }
        #endregion
        #region App preferences
        private static void SetURL()
        {
            string url = string.Empty;
            do
            {
                Console.Write("Kérem adja meg az elérési utat: ");
                url = Console.ReadLine();
            } while (url.Length == 0);

            req.SetUrl(url);
            Console.WriteLine($"\nAz elérési útvonal sikeresen beállítva! ({req.GetUrl()})");
        }
        private static void Exit()
        {
            run = false;
        }

        private static void DelReqList()
        {
            throw new NotImplementedException();
        }

        private static void ReqListView()
        {
            throw new NotImplementedException();
        }
        #endregion
        #region Control
        private static void PressKey()
        {
            Console.Write("\nKérem nyomjon egy billentyűt a folytatáshoz...");
            Console.ReadKey();
        }
        private static byte Selector()
        {
            byte selected;
            bool success = false;
            do
            {
                Console.Write("Választott menüpont: ");
                success = (byte.TryParse(Console.ReadLine(), out selected));

            } while (selected < 1 || selected > 9 && success);
            return selected;
        }

        private static void Menu()
        {
            Console.WriteLine("\nKérem válasszon az alábbi menüpontok közül!\n");
            Console.WriteLine("[1] - Szerver elérési útvonalának beállítása");
            Console.WriteLine("[2] - GET lekérdezés indítása");
            Console.WriteLine("[3] - POST lekérdezés indítása");
            Console.WriteLine("[4] - PUT lekérdezés indítása");
            Console.WriteLine("[5] - PATCH lekérdezés indítása");
            Console.WriteLine("[6] - DELETE lekérdezés indítása");
            Console.WriteLine("[7] - Tárolt lekérdezések eredményeinek visszahívása");
            Console.WriteLine("[8] - Tárolt lekérdezések törlése");
            Console.WriteLine("[9] - Kilépés");
            Console.WriteLine();
        }

        private static void AppName()
        {
            Console.WriteLine(CenterText("▒█▀▀▀ ▀▀█▀▀ ▒█▀▀▀█ ▒█░▒█ 　 ░█▀▀█ ▒█▀▀█ ▀█▀ 　 ▒█▀▀█ ▒█░░░ ▀█▀ ▒█▀▀▀ ▒█▄░▒█ ▀▀█▀▀"));
            Console.WriteLine(CenterText("▒█▀▀▀ ░▒█░░ ░▀▀▀▄▄ ▒█▀▀█ 　 ▒█▄▄█ ▒█▄▄█ ▒█░ 　 ▒█░░░ ▒█░░░ ▒█░ ▒█▀▀▀ ▒█▒█▒█ ░▒█░░"));
            Console.WriteLine(CenterText("▒█░░░ ░▒█░░ ▒█▄▄▄█ ▒█░▒█ 　 ▒█░▒█ ▒█░░░ ▄█▄ 　 ▒█▄▄█ ▒█▄▄█ ▄█▄ ▒█▄▄▄ ▒█░░▀█ ░▒█░░"));
            Console.WriteLine("\n" + CenterText("Powered by: FelX Technical Solutions, Software Development Divison " + req.GetVersion()) + "\n");
            //Console.WriteLine("\n" + CenterText("Tárolt lekérdezések száma: " + req.AnswersCount()));
        }

        private static string CenterText(string text)
        {
            return String.Format("{0," + ((Console.WindowWidth / 2) + (text.Length / 2)) + "}", text);
        }
        #endregion
    }
}
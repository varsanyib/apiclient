using APIClient.Internal;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace APIClient
{
    class Program
    {
        static APIReq req;
        static bool run = true;
        static void Main(string[] args)
        {
            req = new APIReq();

            
            while (run) {

                Console.Clear();
                AppName();

                if (req.CheckUrl())
                {
                    Console.WriteLine("\nBeállított alapértelmezett elérési útvonal: " + req.GetUrl());
                }

                Menu();
                byte selected = Selector();
                OperationsCenter(selected);

            }
            

        }

        private static void OperationsCenter(byte sel)
        {
            switch (sel)
            {
                case 1:
                    SetURL();
                    break;
                case 2:
                    GetReq();
                    break;
                case 3:
                    PostReq();
                    break;
                case 4:
                    PutReq();
                    break;
                case 5:
                    PatchReq();
                    break;
                case 6:
                    DeleteReq();
                    break;
                case 7:
                    ReqListView();
                    break;
                case 8:
                    DelReqList();
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
        #region API Operations
        private static void DeleteReq()
        {
            Console.WriteLine("Fejlesztés alatt!");
        }

        private static void PatchReq()
        {
            Console.WriteLine("Fejlesztés alatt!");
        }

        private static void PutReq()
        {
            Console.WriteLine("Fejlesztés alatt!");
        }

        private static void PostReq()
        {
            Console.WriteLine("Fejlesztés alatt!");
        }

        private static void GetReq()
        {
            Console.WriteLine("Fejlesztés alatt!");
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
            PressKey();
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
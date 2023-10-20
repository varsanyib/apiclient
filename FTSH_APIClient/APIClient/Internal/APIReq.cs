using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Reflection.Metadata;
using System.Net.Http.Headers;
using System.Reflection;

namespace APIClient.Internal
{
    public class APIReq
    {
        #region Properties
        /// <summary>
        /// A szerver címe.
        /// </summary>
        private string url = "";
        /// <summary>
        /// Lefutott lekérdezések listája.
        /// </summary>
        public List<APIAnswer> Answers { get; private set; } = new List<APIAnswer>();
        #endregion
        #region Methods - Properties
        /// <summary>
        /// Visszaadja az alkalmazás verziószámát.
        /// </summary>
        /// <returns></returns>
        public string GetVersion() { return Assembly.GetExecutingAssembly().GetName().Version.ToString(); }
        /// <summary>
        /// Visszaadja a szerver címét.
        /// </summary>
        /// <returns>Szerver címe</returns>
        public string GetUrl() { return url; }

        /// <summary>
        /// Beállítja a szerver elérhetőségét.
        /// </summary>
        /// <param name="url">Szerver címe</param>
        public void SetUrl(string url) 
        {
            this.url = url;
            if (this.url[this.url.Length - 1] != '/')
            {
                this.url += '/';
            }
            
        }

        /// <summary>
        /// Logikai változóban kifejezi, hogy van-e beállított szerver elérhetőségének megfelelő cím.
        /// </summary>
        /// <returns>Igaz feltétel esetén van beállított szerver cím, hamis feltétel esetén nincsen.</returns>
        public bool CheckUrl()
        {
            return this.url.Length != 0;
        }

        /// <summary>
        /// Visszaadja a tárolt lekérdezések számát.
        /// </summary>
        /// <returns>Tárolt lekérdezések száma</returns>
        public int AnswersCount() { return this.Answers.Count; }
        #endregion
        #region Constructors
        /// <summary>
        /// Meghívja a konstruktort
        /// </summary>
        public APIReq() { }

        /// <summary>
        /// Meghívja a konstruktort, illetve beállítja a szerver elérhetőségét a megadott paraméter felhasználásával.
        /// </summary>
        /// <param name="url">Szerver címe</param>
        public APIReq(string url)
        {
            SetUrl(url);
        }
        #endregion
        #region Methods - Requests
        #region GET
        /// <summary>
        /// GET lekérdezés indítása a beállított paraméterekkel.
        /// </summary>
        /// <param name="name">Lekérdezés rövid megnevezése</param>
        /// <param name="route">Opcionális: Elérési útvonal kiegészítés</param>
        public async Task<APIAnswer> Get(string name, string route = "")
        {
            APIAnswer answer = new APIAnswer(new Random().Next(0, 1000), name, "GET", (url + route));
            answer.StartTimer();
            string fullUrl = url + route;
            if (fullUrl.Length != 0)
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("FTSH_API_Client", GetVersion()));
                    HttpResponseMessage response = await client.GetAsync((url + route));

                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        answer.SetResult(Convert.ToInt32(response.StatusCode), content);
                    }
                    else
                    {
                        answer.SetResult(Convert.ToInt32(response.StatusCode), answer.Id + ". lekérdezés sikertelen!");
                    }
                }

                answer.StopTimer();
                Answers.Add(answer);
            }
            return answer;


        }
        #endregion
        #region POST
        /// <summary>
        /// POST lekérdezés indítása a beállított paraméterekkel.
        /// </summary>
        /// <param name="name">Lekérdezés rövid megnevezése</param>
        /// <param name="values">Form kulcs-érték párok megadása</param>
        /// <param name="route">Opcionális: Elérési útvonal kiegészítés</param>
        /// <returns>Választ és technikai információkat tartalmazó példány</returns>
        public async Task<APIAnswer> Post(string name, Dictionary<string, string> values, string route = "")
        {
            APIAnswer answer = new APIAnswer(new Random().Next(0, 1000), name, "POST", (url + route));
            answer.StartTimer();
            string fullUrl = url + route;
            if (fullUrl.Length != 0)
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("FTSH_API_Client", GetVersion()));
                    var valuesForm = new FormUrlEncodedContent(values);
                    HttpResponseMessage response = await client.PostAsync((url + route), valuesForm);

                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        answer.SetResult(Convert.ToInt32(response.StatusCode), content);
                    }
                    else
                    {
                        answer.SetResult(Convert.ToInt32(response.StatusCode), answer.Id + ". lekérdezés sikertelen!");
                    }
                }

                answer.StopTimer();
                Answers.Add(answer);
            }
            return answer;
        }
        #endregion
        #region PUT
        /// <summary>
        /// PUT lekérdezés indítása a beállított paraméterekkel.
        /// </summary>
        /// <param name="name">Lekérdezés rövid megnevezése</param>
        /// <param name="values">Form kulcs-érték párok megadása</param>
        /// <param name="route">Opcionális: Elérési útvonal kiegészítés</param>
        /// <returns>Választ és technikai információkat tartalmazó példány</returns>
        public async Task<APIAnswer> Put(string name, Dictionary<string, string> values, string route = "")
        {
            APIAnswer answer = new APIAnswer(new Random().Next(0, 1000), name, "PUT", (url + route));
            answer.StartTimer();
            string fullUrl = url + route;
            if (fullUrl.Length != 0)
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("FTSH_API_Client", GetVersion()));
                    var valuesForm = new FormUrlEncodedContent(values);
                    HttpResponseMessage response = await client.PutAsync((url + route), valuesForm);

                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        answer.SetResult(Convert.ToInt32(response.StatusCode), content);
                    }
                    else
                    {
                        answer.SetResult(Convert.ToInt32(response.StatusCode), answer.Id + ". lekérdezés sikertelen!");
                    }
                }

                answer.StopTimer();
                Answers.Add(answer);
            }
            return answer;
        }
        #endregion
        #region PATCH
        /// <summary>
        /// PATCH lekérdezés indítása a beállított paraméterekkel.
        /// </summary>
        /// <param name="name">Lekérdezés rövid megnevezése</param>
        /// <param name="values">Form kulcs-érték párok megadása</param>
        /// <param name="route">Opcionális: Elérési útvonal kiegészítés</param>
        /// <returns>Választ és technikai információkat tartalmazó példány</returns>
        public async Task<APIAnswer> Patch(string name, Dictionary<string, string> values, string route = "")
        {
            APIAnswer answer = new APIAnswer(new Random().Next(0, 1000), name, "PATCH", (url + route));
            answer.StartTimer();
            string fullUrl = url + route;
            if (fullUrl.Length != 0)
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("FTSH_API_Client", GetVersion()));
                    var valuesForm = new FormUrlEncodedContent(values);
                    HttpResponseMessage response = await client.PatchAsync((url + route), valuesForm);

                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        answer.SetResult(Convert.ToInt32(response.StatusCode), content);
                    }
                    else
                    {
                        answer.SetResult(Convert.ToInt32(response.StatusCode), answer.Id + ". lekérdezés sikertelen!");
                    }
                }

                answer.StopTimer();
                Answers.Add(answer);
            }
            return answer;
        }
        #endregion
        #region DELETE
        /// <summary>
        /// DELETE lekérdezés indítása a beállított paraméterekkel.
        /// </summary>
        /// <param name="name">Lekérdezés rövid megnevezése</param>
        /// <param name="route">Opcionális: Elérési útvonal kiegészítés</param>
        /// <returns>Választ és technikai információkat tartalmazó példány</returns>
        public async Task<APIAnswer> Delete(string name, string route = "")
        {
            APIAnswer answer = new APIAnswer(new Random().Next(0, 1000), name, "DELETE", (url + route));
            answer.StartTimer();
            string fullUrl = url + route;
            if (fullUrl.Length != 0)
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("FTSH_API_Client", GetVersion()));
                    HttpResponseMessage response = await client.DeleteAsync((url + route));

                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        answer.SetResult(Convert.ToInt32(response.StatusCode), content);
                    }
                    else
                    {
                        answer.SetResult(Convert.ToInt32(response.StatusCode), answer.Id + ". lekérdezés sikertelen!");
                    }
                }

                answer.StopTimer();
                Answers.Add(answer);
            }
            return answer;
        }
        #endregion
        #endregion
    }
}

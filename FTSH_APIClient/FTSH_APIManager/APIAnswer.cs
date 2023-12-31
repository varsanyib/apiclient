﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FTSH_APIManager
{
    public class APIAnswer
    {
        #region Properties
        /// <summary>
        /// Lekérdezés azonosítója
        /// </summary>
        public int Id { get; private set; }
        /// <summary>
        /// Lekérdezés megnevezése
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// Lekérdezés típusa
        /// </summary>
        public string Method { get; private set; }
        /// <summary>
        /// Lekérdezés teljes elérési útvonala
        /// </summary>
        public string Route { get; private set; }
        /// <summary>
        /// Lekérdezés HTTP státuszkódja
        /// </summary>
        public int Code { get; private set; } = 0;
        /// <summary>
        /// Lekérdezés szöveges eredménye
        /// </summary>
        public string AnswerText { get; private set; } = string.Empty;
        public Dictionary<string, string> Values { get; private set; }
        /// <summary>
        /// A lekérdezés elindításának kezdeti időpontja
        /// </summary>
        public DateTime StartTime { get; private set; }
        /// <summary>
        /// A lekérdezés eredményének kiértékelésének időpontja
        /// </summary>
        public DateTime EndTime { get; private set; }
        /// <summary>
        /// Lekérdezés lezárt állapota
        /// </summary>
        public bool Closed { get; private set; } = false;
        /// <summary>
        /// Lekérdezés JSON formátumban
        /// </summary>
        public JObject AnswerJson { get; private set; }
        #endregion
        #region Methods
        /// <summary>
        /// Beállítja a válaszadás kezdő időpontját
        /// </summary>
        public void StartTimer()
        {
            StartTime = DateTime.Now;
        }
        /// <summary>
        /// Beállítja a válaszadás lezárásának időpontját
        /// </summary>
        public void StopTimer()
        {
            EndTime = DateTime.Now;
        }
        /// <summary>
        /// Megadja a válaszadás eltelt idejét
        /// </summary>
        /// <returns>Eltelt idő</returns>
        public TimeSpan ElapsedTime()
        {
            return EndTime - StartTime;
        }
        /// <summary>
        /// Feltölti a hiányzó adatokat a lekérdezést követően, illetve lezárja a jelenlegi kérést.
        /// </summary>
        /// <param name="code">HTTP Státuszkód</param>
        /// <param name="answertext">Lekérdezett adat szöveges típusban</param>
        public void SetResult(int code, string answertext)
        {
            Code = code;
            AnswerText = answertext;

            try
            {
                AnswerJson = JObject.Parse(AnswerText);
            }
            catch (Exception ex)
            {
                AnswerJson = JObject.Parse("{\"appError\":\"" + ex.Message + "\",\"code\":" + code + "}");
            }

            Closed = true;
        }
        /// <summary>
        /// Visszaadja a lekérdezés eredményét a technikai információkkal együtt.
        /// </summary>
        /// <returns>Lekérdezés eredménye szöveges típusban</returns>
        public string GetResult()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("\n---\n");
            sb.AppendLine("FTSH APIClient - lekérdezés eredménye:");
            sb.AppendLine("Azonosító: " + Id);
            sb.AppendLine("Megnevezés: " + Name);
            sb.AppendLine("Típus: " + Method);
            sb.AppendLine("Elérési út: " + Route);
            if (Values != null && Values.Count != 0)
            {
                sb.AppendLine("Hozzáadott értékek: ");
                foreach (var item in Values)
                {
                    sb.AppendLine(item.Key + " - " + item.Value);
                }
            }
            if (StartTime != DateTime.MinValue)
            {
                sb.AppendLine("Indítás időpontja: " + StartTime.ToString());
            }

            if (Closed)
            {
                if (EndTime != DateTime.MinValue)
                {
                    sb.AppendLine("Lezárás időpontja: " + EndTime.ToString());
                }
                if (ElapsedTime() != TimeSpan.Zero)
                {
                    sb.AppendLine("Eltelt idő: " + ElapsedTime().ToString());
                }
                sb.AppendLine("Státuszkód: " + Code);
                sb.AppendLine("\nEredmény:\n");
                try
                {
                    sb.AppendLine(JsonConvert.SerializeObject(AnswerJson, Formatting.Indented));
                }
                catch (Exception)
                {
                    sb.AppendLine("A válaszként kapott adat nem JSON formátumú!\n");
                    sb.AppendLine(AnswerText);
                }
                sb.AppendLine("\n---");
            }
            else
            {
                sb.AppendLine("Információ: A lekérdezés még nem került lezárásra!");
            }

            return sb.ToString();
        }
        #endregion
        #region Constructors
        /// <summary>
        /// A konstruktor meghívásával beállítja a válaszadás alapértelmezett tulajdonságok értékeit.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="method"></param>
        /// <param name="route"></param>
        /// <exception cref="ArgumentNullException">A paraméter értéke nem lehet nulla!</exception>
        public APIAnswer(int id, string name, string method, string route)
        {
            Id = id;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Method = method ?? throw new ArgumentNullException(nameof(method));
            Route = route ?? throw new ArgumentNullException(nameof(route));
        }
        #endregion
    }
}

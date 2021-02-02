using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using WeatherData.Models;

namespace WeatherData
{
    public static class AddToDb
    {

        //Hämtar från csv-fil och fyller mina tabeller
        //Har kommenterat bort db.SaveChanges() utifall jag skulle råka anropa metoder i denna klass i program klassen
        static List<string> file = new List<string>();

        public static void GenerateData()
        {
            file = ReadFromTextFile();
            Insert(file);
        }

        public static List<string> ReadFromTextFile()
        {
            string file = @"C:\Users\Elin Sandås\source\repos\WeatherData\WeatherData\SeedData\TemperaturData1.csv";

            return File.ReadAllLines(file).ToList();
        }

        public static void Insert(List<string> lines)
        {
            string[] col;

            foreach (string line in lines)
            {
                col = line.Split(","); //date[0], environment[1], Temp [2], Humdidity[3]

                using (var db = new TDContext())
                {
                    Temperature temperature = new Temperature();
                    temperature.Date = DateTime.Parse(col[0]);
                    temperature.Environment = col[1];
                    temperature.Temp = double.Parse(col[2], CultureInfo.InvariantCulture);
                    temperature.Humidity = int.Parse(col[3]);
                    db.Add(temperature);
                    //db.SaveChanges();

                };

            }
        }

        public static List<DateTime> GetDateTimes()
        {
            List<DateTime> list = new List<DateTime>();

            using (var db = new TDContext())
            {
                foreach (var t in db.Temperatures)
                {
                    //yyyy-mm-dd 00:00:00
                    t.Date = t.Date.Value.Date;

                    if (!(list.Contains(t.Date.Value)))
                    {
                        list.Add(t.Date.Value); //Lägger till varje datum EN gång
                    }
                }
            }
            return list;
        }

        public static void FindAverageValues(List<DateTime> list, string environment)
        {
            foreach (var l in list)
            {
                ArrayList al = new ArrayList();
                double sumTempOut = 0;//Håller koll på summan av temp
                double sumTempIn = 0;
                int sumHumOut = 0; //Håller koll på summan av luftfuktighet
                int sumHumIn = 0;
                double amountOut = 0; //För att kunna få ut medeltal
                double amountIn = 0;
                int i = 0;


                using (var db = new TDContext())
                {
                    foreach (var t in db.Temperatures)
                    {
                        if (l == t.Date.Value.Date)
                        {
                            if (t.Environment == "Ute")
                            {
                                sumTempOut += t.Temp.Value;
                                sumHumOut += t.Humidity.Value;
                                amountOut++;
                            }
                            if (t.Environment == "Inne")
                            {
                                sumTempIn += t.Temp.Value;
                                sumHumIn += t.Humidity.Value;
                                amountIn++;
                            }

                        }

                    }
                }


                al.Add(l);
                al.Add(Math.Round(sumTempOut / amountOut, 1));
                al.Add(sumHumOut / Convert.ToInt32(amountOut));
                al.Add(Math.Round(sumTempIn / amountIn, 1));
                al.Add(sumHumIn / Convert.ToInt32(amountIn));
                AddAverageData(al);
            }

        }

        public static void AddAverageData(ArrayList al)
        {
            using (var db = new TDContext())
            {
                Average average = new Average();
                average.Date = (DateTime)al[0];
                average.AvgTemp = (double)al[1];
                average.AvgHum = (int)al[2];
                average.AvgTempInside = (double)al[3];
                average.AvgHumInside = (int)al[4];

                db.Add(average);
                //db.SaveChanges();

            };

        }

        internal static void MakeTempListForMoldRisk()
        {
           

            List<Temperature> temperatureList = new List<Temperature>();
            using (var db = new TDContext())
            {
                temperatureList = db.Temperatures.ToList();
            }

            AddMoldData(temperatureList);

        }

        static void AddMoldData(List<Temperature> temperatureList)
        {
            //Mögelrisk = ((fuktighet - 78) * (temperatur / 15)) / 0,22

            using (var db = new TDContext())
            {
                foreach (var row in temperatureList)
                {
                    double risk = 0;
                 /*   double Hum2 = Convert.ToDouble(row.Humidity.Value)*/
                    risk = ((row.Humidity.Value - 78) * (row.Temp.Value / 15)) / 0.22;

                    MoldRisk moldRisk = new MoldRisk();
                    moldRisk.Date = row.Date.Value;
                    moldRisk.MoldPercentage = Math.Round(risk,4);
                    db.Add(moldRisk);        
                }

                //db.SaveChanges();
            }

        }
        //Lägg till metod för foreign key
    }
}

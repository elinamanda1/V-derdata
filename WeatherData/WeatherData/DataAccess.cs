using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using WeatherData.Models;

namespace WeatherData
{
    class DataAccess
    {
        internal static void FindSelectedDate(string selectedDate, string environment)
        {    
            using (var db = new TDContext())
            {
                var res1 = db.Averages.Where(a => a.Date.ToString().Contains(selectedDate)); // Få in INNE

                int notFound = 0;
                foreach (var r in res1)
                {

                    Console.WriteLine($"Datum:{r.Date.ToShortDateString()}\tMedeltemperatur: {r.AvgTemp}");
                    notFound++;
                }
                if (notFound == 0) Console.WriteLine("Det finns inte väderdata för det valda datumet");
            }
        
        }
        
        internal static void SortTemp(bool environment)
        {
            using (var db = new TDContext())
            {
                var res = db.Averages.OrderByDescending(a => a.AvgTemp );

                Console.Clear();
                int i = 0;
                foreach (var r in res)
                {   
                    Console.WriteLine($"Datum:{r.Date.ToShortDateString()}\tMedeltemperatur: {r.AvgTemp}" );
                    i++;
                    if (i == 60) break;
                  
                }
            }
        }internal static void SortHum(bool environment)
        {
            using (var db = new TDContext())
            {
                var res = db.Averages.OrderBy(a => a.AvgHum);

                Console.Clear();
                int i = 0;
                foreach (var r in res)
                {
               
                    Console.WriteLine($"Datum:{r.Date.ToShortDateString()}\tMedelluftfuktighet: {r.AvgHum}%");
                    i++;
                    if(i == 60)break;
                }
            } 
        }
        internal static void SortMoldRisk()
        {
            using (var db = new TDContext())
            {

                var res = db.MoldRisk.OrderBy(m => m.MoldPercentage);

                int i = 0;
                Console.Clear();
                foreach (var row in res)
                {
                    if (row.MoldPercentage > 1 && row.MoldPercentage < 100)
                    {
                        Console.WriteLine($"Datum:{row.Date}\tMögelrisk: {row.MoldPercentage} %");
                        i++;
                        if (i == 60) break;
                    }

                }//Jag vet inte om jag skulle gjort detta på medeltemperaturen. Så om min kod stämmer, så
                //skulle jag bara gjort likadant som jag gjort, men på average värdena istället. 
                //Förstod inte riktigt hur jag skulle göra på denna.
            }
        }
        internal static void MeteorologicalAutumn()
        {
            //då dygnsmedeltemperaturen som högst är 0 grader Celsius minst 5 dagar i sträck.

            //Om dygnsmedeltemperaturen är lägre än 10,0°C fem dygn i följd, säger vi att hösten anlände det första av dessa dyg

            int daysInRow = 0;
            List<Average> datesInRow = new List<Average>();

            using (var db = new TDContext())
            {
                var res = from a in db.Averages
                          where a.Date.Month == 08 || a.Date.Month == 09 || a.Date.Month == 10
                          orderby a.Date
                          select a;

                Console.Clear();
                foreach (var row in res)
                {
                    if(row.AvgTemp < 10)
                    {
                         daysInRow++;
                        datesInRow.Add(row);
                        if (daysInRow == 5)
                        {
                            
                            Console.WriteLine($"Det finns inte väderdata för alla datum år 2016 i databasen,\n" +
                                $" men om man utgår från de datum som finns så " +
                                $"började\nden meteorologiska hösten :{datesInRow[0].Date.Date}");
                            Console.ReadKey();
                        } 
                    }
                    else
                    {
                        datesInRow.Clear();
                        daysInRow = 0;
                    }
                }

            }
        }
        internal void MeteorologicalWinter()
        {
            using (var db = new TDContext())
            {

                


            }
        }

    }
}

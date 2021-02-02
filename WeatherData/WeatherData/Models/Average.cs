using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherData.Models
{
    class Average
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double? AvgTemp { get; set; }
        public int? AvgHum { get; set; }
        public double? AvgTempInside { get; set; }
        public int? AvgHumInside { get; set; }

    }
}

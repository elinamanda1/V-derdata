using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherData.Models
{
    class MoldRisk
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double MoldPercentage { get; set; }
    }
}

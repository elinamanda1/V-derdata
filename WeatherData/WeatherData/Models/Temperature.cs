using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherData.Models
{
    class Temperature
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public string? Environment { get; set; }
        public double? Temp {get; set; }
        public int? Humidity { get; set; }
        public Average? Average { get; set; }

        
    }
}

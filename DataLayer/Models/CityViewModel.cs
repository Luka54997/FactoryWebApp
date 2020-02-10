using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Models
{
    public class CityViewModel
    {
        public City City { get; set; }
        public IEnumerable<Factory> Factories { get; set;}
    }
}

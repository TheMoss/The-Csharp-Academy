using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabitTracker
{
    public class HabitModel
    {        
        public string Name { get; set; }
        public string Date { get; set; }
        public int Quantity { get; set; }

        public HabitModel(string date, int quantity, string name = "drinking water")
        {
            Name = name;
            Date = date;
            Quantity = quantity;
        }
    }
}

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabitTracker
{
    public class HabitModel
    {        
        public ObjectId Id { get; set; }        
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("date")]
        public string Date { get; set; }
        [BsonElement("quantity")]
        public int Quantity { get; set; }

        public HabitModel(string date, int quantity, string name = "drinking water")
        {            
            Name = name;
            Date = date;
            Quantity = quantity;
        }
    }
}

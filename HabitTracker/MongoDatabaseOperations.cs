using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabitTracker
{
    public class MongoDatabaseOperations
    {

        public void ReadAllRecords(IMongoCollection<HabitModel> mongoCollection)
        {
            var filter = Builders<HabitModel>.Filter.Empty;
            var habitList = mongoCollection.Find(filter).ToList();
            foreach (var habit in habitList)
            {
                Console.WriteLine($"You drank {habit.Quantity} glasses of water on {habit.Date}");
            }
        }

        public void CreateRecord(IMongoCollection<HabitModel> mongoCollection, HabitModel bsonDocument)
        {
            mongoCollection.InsertOne(bsonDocument);
            Console.WriteLine("Adding data...");
            Thread.Sleep(800);
        }

        public void UpdateRecord(IMongoCollection<HabitModel> mongoCollection, string date, int quantity)
        {
            var filter = Builders<HabitModel>.Filter.Eq(habit => habit.Date, date);
            var update = Builders<HabitModel>.Update.Set(update => update.Quantity, quantity);
            var result = mongoCollection.FindOneAndUpdate(filter, update);
            if (result != null)
            {
                Console.WriteLine("Update successful.");
            }
            else
            {
                Console.WriteLine("Update failed.");
            }
            Thread.Sleep(800);
        }

        public void DeleteRecord(IMongoCollection<HabitModel> mongoCollection, string date)
        {
            var filter = Builders<HabitModel>.Filter.Eq(habit => habit.Date, date);
            var result = mongoCollection.FindOneAndDelete(filter);

            if (result != null)
            {
                Console.WriteLine("Delete successful.");
            }
            else
            {
                Console.WriteLine("Delete failed.");
            }
            Thread.Sleep(800);

        }
    }
}

using HabitTracker;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

BsonClassMap.RegisterClassMap<HabitModel>();

string connectionString = //connection string
MongoClient dbClient = new MongoClient(connectionString);

var habitsDatabase = dbClient.GetDatabase("HabitTracker");
var habitsCollection = habitsDatabase.GetCollection<BsonDocument>("loggedHabits");

var habitsApp = new HabitsAppUI() { MongoCollection = habitsCollection };
while (habitsApp.IsRunning)
{
    habitsApp.MainMenu();
}

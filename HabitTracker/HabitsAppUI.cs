using Amazon.SecurityToken.Model;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HabitTracker
{
    internal class HabitsAppUI
    {

        public string VerifiedDate { get; set; }
        public int VerifiedQuantity { get; set; }
        public bool IsRunning { get; set; } = true;
        public IMongoCollection<BsonDocument> MongoCollection { get; set; }

        MongoDatabaseOperations dbOperations = new();

        public void MainMenu()
        {
            Console.WriteLine("1 - view all records");
            Console.WriteLine("2 - add new record");
            Console.WriteLine("3 - edit a record");
            Console.WriteLine("4 - delete a record");
            Console.WriteLine("0 - quit");


            string chosenOption = Console.ReadLine();
            Console.Clear();
            switch (chosenOption)
            {
                case "0": //quit
                    IsRunning = false;
                    break;

                case "1": //view all
                    dbOperations.ReadAllRecords(MongoCollection);
                    Console.WriteLine("\nAll documents in the collection are listed. Press any key to return to main menu.");
                    Console.ReadLine();
                    break;

                case "2": //add new
                    Console.WriteLine("How many glasses of water did you drink?");
                    GetQuantity();
                    Console.Clear();
                    GetDate();
                    var newDocument = new HabitModel(VerifiedDate, VerifiedQuantity);
                    var bsonDocument = newDocument.ToBsonDocument();
                    dbOperations.CreateRecord(MongoCollection, bsonDocument);
                    Console.WriteLine("New record created, press any key to return to menu.");
                    Console.ReadKey();
                    break;

                case "3": 
                    dbOperations.ReadAllRecords(MongoCollection);
                    Console.WriteLine("\nA record from which day would you like to update?");
                    GetDate();
                    Console.WriteLine("What should be the new amount of glasses of water?");
                    GetQuantity();
                    dbOperations.UpdateRecord(MongoCollection, VerifiedDate, VerifiedQuantity);
                    Console.WriteLine("Record edited, return to main menu");
                    Console.ReadKey();
                    break;

                case "4": //delete record
                    dbOperations.DeleteRecord();
                    break;
            }
            Console.Clear();
        }

        private int GetQuantity()
        {
            Console.WriteLine("Use only integers. Write 0 to return to main menu.");
            string userQuantity = Console.ReadLine();
            while (true)
            {
                if (IsInputZero(userQuantity))
                {
                    QuitToMainMenu();
                }
                else if (!IsQuantityInteger(userQuantity))
                {
                    Console.WriteLine("The value wasn't an integer, try again. Write 0 to return to main menu.");
                    userQuantity = Console.ReadLine();
                }
                else
                {
                    break;
                }
            }
            return VerifiedQuantity;
        }

        private bool IsQuantityInteger(string userQuantity)
        {
            Console.Clear();
            IsInputZero(userQuantity);
            bool isInteger = int.TryParse(userQuantity, out int finalQuantity);
            if (isInteger)
            {
                Console.WriteLine("The value is an integer.");
                VerifiedQuantity = finalQuantity;
                return true;
            }
            else
            {
                return false;
            }
        }


        private bool IsInputZero(string consoleInput)
        {
            if (consoleInput == "0")
            {
                QuitToMainMenu();
            }
            return false;
        }
        private void QuitToMainMenu()
        {
            Console.WriteLine("Returning to main menu...");
            Console.Clear();
            MainMenu();
        }

        private string GetDate()
        {
            Console.WriteLine("Insert the date in dd-mm-yyyy format. Write 0 to return to main menu.");
            string userDate = Console.ReadLine();
            while (true)
            {
                if (IsInputZero(userDate))
                {
                    QuitToMainMenu();
                }
                else if (!IsDateFormatCorrect(userDate))
                {
                    Console.WriteLine("Date format was incorrect, try again. Write 0 to return to main menu.");
                    userDate = Console.ReadLine();
                }
                else
                {
                    break;
                }
            }
            return VerifiedDate;
        }

        private bool IsDateFormatCorrect(string userDate)
        {
            Console.Clear();
            bool isDate = DateTime.TryParse(userDate, out DateTime date);
            if (isDate)
            {
                Console.WriteLine("Date is in correct format.");
                VerifiedDate = userDate;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

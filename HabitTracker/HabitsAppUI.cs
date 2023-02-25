﻿using Amazon.SecurityToken.Model;
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

            switch (chosenOption)
            {
                case "0":
                    IsRunning = false;
                    break;

                case "1":
                    dbOperations.ReadAllRecords(MongoCollection);
                    Console.WriteLine("\nAll documents in the collection are listed. Press any key to return to main menu.");
                    Console.ReadLine();
                    break;

                case "2":
                    GetQuantity();
                    GetDate();
                    var newDocument = new HabitModel(VerifiedDate, VerifiedQuantity);
                    var bsonDoc = newDocument.ToBsonDocument();
                   dbOperations.CreateRecord(MongoCollection, bsonDoc);
                    Console.WriteLine("\nNew record created, press any key to return to menu.");
                    Console.ReadLine();
                    break;

                case "3":
                    dbOperations.UpdateRecord();
                    break;

                case "4":
                    dbOperations.DeleteRecord();
                    break;
            }
            
            Console.Clear();
        }

        public int GetQuantity()
        {
            Console.WriteLine("How many glasses of water did you drink? Use only integers");
            string userQuantity = Console.ReadLine();
            IsQuantityInteger(userQuantity);
            return VerifiedQuantity;
        }

        public void IsQuantityInteger(string userQuantity)
        {
            bool isInteger = int.TryParse(userQuantity, out int finalQuantity);
            if (isInteger)
            {
                Console.WriteLine("The value is an integer.");
                VerifiedQuantity = finalQuantity;
            }
            else if (userQuantity== "0") 
            {
                
                QuitToMainMenu();
            }
            else
            {
                Console.WriteLine("The value isn't an integer, try again.");
                GetQuantity();
            }
        }

        private void QuitToMainMenu()
        {
            Console.WriteLine("Returning to main menu...");
            Console.Clear();
            MainMenu();
        }

        public string GetDate()
        {
            Console.WriteLine("When did you drink the water? Insert date in dd-mm-yyyy format.");
            string userDate = Console.ReadLine();
            IsDateFormatCorrect(userDate);
            return VerifiedDate;
        }

        public void IsDateFormatCorrect(string userDate) 
        {
            bool isDate = DateTime.TryParse(userDate, out DateTime date);
            if (isDate) 
            {
                Console.WriteLine("Date is in correct format.");
                VerifiedDate = userDate;
            }
            else if(userDate == "0")
            {
                QuitToMainMenu();
            }
            else
            {
                Console.WriteLine("Format is incorrect, try again. Try again.");
                GetDate();
            }
        }
    }
}

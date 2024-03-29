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
    public class HabitsAppUI
    {

        public string VerifiedDate { get; set; }
        public int VerifiedQuantity { get; set; }
        public bool IsRunning { get; set; } = true;
        public IMongoCollection<HabitModel> MongoCollection { get; set; }

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

                    while (true)
                    {
                        Console.WriteLine("How many glasses of water did you drink?");
                        GetQuantity();
                        GetDate();
                        var newDocument = new HabitModel(VerifiedDate, VerifiedQuantity);

                        dbOperations.CreateRecord(MongoCollection, newDocument);
                        break;
                    }
                    break;

                case "3":

                    while (true)
                    {
                        dbOperations.ReadAllRecords(MongoCollection);

                        Console.WriteLine("\nA record from which day would you like to update?");
                        GetDate();
                        Console.WriteLine("\nWhat should be the new amount of glasses of water?");
                        GetQuantity();

                        dbOperations.UpdateRecord(MongoCollection, VerifiedDate, VerifiedQuantity);

                        break;
                    }
                    break;



                case "4": //delete record

                    while (true)
                    {
                        dbOperations.ReadAllRecords(MongoCollection);

                        Console.WriteLine("\nA record from which day would you like to delete?");
                        GetDate();
                        Console.WriteLine("Are you sure? Write Y to continue.");

                        string userInput = Console.ReadLine();
                        if (userInput == "Y")
                        {
                            dbOperations.DeleteRecord(MongoCollection, VerifiedDate);
                        }
                        else
                        {
                            Console.WriteLine("Returning to main menu...");
                        }
                        Thread.Sleep(800);
                        break;
                    }
                    break;

            }
            Console.Clear();
        }


        private int GetQuantity()
        {

            Console.WriteLine("Use only integers.");
            string userQuantity = Console.ReadLine();
            while (true)
            {
                if (!IsUserInputInteger(userQuantity))
                {

                    Console.Clear();
                    Console.WriteLine("The value wasn't an integer, try again.");
                    userQuantity = Console.ReadLine();
                }
                else
                {
                    break;
                }
            }
            Console.Clear();
            return VerifiedQuantity;
        }

        public bool IsUserInputInteger(string userQuantity)
        {
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

        private string GetDate()
        {
            Console.WriteLine("Insert the date in dd-mm-yyyy format.");
            string userDate = Console.ReadLine();
            while (true)
            {
                if (!IsDateFormatCorrect(userDate))
                {

                    Console.WriteLine("Date format was incorrect, try again.");
                    userDate = Console.ReadLine();
                    Console.Clear();
                }
                else
                {
                    break;
                }
            }
            Console.Clear();
            return VerifiedDate;
        }
        public bool IsDateFormatCorrect(string userDate)
        {
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

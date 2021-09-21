using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Layerd.Domain;
using Layerd.Service;

namespace Layerd.UI
{
    public class TransactionUI : IUI
    {
        private readonly IService Service;

        public void UpdateFile()
        {
            Service.UpdateFile();
        }

        public TransactionUI(IService service)
        {
            Service = service;
        }

        public void DisplayMenu()
        {
            Console.WriteLine("0. Close application.");
            Console.WriteLine("1. Display menu.");
            Console.WriteLine("2. Add new transaction.");
            Console.WriteLine("3. Show all transactions.");
            Console.WriteLine("4. Filter the transaction by name.");
            Console.WriteLine("5. Filter the transaction by date.");
            Console.WriteLine("6. Filter the transactions between 2 dates.");
            Console.WriteLine("7. Update existing transaction through ID.");
            Console.WriteLine();
        }

        public int ReadCommand()
        {
            string str = Console.ReadLine();
            Console.WriteLine("");

            return int.TryParse(str, out int command) ? command : -1;
        }

        //add transaction (date, name, sum, type)
        public void AddTransaction()
        {
            DateTime dateTime;
            bool succeded;
            do
            {
                string format = "yyyy/MM/dd HH:mm:ss";
                Console.WriteLine($"Give a date and time in the following format: {format}");
                string dateString = Console.ReadLine();
                succeded = DateTime.TryParseExact(dateString, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime);
            }
            while (!succeded);

            Console.WriteLine("Enter the Name of your transaction");
            string name = Console.ReadLine();

            double amount;
            do
            {
                Console.WriteLine("Enter the Value of your transaction");
                succeded = double.TryParse(Console.ReadLine(), out amount);
            }
            while (!succeded);

            string transactionType;
            do
            {
                Console.WriteLine($"Is your transaction {TransactionType.Incoming} or {TransactionType.Outgoing}?");
                transactionType = Console.ReadLine();
            }
            while (transactionType != TransactionType.Outgoing.ToString() && transactionType != TransactionType.Incoming.ToString());
            TransactionType type = (TransactionType)Enum.Parse(typeof(TransactionType), transactionType);

            Transaction transaction = new Transaction
            {
                Date = dateTime,
                Name = name,
                Amount = amount,
                Type = type
            };

            if (null == Service.AddTransaction(transaction))
            {
                Console.WriteLine("Failed adding transaction.");
            }
            else
            {
                Console.WriteLine("Transaction added succesfully.");
            }
        }

        public void DisplayAllTransactions()
        {
            foreach (Transaction transaction in Service.GetAllTransactions())
            {
                Console.WriteLine(transaction.ToString());
            }
        }

        public void FilterByName()
        {
            Console.WriteLine("Transaction name");
            string transactionName = Console.ReadLine();

            IEnumerable<Transaction> listOfTransactions = Service.FilterByName(transactionName);

            if (listOfTransactions.Any())
            {
                foreach (Transaction transaction in listOfTransactions)
                {
                    Console.WriteLine(transaction.ToString());
                }
            }
            else
            {
                Console.WriteLine("No transactions with such name.");
            }
        }

        public void FilterBetweenDates()
        {

            DateTime dateTime;
            DateTime secondDateTime;

            IEnumerable<Transaction> listOfTransactions;

            bool succeded;
            do
            {
                string format = "yyyy/MM/dd HH:mm:ss";
                Console.WriteLine($"Give the first date and time in the following format: {format}");
                string dateString = Console.ReadLine();
                succeded = DateTime.TryParseExact(dateString, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime);
            }
            while (!succeded);

            do
            {
                string format = "yyyy/MM/dd HH:mm:ss";
                Console.WriteLine($"Give the second date and time in the following format: {format}");
                string dateString = Console.ReadLine();
                succeded = DateTime.TryParseExact(dateString, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out secondDateTime);
            }
            while (!succeded);

            listOfTransactions = Service.FilterBetweenDates(dateTime, secondDateTime);


            if (listOfTransactions.Any())
            {
                Console.WriteLine($"Found {listOfTransactions.Count()} transactions:");
                foreach (Transaction transaction in listOfTransactions)
                {
                    Console.WriteLine(transaction);
                }
            }
            else
            {
                Console.WriteLine("No such transaction betweem those dates.");
            }
        }


        public void FilterByDate()
        {
            string filterType;
            do
            {
                Console.WriteLine($"Which type of filter do you want: {FilterType.AfterDate}, {FilterType.BeforeDate}");
                filterType = Console.ReadLine();
            }
            while (filterType != FilterType.AfterDate.ToString() && filterType != FilterType.BeforeDate.ToString());
            FilterType type = (FilterType)Enum.Parse(typeof(FilterType), filterType);

            DateTime dateTime;
            bool succeded;
            do
            {
                string format = "yyyy/MM/dd HH:mm:ss";
                Console.WriteLine($"Give a date and time in the following format: {format}");
                string dateString = Console.ReadLine();
                succeded = DateTime.TryParseExact(dateString, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime);
            }
            while (!succeded);
            IEnumerable<Transaction> listOfTransactions;

            listOfTransactions = Service.FilterWithDate(type, dateTime);



            if (listOfTransactions.Any())
            {
                Console.WriteLine($"Found {listOfTransactions.Count()} transactions:");
                foreach (Transaction transaction in listOfTransactions)
                {
                    Console.WriteLine(transaction);
                }
            }
            else
            {
                Console.WriteLine("No such transaction before that date.");
            }
        }

        public void UpdateTransaction()
        {
            // parse string into guid

            bool succeded;
            Guid idSearch;
            do
            {
                string format = "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX";
                Console.WriteLine($"Give the ID of the Transaction you want to update in the following format: {format}");

                succeded = Guid.TryParse(Console.ReadLine(), out idSearch);
            }
            while (!succeded);


            Console.WriteLine("Enter the new Values for your Updated Transaction");
            Console.WriteLine();


            DateTime newDateTime;
            do
            {
                string format = "yyyy/MM/dd HH:mm:ss";
                Console.WriteLine($"Give the New date and time in the following format: {format}");
                string dateString = Console.ReadLine();
                succeded = DateTime.TryParseExact(dateString, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out newDateTime);
            }
            while (!succeded);

            Console.WriteLine("Enter the New Name of your transaction");
            string newName = Console.ReadLine();

            double newAmount;
            do
            {
                Console.WriteLine("Enter the New Value of your transaction");
                succeded = double.TryParse(Console.ReadLine(), out newAmount);
            }
            while (!succeded);

            string newTransactionType;
            do
            {
                Console.WriteLine($"Is your Updated transaction {TransactionType.Incoming} or {TransactionType.Outgoing}?");
                newTransactionType = Console.ReadLine();
            }
            while (newTransactionType != TransactionType.Outgoing.ToString() && newTransactionType != TransactionType.Incoming.ToString());
            TransactionType type = (TransactionType)Enum.Parse(typeof(TransactionType), newTransactionType);

            Transaction transaction = new Transaction
            {
                Id = idSearch,
                Date = newDateTime,
                Name = newName,
                Amount = newAmount,
                Type = type
            };

            if (null == Service.UpdateTransaction(transaction))
            {
                Console.WriteLine("Failed updating transaction.");
            }
            else
            {
                Console.WriteLine("Transaction updated successfully!");
            }
        }
    }
}


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
            Console.WriteLine("3. Update existing transaction through ID.");
            Console.WriteLine("4. Filtering transactions menu");
            Console.WriteLine("5. Deleting Transactions menu");
            Console.WriteLine("6. Show the sum of the transaction values of a given type");
            Console.WriteLine("7. Show account balance at a given date.");
            Console.WriteLine();
        }

        public void DisplayFilters()
        {
            Console.WriteLine("0.Go back to previous menu");
            Console.WriteLine("1. Filter the transaction by name.");
            Console.WriteLine("2. Filter the transaction by date.");
            Console.WriteLine("3. Filter the transactions between 2 dates.");
            Console.WriteLine("4. Filter the transactions bigger than a given amount.");
            Console.WriteLine("5. Filter the transactions bigger than a given amount and before a given date");
            Console.WriteLine("6. Filter the transactions by Type.");
            Console.WriteLine("7. Show all transactions.");
            Console.WriteLine();
        }

        public void DisplayDeletes()
        {
            Console.WriteLine("0.Go back to previous menu");
            Console.WriteLine("1. Delete transaction though a given date.");
            Console.WriteLine("2. Delete transaction though a given interval of dates.");
            Console.WriteLine("3. Delete transaction though a given type.");
            Console.WriteLine();
        }

        public int ReadCommand()
        {
            string str = Console.ReadLine();
            Console.WriteLine("");

            return int.TryParse(str, out int command) ? command : -1;
        }

        static Guid ReadGuid()
        {
            bool succeded;
            Guid id;
            do
            {
                string format = "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX";
                Console.WriteLine($"Give the ID of the Transaction you want to update in the following format: {format}");

                succeded = Guid.TryParse(Console.ReadLine(), out id);
            }
            while (!succeded);

            return id;
        }

        static DateTime ReadDate(string format = "yyyy/MM/dd HH:mm:ss")
        {
            DateTime dateTime;
            bool succeded;
            do
            {
                Console.WriteLine($"Give the date and time in the following format: {format}");
                string dateString = Console.ReadLine();
                succeded = DateTime.TryParseExact(dateString, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime);
            }
            while (!succeded);

            return dateTime;
        }

        static double ReadAmount()
        {
            bool succeded;
            double amount;
            do
            {
                Console.WriteLine("Enter the Value of your transaction");
                succeded = double.TryParse(Console.ReadLine(), out amount);
            }
            while (!succeded);

            return amount;
        }

        static string ReadString(string hint)
        {
            Console.WriteLine(hint);
            string name = Console.ReadLine();
            return name;
        }

        public static T ReadEnum<T>() // T is a generic type class
        {
            string transactionType;

            string[] list = System.Enum.GetNames(typeof(T)); // gets the names of all the the Transaction types (inc and outgo)

            string enumNames = string.Join(" or ", list);

            do
            {
                Console.WriteLine($"Is your {typeof(T).Name} {enumNames}?"); // basically using {} instead of +
                transactionType = Console.ReadLine();
            }
            while (!list.Contains(transactionType));

            return (T)Enum.Parse(typeof(T), transactionType);

        }

        //add transaction (date, name, sum, type)
        public void AddTransaction()
        {
            DateTime dateTime = ReadDate();

            string name = ReadString("Enter the name of your transaction");

            double amount = ReadAmount();

            TransactionType transactionType = ReadEnum<TransactionType>();

            Transaction transaction = new()
            {
                Date = dateTime,
                Name = name,
                Amount = amount,
                Type = transactionType
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
            string transactionName = ReadString("Enter the name you want to search");

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

            DateTime dateTime = ReadDate();
            DateTime secondDateTime = ReadDate();

            IEnumerable<Transaction> listOfTransactions;

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

            DateTime dateTime = ReadDate();

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
            Guid id = ReadGuid();

            Console.WriteLine("Enter the new Values for your Updated Transaction");
            Console.WriteLine();

            DateTime newDateTime = ReadDate();

            string newName = ReadString("Enter your Transaction name");

            double newAmount = ReadAmount();

            TransactionType type = ReadEnum<TransactionType>();

            Transaction transaction = new()
            {
                Id = id,
                Date = newDateTime,
                Name = newName,
                Amount = newAmount,
                Type = type
            };

            Service.UpdateTransaction(transaction);
            Console.WriteLine("Transaction updated");
        }

        public void DeleteThroughDate()
        {
            DateTime date = ReadDate();

            IEnumerable<Transaction> listOfFilterByDate = Service.FilterByOneDate(date);

            if (listOfFilterByDate.Any())
            {
                foreach (Transaction transaction in listOfFilterByDate)
                {
                    Console.WriteLine(transaction.Name.ToString());
                }

                Console.WriteLine("Would you like to delete the Transactions shown. Press y for Yes and n for No");
                Console.WriteLine("");

                string input = Console.ReadLine();
                switch (input.ToLower())
                {
                    case "y":
                        {
                            Service.DeleteTransactions(date);
                            Console.WriteLine("Transaction Deleted Successfully!");
                            break;
                        }
                    case "n":
                        {
                            Console.WriteLine("");
                            Console.WriteLine("Enter new command");
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Wrong command");
                            break;
                        }

                }
            }
            else
            {
                Console.WriteLine("No transactions with such date.");
                Console.WriteLine("");
            }
        }

        public void DeleteTransactionsBetweenDates()
        {
            DateTime dateTime = ReadDate();
            DateTime secondDateTime = ReadDate();

            IEnumerable<Transaction> listOfTransactions;

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

            Console.WriteLine("Would you like to delete the Transactions shown. Press y for Yes and n for No");
            Console.WriteLine("");

            string input = Console.ReadLine();
            switch (input.ToLower())
            {
                case "y":
                    {
                        Service.DeleteTransactionById(listOfTransactions.Select(t => t.Id)); // it selects the id from each transaction
                        Console.WriteLine("Transaction Deleted Successfully!");
                        break;
                    }
                case "n":
                    {
                        Console.WriteLine("");
                        Console.WriteLine("Enter new command");
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Wrong command");
                        break;
                    }
            }
        }

        public void DeleteTransactionType()
        {
            TransactionType type = ReadEnum<TransactionType>();

            IEnumerable<Transaction> listOfTransactionsOfTheType;

            listOfTransactionsOfTheType = Service.FilterTransactionTypes(type);

            if (listOfTransactionsOfTheType.Any())
            {
                Console.WriteLine($"Found {listOfTransactionsOfTheType.Count()} transactions :");
                foreach (Transaction transaction in listOfTransactionsOfTheType)
                {
                    Console.WriteLine(transaction);
                }
            }
            else
            {
                Console.WriteLine("No such transaction type");
            }

            Console.WriteLine("Would you like to delete the shown transactions y/n");
            string command = Console.ReadLine();

            switch (command.ToLower())
            {
                case "y":
                    {
                        Service.DeleteTransactionById(listOfTransactionsOfTheType.Select(t => t.Id)); // it selects the id from each transaction
                        Console.WriteLine("Transaction Deleted Successfully!");
                        break;
                    }
                case "n":
                    {
                        Console.WriteLine("");
                        Console.WriteLine("Enter new command");
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Wrong command");
                        break;
                    }
            }
        }

        public void FilterTransactionValues()
        {
            double cValue = ReadAmount();

            IEnumerable<Transaction> listOfTransactionsLargerThan;

            listOfTransactionsLargerThan = Service.FilterTransactionValues(cValue);

            if (listOfTransactionsLargerThan.Any())
            {
                Console.WriteLine($"Found {listOfTransactionsLargerThan.Count()} transactions:");
                foreach (Transaction transaction in listOfTransactionsLargerThan)
                {
                    Console.WriteLine(transaction);
                }
            }
            else
            {
                Console.WriteLine("No such transaction larger than the given amount");
            }

        }

        public void FilterTransactionValueAndDate()
        {
            double amount = ReadAmount();
            DateTime dateTime = ReadDate();

            IEnumerable<Transaction> listOfTransactionsLargerThanAndBeforeDate;

            listOfTransactionsLargerThanAndBeforeDate = Service.FilterTransactionValueAndDate(dateTime, amount);

            if (listOfTransactionsLargerThanAndBeforeDate.Any())
            {
                Console.WriteLine($"Found {listOfTransactionsLargerThanAndBeforeDate.Count()} transactions:");
                foreach (Transaction transaction in listOfTransactionsLargerThanAndBeforeDate)
                {
                    Console.WriteLine(transaction);
                }
            }
            else
            {
                Console.WriteLine("No such transaction larger than the given amount and before the given date");
            }

        }

        public void FilterTransactionTypes()
        {
            TransactionType type = ReadEnum<TransactionType>();
            IEnumerable<Transaction> listOfTransactionTypes = Service.FilterTransactionTypes(type);

            if (listOfTransactionTypes.Any())
            {
                Console.WriteLine($"Found {listOfTransactionTypes.Count()} transactions:");
                foreach (Transaction transaction in listOfTransactionTypes)
                {
                    Console.WriteLine(transaction);
                }
            }
            else
            {
                Console.WriteLine("No such transaction larger than the given amount and before the given date");
            }
        }

        public void ShowTypeAmount()
        {
            TransactionType type = ReadEnum<TransactionType>();

            IEnumerable<Transaction> listOfType = Service.FilterTransactionTypes(type);

            double sum = Service.ShowTypeAmount(type);

            Console.WriteLine($"Found {listOfType.Count()} tansactions totaling a value of : {sum}$");

        }

        public void AccountBallanceAtGivenTime()
        {
            DateTime dateTime = ReadDate();

            double ballance = Service.AccountBallanceAtGivenTime(dateTime);

            Console.WriteLine($"Your account ballance at this date is : {ballance}$");
        }

    }
}


using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using Layerd.Domain;
using System;
using System.Linq;

namespace Layerd.Repository
{
    //crud operations
    //create    add new stuff to file
    //read      read stuff from file
    //update    update/change stuff from file
    //delete    delete some stuff from file
    public class HashSetRepository : IRepository
    {
        public readonly string SourceFile = @"..\..\..\transactions.json";

        private HashSet<Transaction> Transactions { get; } = new HashSet<Transaction>();

        public HashSetRepository(string sourceFile = @"..\..\..\transactions.json")
        {
            SourceFile = sourceFile;
            ReadAllFromFile();
        }

        public Transaction AddTransaction(Transaction transaction)
        {
            if (Transactions.Add(transaction))
            {
                UpdateFile();
                return transaction;
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<Transaction> GetAllTransactions()
        {
            return Transactions;
        }

        public void UpdateFile()
        {
            //transforms Transactions into a string
            string jsonString = JsonConvert.SerializeObject(Transactions, Formatting.Indented);
            StreamWriter streamWriter = new(SourceFile);
            streamWriter.Write(jsonString);
            streamWriter.Close();
        }

        public void ReadAllFromFile()
        {
            StreamReader streamReader = new(SourceFile);
            string jsonString = streamReader.ReadToEnd();

            //transforms a string into Transactions
            try
            {
                WipeRepository(false);
                foreach (Transaction transaction in JsonConvert.DeserializeObject<HashSet<Transaction>>(jsonString))
                {
                    Transactions.Add(transaction);
                }
            }
            catch (Exception)
            {
                streamReader.Close();
                UpdateFile();
            }
            finally
            {
                if (null != streamReader.BaseStream)
                    streamReader.Close();
            }
        }

        public IEnumerable<Transaction> FilterByName(string transactionName)
        {
            List<Transaction> listOfFilteredTransactions = new();

            foreach (Transaction transaction in Transactions)
            {
                if (transaction.Name.ToLower() == transactionName.ToLower())
                {
                    listOfFilteredTransactions.Add(transaction);
                }
            }

            return listOfFilteredTransactions;
        }

        public IEnumerable<Transaction> FilterWithDate(FilterType type, DateTime dateTime)
        {
            List<Transaction> listOfFilteredTransactions = new();

            foreach (Transaction transaction in Transactions)
            {
                if (FilterType.AfterDate == type && transaction.Date > dateTime)
                    listOfFilteredTransactions.Add(transaction);
                if (FilterType.BeforeDate == type && transaction.Date < dateTime)
                    listOfFilteredTransactions.Add(transaction);
            }

            return listOfFilteredTransactions;
        }

        public IEnumerable<Transaction> FilterBetweenDates(DateTime start, DateTime end)
        {
            List<Transaction> listOfTransactionsBetween = new();

            foreach (Transaction transaction in Transactions)
            {
                if (transaction.Date > start && transaction.Date < end)
                {
                    listOfTransactionsBetween.Add(transaction);
                }
            }

            return listOfTransactionsBetween;
        }

        //updates a single transaction with the given id
        //returns updated transaction if found
        //returns null if not found
        public Transaction UpdateTransaction(Transaction transaction)
        {
            foreach (Transaction tr in Transactions)
            {
                if (tr.Id == transaction.Id)
                {
                    tr.Name = transaction.Name;
                    tr.Type = transaction.Type;
                    tr.Amount = transaction.Amount;
                    tr.Date = transaction.Date;

                    UpdateFile();
                    return tr;
                }
            }

            return null;
        }

        public IEnumerable<Transaction> FilterByOneDate(DateTime date)
        {
            List<Transaction> listOfFilterByDate = new();
            foreach (Transaction transaction in Transactions)
            {
                if (transaction.Date.Date == date.Date)
                {
                    listOfFilterByDate.Add(transaction);
                }
            }
            return listOfFilterByDate;
        }

        public void DeleteTransactionsByDate(DateTime date)
        {
            foreach (Transaction transaction in Transactions.ToArray())
            {
                if (transaction.Date.Date == date.Date)
                {
                    Transactions.Remove(transaction);
                    UpdateFile();
                }
            }
        }

        public void DeleteTransactionById(Guid id)
        {
            foreach (Transaction transaction in Transactions.ToArray())
            {
                if (transaction.Id == id)
                {
                    Transactions.Remove(transaction);
                    UpdateFile();
                }
            }
        }

        public void DeleteTransactionById(IEnumerable<Guid> ids)
        {
            foreach (Transaction transaction in Transactions.ToArray())
            {
                if (ids.Contains(transaction.Id))
                {
                    Transactions.Remove(transaction);
                    UpdateFile();
                }
            }
        }


        /// <summary>
        ///  clears repository of all entities
        /// </summary>
        /// <param name="updateFile"> updateFile - true to change local file, false to not change it</param>

        public void WipeRepository(bool updateFile = true)
        {
            Transactions.Clear();
            if (updateFile)
            {
                UpdateFile();
            }
        }

        public Transaction GetTransactionById(Guid id)
        {
            return Transactions.FirstOrDefault(transaction => transaction.Id == id);
        }

        public void DeleteAllByType(TransactionType type)
        {
            foreach (Transaction transaction in Transactions.ToArray())
            {
                if (transaction.Type == type)
                {
                    Transactions.Remove(transaction);
                    UpdateFile();
                }
            }
        }

        public IEnumerable<Transaction> FilterTransactionValues(double cValue)
        {
            List<Transaction> listOfTransactionsBigger = new();

            foreach (Transaction transaction in Transactions)
            {
                if (cValue < transaction.Amount)
                {
                    listOfTransactionsBigger.Add(transaction);
                }
            }

            return listOfTransactionsBigger;
        }

        public IEnumerable<Transaction> FilterTransactionValueAndDate(DateTime dateTime, double amount)
        {
            List<Transaction> listOfTransactionsBiggerAndDate = new();

            foreach (Transaction transaction in Transactions)
            {
                if (amount < transaction.Amount && dateTime > transaction.Date)
                {
                    listOfTransactionsBiggerAndDate.Add(transaction);
                }
            }

            return listOfTransactionsBiggerAndDate;
        }

        public IEnumerable<Transaction> FilterTransactionTypes(TransactionType type)
        {
            List<Transaction> listOfTransactionType = new();

            foreach (Transaction transaction in Transactions)
            {
                if (transaction.Type == type)
                    listOfTransactionType.Add(transaction);
            }

            return listOfTransactionType;
        }

        public double ShowTypeAmount(TransactionType type)
        {
            double sumOfTransactions = 0;

            foreach (Transaction transaction in Transactions)
            {
                if (transaction.Type == type)
                    sumOfTransactions = transaction.Amount + sumOfTransactions;
            }
            return sumOfTransactions;
        }

        public IEnumerable<Transaction> AccountBallanceAtGivenTime(DateTime time)
        {
            List<Transaction> theTransactionsforedate = new();

            foreach (Transaction transaction in Transactions)
            {
                if (transaction.Date < time)
                    theTransactionsforedate.Add(transaction);
            }

            return theTransactionsforedate;
        }

        public IEnumerable<Transaction>  FilterByTypeAndAmount(TransactionType type)
        {
            IEnumerable<Transaction> listOfTypeByAmount = FilterTransactionTypes(type);

            IEnumerable<Transaction> list = listOfTypeByAmount.OrderBy(transaction => transaction.Amount);

            return list;
        }
    }
}

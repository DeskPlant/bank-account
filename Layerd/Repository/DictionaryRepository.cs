using Layerd.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Layerd.Repository
{
    public class DictionaryRepository : IRepository
    {
        public readonly string SourceFile = @"..\..\..\transactions.json";

        private Dictionary<Guid, Transaction> Transactions { get; } = new Dictionary<Guid, Transaction>();

        public DictionaryRepository(string sourceFile = @"..\..\..\transactions.json")
        {
            SourceFile = sourceFile;
            ReadAllFromFile();
        }

        public Transaction AddTransaction(Transaction transaction)
        {
            if (Transactions.ContainsKey(transaction.Id))
            {
                return null;
            }
            else
            {
                Transactions.Add(transaction.Id, transaction);
                UpdateFile();
                return Transactions[transaction.Id];
            }
        }

        public IEnumerable<Transaction> FilterBetweenDates(DateTime first, DateTime second)
        {
            List<Transaction> listOfTransactionsBetween = new();

            foreach (Transaction transaction in Transactions.Values)
            {
                if (transaction.Date > first && transaction.Date < second)
                {
                    listOfTransactionsBetween.Add(transaction);
                }
            }

            return listOfTransactionsBetween;
        }

        public IEnumerable<Transaction> FilterTransactionsByName(string transactionName)
        {
            List<Transaction> listOfFilteredTransactions = new();

            foreach (Transaction transaction in Transactions.Values)
            {
                if (transaction.Name == transactionName)
                {
                    listOfFilteredTransactions.Add(transaction);
                }
            }

            return listOfFilteredTransactions;
        }

        public IEnumerable<Transaction> FilterTransactionsByTypeAndDate(FilterType type, DateTime dateTime)
        {
            List<Transaction> listOfFilteredTransactions = new();

            foreach (Transaction transaction in Transactions.Values)
            {
                if (FilterType.AfterDate == type && transaction.Date > dateTime)
                    listOfFilteredTransactions.Add(transaction);

                if (FilterType.BeforeDate == type && transaction.Date < dateTime)
                    listOfFilteredTransactions.Add(transaction);
            }

            return listOfFilteredTransactions;
        }

        public IEnumerable<Transaction> GetAllTransactions()
        {
            return Transactions.Values;
        }

        public void ReadAllFromFile()
        {
            StreamReader streamReader = new(SourceFile);
            string jsonString = streamReader.ReadToEnd();

            // transforms a string into Transactions
            try
            {
                HashSet<Transaction> transactions = JsonConvert.DeserializeObject<HashSet<Transaction>>(jsonString);
                WipeRepository(false);
                foreach (Transaction transaction in transactions)
                {
                    Transactions.Add(transaction.Id, transaction);
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

        public void UpdateFile()
        {
            // transforms Transactions into a string
            string jsonString = JsonConvert.SerializeObject(Transactions.Values, Formatting.Indented);

            StreamWriter streamWriter = new(SourceFile);
            streamWriter.Write(jsonString);
            streamWriter.Close();
        }

        public Transaction UpdateTransaction(Transaction transaction)
        {
            if (Transactions.ContainsKey(transaction.Id))
            {
                Transactions[transaction.Id] = transaction;

                UpdateFile();

                return Transactions[transaction.Id];
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<Transaction> FilterTransactionsByDate(DateTime date)
        {
            List<Transaction> list = new();

            foreach (Transaction transaction in Transactions.Values)
            {
                if (transaction.Date.Date == date.Date)
                {
                    list.Add(transaction);
                }
            }

            return list;
        }

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
            return Transactions[id]; // the thing inside the [] is the key that we use to search the values inside the Transactions
        }

        public void DeleteTransactionById(Guid id)
        {
            Transactions.Remove(id);
            UpdateFile();
        }

        public void DeleteTransactionsById(IEnumerable<Guid> ids)
        {
            foreach (Guid id in ids)
            {
                Transactions.Remove(id);
            }
            UpdateFile();
        }

        public void DeleteAllByType(TransactionType type)
        {
            foreach (Transaction transaction in Transactions.Values)
            {
                if (transaction.Type == type)
                {
                    Transactions.Remove(transaction.Id);
                }
            }
            UpdateFile();
        }

        public IEnumerable<Transaction> FilterTransactionsByValueLessThan(double value)
        {
            List<Transaction> listOfTransactionsBigger = new();

            foreach (Transaction transaction in Transactions.Values)
            {
                if (value < transaction.Amount)
                {
                    listOfTransactionsBigger.Add(transaction);
                }
            }

            return listOfTransactionsBigger;
        }

        public IEnumerable<Transaction> FilterTransactionBeforDateAndBiggerThanAmount(DateTime dateTime, double amount)
        {
            List<Transaction> listOfTransactionsBiggerAndDate = new();

            foreach (Transaction transaction in Transactions.Values)
            {
                if (amount < transaction.Amount && dateTime > transaction.Date)
                {
                    listOfTransactionsBiggerAndDate.Add(transaction);
                }
            }

            return listOfTransactionsBiggerAndDate;
        }

        public IEnumerable<Transaction> FilterTransactionsByType(TransactionType type)
        {
            List<Transaction> listOfTransactionType = new();

            foreach (Transaction transaction in Transactions.Values)
            {
                if (transaction.Type == type)
                    listOfTransactionType.Add(transaction);
            }

            return listOfTransactionType;
        }

        IEnumerable<Transaction> IRepository.FilterTransactionsBeforeDate(DateTime time)
        {
            List<Transaction> theTransactionsforedate = new();

            foreach (Transaction transaction in Transactions.Values)
            {
                if (transaction.Date < time)
                    theTransactionsforedate.Add(transaction);
            }

            return theTransactionsforedate;
        }

        public IEnumerable<Transaction> FilterTransactionsByTypeAndOrderByAmount(TransactionType type)
        {
            IEnumerable<Transaction> listOfTypeByAmount = FilterTransactionsByType(type);

            IEnumerable<Transaction> list = listOfTypeByAmount.OrderBy(transaction => transaction.Amount);

            return list;
        }

        public void ChangeIOFile(string path)
        {
            
        }
    }
}

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
            List<Transaction> listOfTransactionsBetween = new List<Transaction>();

            foreach (Transaction transaction in Transactions.Values)
            {
                if (transaction.Date > first && transaction.Date < second)
                {
                    listOfTransactionsBetween.Add(transaction);
                }
            }

            return listOfTransactionsBetween;
        }

        public IEnumerable<Transaction> FilterByName(string transactionName)
        {
            List<Transaction> listOfFilteredTransactions = new List<Transaction>();

            foreach (Transaction transaction in Transactions.Values)
            {
                if (transaction.Name == transactionName)
                {
                    listOfFilteredTransactions.Add(transaction);
                }
            }

            return listOfFilteredTransactions;
        }

        public IEnumerable<Transaction> FilterWithDate(FilterType type, DateTime dateTime)
        {
            List<Transaction> listOfFilteredTransactions = new List<Transaction>();

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
            StreamReader streamReader = new StreamReader(SourceFile);
            string jsonString = streamReader.ReadToEnd();

            //transforms a string into Transactions
            try
            {
                HashSet<Transaction> transactions = JsonConvert.DeserializeObject<HashSet<Transaction>>(jsonString);
                WipeRepository();
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
            //transforms Transactions into a string
            string jsonString = JsonConvert.SerializeObject(Transactions.Values, Formatting.Indented);

            StreamWriter streamWriter = new StreamWriter(SourceFile);
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

        public void DeleteTransactionsByDate(DateTime date)
        {
            int count = 0;

            foreach (Transaction transaction in Transactions.Values.ToArray())
            {
                if (transaction.Date.Date == date.Date)
                {
                    Transactions.Remove(transaction.Id);
                    count++;
                    UpdateFile();
                }
            }


        }

        public IEnumerable<Transaction> FilterByOneDate(DateTime date)
        {
            throw new NotImplementedException();
        }

        public void WipeRepository()
        {
            Transactions.Clear();
            UpdateFile();
        }

        public Transaction GetTransactionById(Guid id)
        {
            return Transactions[id];
        }

        public void DeleteTransactionById(Guid id)
        {
            throw new NotImplementedException();
        }

        public void DeleteTransactionById(IEnumerable<Guid> ids)
        {
            throw new NotImplementedException();
        }
    }
}

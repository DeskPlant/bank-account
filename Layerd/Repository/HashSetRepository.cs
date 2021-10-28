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

        private HashSet<Transaction> Transactions { get; set; } = new HashSet<Transaction>();

        public HashSetRepository()
        {
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
            StreamWriter streamWriter = new StreamWriter(SourceFile);
            streamWriter.Write(jsonString);
            streamWriter.Close();
        }

        public void ReadAllFromFile()
        {
            StreamReader streamReader = new StreamReader(SourceFile);
            string jsonString = streamReader.ReadToEnd();

            //transforms a string into Transactions
            try
            {
                Transactions = JsonConvert.DeserializeObject<HashSet<Transaction>>(jsonString);
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
            List<Transaction> listOfFilteredTransactions = new List<Transaction>();

            foreach (Transaction transaction in Transactions)
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

            foreach (Transaction transaction in Transactions)
            {
                if (FilterType.AfterDate == type && transaction.Date > dateTime)
                    listOfFilteredTransactions.Add(transaction);
                if (FilterType.BeforeDate == type && transaction.Date < dateTime)
                    listOfFilteredTransactions.Add(transaction);
            }

            return listOfFilteredTransactions;
        }

        public IEnumerable<Transaction> FilterBetweenDates(DateTime first, DateTime second)
        {
            List<Transaction> listOfTransactionsBetween = new List<Transaction>();

            foreach (Transaction transaction in Transactions)
            {
                if (transaction.Date > first && transaction.Date < second)
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
            List<Transaction> listOfFilterByDate = new List<Transaction>();
            foreach  (Transaction transaction in Transactions)
            {
                if (transaction.Date.Date == date.Date)
                {
                    listOfFilterByDate.Add(transaction);
                }
            }
                return listOfFilterByDate;
        }

        public void DeleteTransaction(DateTime date)
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
    }
}

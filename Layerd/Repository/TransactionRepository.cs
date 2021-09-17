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
    public class TransactionRepository : IRepository
    {
        public readonly string SourceFile = @"C:\Users\patri\source\repos\Layerd\Layerd\transactions.json";

        private List<Transaction> Transactions { get; set; } = new List<Transaction>();

        public TransactionRepository()
        {
            ReadAllFromFile();
        }

        public void AddTransaction(Transaction transaction)
        {
            Transactions.Add(transaction);
            UpdateFile();
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
                Transactions = JsonConvert.DeserializeObject<List<Transaction>>(jsonString);
            }
            catch (Exception exception)
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
            return Transactions.Where(t => t.Name == transactionName);
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
        public Transaction UpdateTransaction(Guid transactionId, Transaction updateData)
        {
            foreach (Transaction transaction in Transactions)
            {
                if (transaction.Id == transactionId)
                {
                    transaction.Name = updateData.Name;
                    transaction.Type = updateData.Type;
                    transaction.Amount = updateData.Amount;
                    transaction.Date = updateData.Date;

                    return transaction;
                }
            }

            return null;
        }
    }
}

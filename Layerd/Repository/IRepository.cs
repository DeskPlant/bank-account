using System;
using System.Collections.Generic;
using Layerd.Domain;

namespace Layerd.Repository
{
    //crud operations
    //create    add new stuff to file
    //read      read stuff from file
    //update    update/change stuff from file
    //delete    delete some stuff from file
    public interface IRepository
    {
        public Transaction AddTransaction(Transaction transaction);

        public IEnumerable<Transaction> GetAllTransactions();

        public void UpdateFile();

        public void ReadAllFromFile();

        public IEnumerable<Transaction> FilterByName(string transactionName);

        public IEnumerable<Transaction> FilterWithDate(FilterType type, DateTime dateTime);

        public IEnumerable<Transaction> FilterBetweenDates(DateTime first, DateTime second);

        public Transaction UpdateTransaction(Transaction transaction);
    }
}

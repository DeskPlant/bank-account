using Layerd.Domain;
using System;
using System.Collections.Generic;

namespace Layerd.Service
{
    public interface IService
    {
        public void UpdateFile();

        public Transaction AddTransaction(Transaction transaction);

        public IEnumerable<Transaction> GetAllTransactions();

        public IEnumerable<Transaction> FilterByName(string transactionName);

        public IEnumerable<Transaction> FilterWithDate(FilterType type, DateTime dateTime);

        public IEnumerable<Transaction> FilterBetweenDates(DateTime first, DateTime second);

        public Transaction UpdateTransaction(Transaction transaction);

        public void DeleteTransaction(DateTime date);

        public IEnumerable<Transaction> FilterByOneDate(DateTime date);
    }

}

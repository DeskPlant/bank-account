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

        public void DeleteTransactions(DateTime date);

        public IEnumerable<Transaction> FilterByOneDate(DateTime date);

        public void DeleteTransactionId(Guid id);

        public void DeleteTransactionById(IEnumerable<Guid> ids);

        public void DeleteAllByType(TransactionType type);

        public IEnumerable<Transaction> FilterTransactionValues(double cValue);

        public IEnumerable<Transaction> FilterTransactionValueAndDate(DateTime dateTime, double amount);

        public IEnumerable<Transaction> FilterTransactionTypes(TransactionType type);

        public double ShowTypeAmount(TransactionType type);

        public double AccountBallanceAtGivenTime(DateTime time);
 
    }

}

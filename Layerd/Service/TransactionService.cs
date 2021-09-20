using Layerd.Repository;
using Layerd.Domain;
using System.Collections.Generic;
using System;

namespace Layerd.Service
{
    public class TransactionService : IService
    {
        public IRepository Repository { get; set; }

        public TransactionService(IRepository repository)
        {
            Repository = repository;
        }

        public void AddTransaction(Transaction transaction)
        {
            Repository.AddTransaction(transaction);
        }

        public IEnumerable<Transaction> GetAllTransactions()
        {
            return Repository.GetAllTransactions();
        }

        public IEnumerable<Transaction> FilterByName(string transactionName)
        {
            return Repository.FilterByName(transactionName);
        }


        public IEnumerable<Transaction> FilterBetweenDates(DateTime first, DateTime second)
        {
            return Repository.FilterBetweenDates(first, second);
        }


        public IEnumerable<Transaction> FilterWithDate(FilterType type, DateTime dateTime)
        {
            return Repository.FilterWithDate(type, dateTime);
        }

        public Transaction UpdateTransaction(Transaction transaction)
        {
            return Repository.UpdateTransaction(transaction);
        }
    }
}

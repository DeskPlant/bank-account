﻿using Layerd.Repository;
using Layerd.Domain;
using System.Collections.Generic;
using System;

namespace Layerd.Service
{
    public class TransactionService : IService
    {
        public IRepository Repository { get; set; }

        public void UpdateFile()
        {
            Repository.UpdateFile();
        }

        public TransactionService(IRepository repository)
        {
            Repository = repository;
        }

        public Transaction AddTransaction(Transaction transaction)
        {
            return Repository.AddTransaction(transaction);
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

        public void DeleteTransactions(DateTime date)
        {
            Repository.DeleteTransaction(date);
        }

        public IEnumerable<Transaction> FilterByOneDate(DateTime date)
        {
            return Repository.FilterByOneDate(date);
        }

        public void DeleteTransactionId(Guid id)
        {
            Repository.DeleteTransactionById(id);
        }

        public void DeleteTransactionById(IEnumerable<Guid> ids)
        {
            Repository.DeleteTransactionById(ids);
        }
    }
}

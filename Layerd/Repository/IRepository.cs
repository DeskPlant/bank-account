﻿using System;
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
        public void WipeRepository(bool updateFile);

        public Transaction AddTransaction(Transaction transaction);

        public Transaction GetTransactionById(Guid id);
        
        public IEnumerable<Transaction> GetAllTransactions();

        public void UpdateFile();

        public void ReadAllFromFile();

        public IEnumerable<Transaction> FilterByName(string transactionName);

        public IEnumerable<Transaction> FilterWithDate(FilterType type, DateTime dateTime);

        public IEnumerable<Transaction> FilterBetweenDates(DateTime first, DateTime second);

        public Transaction UpdateTransaction(Transaction transaction);

        public void DeleteTransactionsByDate(DateTime date);

        public IEnumerable<Transaction> FilterByOneDate(DateTime date);

        public void DeleteTransactionById(Guid id);

        public void DeleteTransactionById(IEnumerable<Guid> ids);

        public void DeleteAllByType(TransactionType type);

        public IEnumerable<Transaction> FilterTransactionValues(double cValue);

        public IEnumerable<Transaction> FilterTransactionValueAndDate(DateTime dateTime, double amount);

        public IEnumerable<Transaction> FilterTransactionTypes(TransactionType type);

        public double ShowTypeAmount(TransactionType type);

        public IEnumerable<Transaction> AccountBallanceAtGivenTime(DateTime time);

        public IEnumerable<Transaction>  FilterByTypeAndAmount(TransactionType type);
    }
}

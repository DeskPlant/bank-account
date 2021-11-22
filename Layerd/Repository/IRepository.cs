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
		public void WipeRepository(bool updateFile);

		public Transaction AddTransaction(Transaction transaction);

		public Transaction GetTransactionById(Guid id);

		public IEnumerable<Transaction> GetAllTransactions();

		public void UpdateFile();

		public void ReadAllFromFile();

		public IEnumerable<Transaction> FilterTransactionsByName(string transactionName);

		public IEnumerable<Transaction> FilterTransactionsByTypeAndDate(FilterType type, DateTime dateTime);

		public IEnumerable<Transaction> FilterBetweenDates(DateTime first, DateTime second);

		public Transaction UpdateTransaction(Transaction transaction);

		public IEnumerable<Transaction> FilterTransactionsByDate(DateTime date);

		public void DeleteTransactionById(Guid id);

		public void DeleteTransactionsById(IEnumerable<Guid> ids);

		public void DeleteAllByType(TransactionType type);

		public IEnumerable<Transaction> FilterTransactionsByValueLessThan(double value);

		public IEnumerable<Transaction> FilterTransactionBeforDateAndBiggerThanAmount(DateTime dateTime, double amount);

		public IEnumerable<Transaction> FilterTransactionsByType(TransactionType type);

		public IEnumerable<Transaction> FilterTransactionsBeforeDate(DateTime time);

		public IEnumerable<Transaction> FilterTransactionsByTypeAndOrderByAmount(TransactionType type);
	}
}

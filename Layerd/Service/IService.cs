using Layerd.Domain;
using System;
using System.Collections.Generic;

namespace Layerd.Service
{
	public interface IService
	{
		public void ChangeIOFile(string path);

		public void UpdateFile();

		public Transaction AddTransaction(Transaction transaction);

		public IEnumerable<Transaction> GetAllTransactions();

		public IEnumerable<Transaction> FilterTransactionsByName(string transactionName);

		public IEnumerable<Transaction> FilterTransactionsByTypeAndDate(FilterType type, DateTime dateTime);

		public IEnumerable<Transaction> FilterTransactionsBetweenDates(DateTime first, DateTime second);

		public Transaction UpdateTransaction(Transaction transaction);

		public void DeleteTransactionsByDate(DateTime date);

		public IEnumerable<Transaction> FilterTransactionsByDate(DateTime date);

		public void DeleteTransactionById(Guid id);

		public void DeleteTransactionsById(IEnumerable<Guid> ids);

		public void DeleteTransactionsByType(TransactionType type);

		public IEnumerable<Transaction> FilterTransactionsByValueLessThan(double cValue);

		public IEnumerable<Transaction> FilterTransactionsByValueAndDate(DateTime dateTime, double amount);

		public IEnumerable<Transaction> FilterTransactionsByType(TransactionType type);

		public double ShowTypeAmount(TransactionType type);

		public double AccountBallanceAtGivenTime(DateTime time);

		public IEnumerable<Transaction> FilterTransactionsByTypeAndOrderByAmount(TransactionType type);
	}
}

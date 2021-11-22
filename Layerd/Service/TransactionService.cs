using Layerd.Repository;
using Layerd.Domain;
using System.Collections.Generic;
using System;
using System.Linq;

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

		public IEnumerable<Transaction> FilterTransactionsByName(string transactionName)
		{
			return Repository.FilterTransactionsByName(transactionName);
		}

		public IEnumerable<Transaction> FilterTransactionsBetweenDates(DateTime first, DateTime second)
		{
			return Repository.FilterBetweenDates(first, second);
		}

		public IEnumerable<Transaction> FilterTransactionsByTypeAndDate(FilterType type, DateTime dateTime)
		{
			return Repository.FilterTransactionsByTypeAndDate(type, dateTime);
		}

		public Transaction UpdateTransaction(Transaction transaction)
		{
			return Repository.UpdateTransaction(transaction);
		}

		public void DeleteTransactionsByDate(DateTime date)
		{
			IEnumerable<Transaction> transactions = Repository.FilterTransactionsByDate(date);

			IEnumerable<Guid> ids = transactions.Select(tr => tr.Id);

			Repository.DeleteTransactionsById(ids);
		}

		public IEnumerable<Transaction> FilterTransactionsByDate(DateTime date)
		{
			return Repository.FilterTransactionsByDate(date);
		}

		public void DeleteTransactionById(Guid id)
		{
			Repository.DeleteTransactionById(id);
		}

		public void DeleteTransactionsById(IEnumerable<Guid> ids)
		{
			Repository.DeleteTransactionsById(ids);
		}

		public void DeleteTransactionsByType(TransactionType type)
		{
			IEnumerable<Transaction> transactions = Repository.FilterTransactionsByType(type);

			IEnumerable<Guid> ids = transactions.Select(tr => tr.Id);

			Repository.DeleteTransactionsById(ids);
		}

		public IEnumerable<Transaction> FilterTransactionsByValueLessThan(double value)
		{
			return Repository.FilterTransactionsByValueLessThan(value);
		}

		public IEnumerable<Transaction> FilterTransactionsByValueAndDate(DateTime dateTime, double amount)
		{
			return Repository.FilterTransactionBeforDateAndBiggerThanAmount(dateTime, amount);
		}

		public IEnumerable<Transaction> FilterTransactionsByType(TransactionType type)
		{
			return Repository.FilterTransactionsByType(type);
		}

		public double ShowTypeAmount(TransactionType type)
		{
			double sumOfTransactions = 0;

			var transactions = Repository.FilterTransactionsByType(type);

			foreach (Transaction transaction in transactions)
			{
				if (transaction.Type == type)
					sumOfTransactions = transaction.Amount + sumOfTransactions;
			}

			return sumOfTransactions;
		}

		public double AccountBallanceAtGivenTime(DateTime dateTime)
		{
			IEnumerable<Transaction> list = Repository.FilterTransactionsBeforeDate(dateTime);

			double ballance = 0;

			foreach (Transaction transaction in list)
			{
				switch (transaction.Type)
				{
					case TransactionType.Incoming:
						ballance += transaction.Amount;
						break;
					case TransactionType.Outgoing:
						ballance -= transaction.Amount;
						break;
				}
			}

			return ballance;
		}

		public IEnumerable<Transaction> FilterTransactionsByTypeAndOrderByAmount(TransactionType type)
		{
			return Repository.FilterTransactionsByTypeAndOrderByAmount(type);
		}
	}
}

using Layerd.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Layerd.Repository
{
	public class DictionaryRepository : IRepository
	{
		public readonly string SourceFile = @"..\..\..\transactions.json";

		private Dictionary<Guid, Transaction> Transactions { get; } = new Dictionary<Guid, Transaction>();

		public DictionaryRepository(string sourceFile = @"..\..\..\transactions.json")
		{
			SourceFile = sourceFile;
			ReadAllFromFile();
		}

		public Transaction AddTransaction(Transaction transaction)
		{
			if (Transactions.ContainsKey(transaction.Id))
			{
				return null;
			}
			else
			{
				Transactions.Add(transaction.Id, transaction);
				UpdateFile();
				return Transactions[transaction.Id];
			}
		}

		public IEnumerable<Transaction> FilterBetweenDates(DateTime first, DateTime second)
		{
			List<Transaction> listOfTransactionsBetween = new();

			foreach (Transaction transaction in Transactions.Values)
			{
				if (transaction.Date > first && transaction.Date < second)
				{
					listOfTransactionsBetween.Add(transaction);
				}
			}

			return listOfTransactionsBetween;
		}

		public IEnumerable<Transaction> FilterTransactionsByName(string transactionName)
		{
			List<Transaction> listOfFilteredTransactions = new();

			foreach (Transaction transaction in Transactions.Values)
			{
				if (transaction.Name == transactionName)
				{
					listOfFilteredTransactions.Add(transaction);
				}
			}

			return listOfFilteredTransactions;
		}

		public IEnumerable<Transaction> FilterTransactionsByTypeAndDate(FilterType type, DateTime dateTime)
		{
			List<Transaction> listOfFilteredTransactions = new();

			foreach (Transaction transaction in Transactions.Values)
			{
				if (FilterType.AfterDate == type && transaction.Date > dateTime)
					listOfFilteredTransactions.Add(transaction);

				if (FilterType.BeforeDate == type && transaction.Date < dateTime)
					listOfFilteredTransactions.Add(transaction);
			}

			return listOfFilteredTransactions;
		}

		public IEnumerable<Transaction> GetAllTransactions()
		{
			return Transactions.Values;
		}

		public void ReadAllFromFile()
		{
			StreamReader streamReader = new(SourceFile);
			string jsonString = streamReader.ReadToEnd();

			// transforms a string into Transactions
			try
			{
				HashSet<Transaction> transactions = JsonConvert.DeserializeObject<HashSet<Transaction>>(jsonString);
				WipeRepository();
				foreach (Transaction transaction in transactions)
				{
					Transactions.Add(transaction.Id, transaction);
				}
			}
			catch (Exception)
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

		public void UpdateFile()
		{
			// transforms Transactions into a string
			string jsonString = JsonConvert.SerializeObject(Transactions.Values, Formatting.Indented);

			StreamWriter streamWriter = new(SourceFile);
			streamWriter.Write(jsonString);
			streamWriter.Close();
		}

		public Transaction UpdateTransaction(Transaction transaction)
		{
			if (Transactions.ContainsKey(transaction.Id))
			{
				Transactions[transaction.Id] = transaction;

				UpdateFile();

				return Transactions[transaction.Id];
			}
			else
			{
				return null;
			}
		}

		public IEnumerable<Transaction> FilterTransactionsByDate(DateTime date)
		{
			throw new NotImplementedException();
		}

		public void WipeRepository(bool updateFile = true)
		{
			Transactions.Clear();
			if (updateFile)
			{
				UpdateFile();
			}
		}

		public Transaction GetTransactionById(Guid id)
		{
#warning Missing method
			return Transactions[id];
		}

		public void DeleteTransactionById(Guid id)
		{
#warning Missing method
			throw new NotImplementedException();
		}

		public void DeleteTransactionsById(IEnumerable<Guid> ids)
		{
#warning Missing method
			throw new NotImplementedException();
		}

		public void DeleteAllByType(TransactionType type)
		{
#warning Missing method
			throw new NotImplementedException();
		}

		public IEnumerable<Transaction> FilterTransactionsByValueLessThan(double cValue)
		{
#warning Missing method
			throw new NotImplementedException();
		}

		public IEnumerable<Transaction> FilterTransactionBeforDateAndBiggerThanAmount(DateTime dateTime, double amount)
		{
#warning Missing method
			throw new NotImplementedException();
		}

		public IEnumerable<Transaction> FilterTransactionsByType(TransactionType type)
		{
#warning Missing method
			throw new NotImplementedException();
		}

		public double ShowTypeAmount(TransactionType type)
		{
#warning Missing method
			throw new NotImplementedException();
		}

		public double AccountBallanceAtGivenTime(DateTime time)
		{
#warning Missing method
			throw new NotImplementedException();
		}

		IEnumerable<Transaction> IRepository.FilterTransactionsBeforeDate(DateTime time)
		{
#warning Missing method
			throw new NotImplementedException();
		}

		public IEnumerable<Transaction> FilterTransactionsByTypeAndOrderByAmount(TransactionType type)
		{
#warning Missing method
			throw new NotImplementedException();
		}
	}
}

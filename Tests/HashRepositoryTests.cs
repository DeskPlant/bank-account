using Layerd.Domain;
using Layerd.Repository;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tests
{
    public class HashRepositoryTests
    {
        private HashSetRepository hashSetRepository = null;

        private static List<Transaction> GetTransactions()
        {
            return new()
            {
                new Transaction
                {
                    Date = new DateTime(1999, 2, 21, 15, 33, 2),
                    Name = "tra",
                    Amount = 120,
                    Type = TransactionType.Incoming
                },
                new Transaction
                {
                    Date = new DateTime(2012, 7, 1, 5, 33, 2),
                    Name = "tra2",
                    Amount = 4000,
                    Type = TransactionType.Incoming
                },
                new Transaction
                {
                    Date = new DateTime(2021, 10, 29, 20, 6, 3),
                    Name = "tra3",
                    Amount = 1,
                    Type = TransactionType.Incoming
                }
            };
        }

        [SetUp]
        public void Setup()
        {
            hashSetRepository = new HashSetRepository();
        }

        [TearDown]
        public void TearDown()
        {
            hashSetRepository.WipeRepository();
        }

        [Test]
        public void AddOneTransaction_Succeeds()
        {
            Transaction transaction = new()
            {
                Date = DateTime.Now,
                Name = "t1",
                Amount = 100,
                Type = TransactionType.Incoming
            };

            hashSetRepository.AddTransaction(transaction);

            var repoTransaction = hashSetRepository.GetTransactionById(transaction.Id);

            Assert.AreEqual(transaction.Id, repoTransaction.Id);
            Assert.AreEqual(transaction.Date, repoTransaction.Date);
            Assert.AreEqual(transaction.Name, repoTransaction.Name);
            Assert.AreEqual(transaction.Amount, repoTransaction.Amount);
            Assert.AreEqual(transaction.Type, repoTransaction.Type);
        }

        [Test]
        public void AddOneTransaction_Fails()
        {
            Transaction transaction = new()
            {
                Date = DateTime.Now,
                Name = "t1",
                Amount = 100,
                Type = TransactionType.Incoming
            };

            Assert.AreEqual(transaction, hashSetRepository.AddTransaction(transaction));

            Assert.IsNull(hashSetRepository.AddTransaction(transaction));
        }

        [Test]
        public void GetAllTransactions_Succeeds()
        {
            Assert.IsEmpty(hashSetRepository.GetAllTransactions());
        }

        [Test]
        public void AddMultipleTransactions_Succeeds()
        {
            List<Transaction> transactions = GetTransactions();

            foreach (Transaction transaction in transactions)
            {
                hashSetRepository.AddTransaction(transaction);
            }

            Assert.AreEqual(transactions.Count, hashSetRepository.GetAllTransactions().Count());
        }

        [Test]
        public void FilterByName_Succeeds()
        {
            List<Transaction> transactions = GetTransactions();

            foreach (Transaction transaction in transactions)
            {
                hashSetRepository.AddTransaction(transaction);
            }

            Transaction _transaction = new()
            {
                Date = DateTime.Now,
                Name = "tr",
                Amount = 120,
                Type = TransactionType.Incoming
            };

            hashSetRepository.AddTransaction(_transaction);

            Assert.AreEqual(1, hashSetRepository.FilterByName(_transaction.Name).Count());
        }

        [Test]
        public void FilterWithDate_Before_Succeeds()
        {
            List<Transaction> transactions = GetTransactions();

            foreach (Transaction transaction in transactions)
            {
                hashSetRepository.AddTransaction(transaction);
            }

            Assert.AreEqual(2, hashSetRepository.FilterByTypeAndDate(FilterType.BeforeDate, new DateTime(2015, 4, 22)).Count());
        }

        [Test]
        public void FilterWithDate_After_Succeeds()
        {
            List<Transaction> transactions = GetTransactions();

            foreach (Transaction transaction in transactions)
            {
                hashSetRepository.AddTransaction(transaction);
            }

            Assert.AreEqual(1, hashSetRepository.FilterByTypeAndDate(FilterType.AfterDate, new DateTime(2015, 4, 22)).Count());
        }

        [Test]
        public void FilterBetweenDates_Succeeds()
        {
            List<Transaction> transactions = GetTransactions();

            foreach (Transaction transaction in transactions)
            {
                hashSetRepository.AddTransaction(transaction);
            }

            Assert.AreEqual(2, hashSetRepository.FilterBetweenDates(new DateTime(1998, 1, 1), new DateTime(2013, 4, 22)).Count());
        }

        [Test]
        public void UpdateTransaction_Succeeds()
        {
            List<Transaction> transactions = GetTransactions();

            foreach (Transaction transaction in transactions)
            {
                hashSetRepository.AddTransaction(transaction);
            }

            Transaction _transaction = new()
            {
                Id = transactions[0].Id,
                Date = DateTime.Now,
                Name = "new name",
                Amount = 999,
                Type = TransactionType.Outgoing
            };

            Assert.IsNotNull(hashSetRepository.UpdateTransaction(_transaction));

            Transaction repoTransaction = hashSetRepository.GetTransactionById(_transaction.Id);

            Assert.AreEqual(_transaction.Id, repoTransaction.Id);
            Assert.AreEqual(_transaction.Date, repoTransaction.Date);
            Assert.AreEqual(_transaction.Name, repoTransaction.Name);
            Assert.AreEqual(_transaction.Amount, repoTransaction.Amount);
            Assert.AreEqual(_transaction.Type, repoTransaction.Type);
        }

        [Test]
        public void UpdateTransaction_Fails()
        {
            List<Transaction> transactions = GetTransactions();

            foreach (Transaction transaction in transactions)
            {
                hashSetRepository.AddTransaction(transaction);
            }

            Transaction _transaction = new()
            {
                Id = Guid.NewGuid(),
                Date = DateTime.Now,
                Name = "new name",
                Amount = 999,
                Type = TransactionType.Outgoing
            };

            Assert.IsNull(hashSetRepository.UpdateTransaction(_transaction));
        }

        [Test]
        public void FilterByOneDate_Succeeds()
        {
            List<Transaction> transactions = GetTransactions();

            foreach (Transaction transaction in transactions)
            {
                hashSetRepository.AddTransaction(transaction);
            }

            Assert.AreEqual(1, hashSetRepository.FilterByDate(transactions[0].Date).Count());
        }

        [Test]
        public void FilterByOneDate_Fails()
        {
            List<Transaction> transactions = GetTransactions();

            foreach (Transaction transaction in transactions)
            {
                hashSetRepository.AddTransaction(transaction);
            }

            Assert.IsEmpty(hashSetRepository.FilterByDate(new DateTime(2020, 2, 21)));
        }

        [Test]
        public void DeleteTransactions_Succeeds()
        {
            List<Transaction> transactions = GetTransactions();

            foreach (Transaction transaction in transactions)
            {
                hashSetRepository.AddTransaction(transaction);
            }

            int previousCount = hashSetRepository.GetAllTransactions().Count();
            hashSetRepository.DeleteTransactionsByDate(transactions[0].Date);
            Assert.AreEqual(previousCount - 1, hashSetRepository.GetAllTransactions().Count());
        }

        [Test]
        public void DeleteTransactions_Fails()
        {
            List<Transaction> transactions = GetTransactions();

            foreach (Transaction transaction in transactions)
            {
                hashSetRepository.AddTransaction(transaction);
            }

            int previousCount = hashSetRepository.GetAllTransactions().Count();
            hashSetRepository.DeleteTransactionsByDate(new DateTime(2012, 7, 20));
            Assert.AreEqual(previousCount, hashSetRepository.GetAllTransactions().Count());
        }
    }
}
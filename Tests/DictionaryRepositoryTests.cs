using Layerd.Domain;
using Layerd.Repository;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tests
{
    public class DictionaryRepositoryTests
    {
        private DictionaryRepository hashSetRepository = null;

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
            hashSetRepository = new DictionaryRepository();
        }

        [TearDown]
        public void TearDown()
        {
            hashSetRepository.WipeRepository();
        }
    }
}
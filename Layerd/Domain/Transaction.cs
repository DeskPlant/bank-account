using System;

namespace Layerd.Domain
{
	public class Transaction
	{
		public Guid Id { get; set; } = Guid.NewGuid();

		public DateTime Date { get; set; }

		public string Name { get; set; }

		public double Amount { get; set; }

		public TransactionType Type { get; set; }

		public override bool Equals(object obj)
		{
			return obj is Transaction transaction && Id.Equals(transaction.Id);
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(Id);
		}

		public override string ToString()
		{
			return Id.ToString() + " " + Date.ToString() + " " + Name + " " + Amount + " " + Type.ToString();
		}
	}
}

namespace Layerd.UI
{
	public interface IUI
	{
		public void UpdateFile();

		public void DisplayMenu();

		public void DisplayFilters();

		public void DisplayDeletes();

		public int ReadCommand();

		public void AddTransaction();

		public void DisplayAllTransactions();

		public void FilterTransactionsByName();

		public void FilterTransactionsByDate();

		public void FilterTransactionsBetweenDates();

		public void UpdateTransaction();

		public void DeleteTransactionsByDate();

		public void DeleteTransactionsBetweenDates();

		public void DeleteTransactionsByType();

		public void FilterTransactionsByValueLessThan();

		public void FilterTransactionsByValueAndDate();

		public void FilterTransactionsByType();

		public void ShowTypeAmount();

		public void AccountBallanceAtGivenTime();

		public void FilterTransactionsByTypeAndOrderByAmount();
	}
}

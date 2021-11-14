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

        public void FilterByName();

        public void FilterByDate();

        public void FilterBetweenDates();

        public void UpdateTransaction();

        public void DeleteThroughDate();

        public void DeleteTransactionsBetweenDates();

        public void DeleteTransactionType();

        public void FilterTransactionValues();

        public void FilterTransactionValueAndDate();

        public void FilterTransactionTypes();

        public void ShowTypeAmount();
    }
}

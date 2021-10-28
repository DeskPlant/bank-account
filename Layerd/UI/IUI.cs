namespace Layerd.UI
{
    public interface IUI
    {
        public void DisplayMenu();

        public int ReadCommand();

        public void AddTransaction();

        public void DisplayAllTransactions();

        public void FilterByName();

        public void FilterByDate();

        public void FilterBetweenDates();

        public void UpdateTransaction();

        public void DeleteTransaction();

        public void FilterByOneDate();
    }
}

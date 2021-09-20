using Layerd.Repository;
using Layerd.Service;
using Layerd.UI;

namespace Layerd
{
    public static class Program
    {
        public static void Main()
        {
            IRepository repository = new DictionaryRepository();
            IService service = new TransactionService(repository);
            IUI ui = new TransactionUI(service);
            BankAccounts bankAccount = new BankAccounts(ui);
            bankAccount.Run();
        }
    }
}

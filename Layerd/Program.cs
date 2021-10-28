using Layerd.Repository;
using Layerd.Service;
using Layerd.UI;
using System.Collections.Generic;

namespace Layerd
{
    public static class Program
    {
        public static void Main()
        {
            IRepository repository = new HashSetRepository();
            IService service = new TransactionService(repository);
            IUI ui = new TransactionUI(service);
            BankAccounts bankAccount = new BankAccounts(ui);
            bankAccount.Run();
        }
    }
}

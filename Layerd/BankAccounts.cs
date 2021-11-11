using Layerd.UI;
using System;

namespace Layerd
{
    public class BankAccounts
    {
        private IUI UI { get; set; }

        private bool Running { get; set; }

        public BankAccounts(IUI UI)
        {
            this.UI = UI;
            this.Running = true;
        }

        public void Run()
        {
            UI.DisplayMenu();
            while (Running)
            {
                try
                {
                    int command = UI.ReadCommand();
                    switch (command)
                    {
                        case 0:
                            {
                                Running = false;
                                UI.UpdateFile();
                                break;
                            }
                        case 1:
                            {
                                UI.DisplayMenu();
                                Console.WriteLine();
                                break;
                            }
                        case 2:
                            {
                                UI.AddTransaction();
                                Console.WriteLine();
                                break;
                            }
                        case 3:
                            {
                                UI.DisplayAllTransactions();
                                Console.WriteLine();
                                break;
                            }
                        case 4:
                            {
                                UI.FilterByName();
                                Console.WriteLine();
                                break;
                            }
                        case 5:
                            {
                                UI.FilterByDate();
                                Console.WriteLine();
                                break;
                            }
                        case 6:
                            {
                                UI.FilterBetweenDates();
                                Console.WriteLine();
                                break;
                            }
                        case 7:
                            {
                                UI.UpdateTransaction();
                                Console.WriteLine();
                                break;
                            }
                        case 8:
                            {
                                UI.DeleteThroughDate();
                                
                                break;
                            }
                        case 9:
                            {
                                UI.DeleteTransactionsBetweenDates();
                                break;
                            }
                        case 10:
                            {
                                UI.DeleteTransactionType();
                                break;
                            }
                        default:
                            {
                                Console.WriteLine("No such command.");
                                Console.WriteLine();
                                break;
                            }
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine($"Exception caught: {exception.Message}");
                    Console.WriteLine(exception.StackTrace);
                }
            }
        }
    }
}


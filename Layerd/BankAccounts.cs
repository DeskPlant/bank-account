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
                                break;
                            }
                        case 1:
                            {
                                UI.DisplayMenu();
                                break;
                            }
                        case 2:
                            {
                                UI.AddTransaction();
                                break;
                            }
                        case 3:
                            {
                                UI.DisplayAllTransactions();
                                break;
                            }
                        case 4:
                            {
                                UI.FilterByName();
                                break;
                            }
                        case 5:
                            {
                                UI.FilterByDate();
                                break;
                            }
                        case 6:
                            {
                                UI.FilterBetweenDates();
                                break;
                            }
                        case 7:
                            {
                                UI.UpdateTransaction();
                                break;
                            }
                        default:
                            {
                                Console.WriteLine("No such command.");
                                break;
                            }
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine($"Exception caught: {exception.Message}");
                }
            }
        }
    }
}


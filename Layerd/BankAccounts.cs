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
                                UI.DisplayMenu();
                                break;
                            }
                        case 3:
                            {
                                UI.UpdateTransaction();
                                Console.WriteLine();
                                UI.DisplayMenu();
                                break;
                            }
                        case 4:
                            {
                                UI.DisplayFilters();
                                int command2 = UI.ReadCommand();
                                switch (command2)
                                {
                                    case 0:
                                        {
                                            UI.DisplayMenu();
                                            break;
                                        }
                                    case 1:
                                        {
                                            UI.FilterByName();
                                            Console.WriteLine();
                                            UI.DisplayMenu();
                                            break;
                                        }
                                    case 2:
                                        {
                                            UI.FilterByDate();
                                            Console.WriteLine();
                                            UI.DisplayMenu();
                                            break;
                                        }
                                    case 3:
                                        {
                                            UI.FilterBetweenDates();
                                            Console.WriteLine();
                                            UI.DisplayMenu();
                                            break;
                                        }
                                    case 4:
                                        {
                                            UI.FilterTransactionValues();
                                            Console.WriteLine();
                                            UI.DisplayMenu();
                                            break;
                                        }
                                    case 5:
                                        {
                                            UI.FilterTransactionValueAndDate();
                                            Console.WriteLine();
                                            UI.DisplayMenu();
                                            break;
                                        }
                                    case 6:
                                        {
                                            UI.FilterTransactionTypes();
                                            Console.WriteLine();
                                            UI.DisplayMenu();
                                            break;
                                        }
                                    case 7:
                                        {
                                            UI.DisplayAllTransactions();
                                            Console.WriteLine();
                                            UI.DisplayMenu();
                                            break;
                                        }
                                    case 8:
                                        {
                                            UI. FilterByTypeAndAmount();
                                            Console.WriteLine();
                                            UI.DisplayMenu();
                                            break;
                                        }
                                    default:
                                        {
                                            Console.WriteLine("No such command.");
                                            Console.WriteLine();
                                            UI.DisplayMenu();
                                            break;
                                        }

                                }
                                break;
                            }
                        case 5:
                            {
                                UI.DisplayDeletes();
                                int command3 = UI.ReadCommand();
                                switch (command3)
                                {
                                    case 0:
                                        {
                                            UI.DisplayMenu();
                                            Console.WriteLine();
                                            UI.DisplayMenu();
                                            break;
                                        }
                                    case 1:
                                        {
                                            UI.DeleteThroughDate();
                                            Console.WriteLine();
                                            UI.DisplayMenu();
                                            break;       
                                        }
                                    case 2:
                                        {
                                            UI.DeleteTransactionsBetweenDates();
                                            Console.WriteLine();
                                            UI.DisplayMenu();
                                            break;
                                        }
                                    case 3:
                                        {
                                            UI.DeleteTransactionType();
                                            Console.WriteLine();
                                            UI.DisplayMenu();
                                            break;
                                        }
                                    default:
                                        {
                                            Console.WriteLine("No such command.");
                                            Console.WriteLine();
                                            UI.DisplayMenu();
                                            break;
                                        }
                                }
                                break;
                            }

                        case 6:
                            {
                                UI.ShowTypeAmount();
                                Console.WriteLine();
                                UI.DisplayMenu();
                                break;
                            }
                        case 7:
                            {
                                UI.AccountBallanceAtGivenTime();
                                Console.WriteLine();
                                UI.DisplayMenu();
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


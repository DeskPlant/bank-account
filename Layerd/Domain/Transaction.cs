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

        public override string ToString()
        {
            return Id.ToString() + " " + Date.ToString() + " " + Name + " " + Amount + " " + Type.ToString();
        }
    }


    /*     
{
    [
        {
            "Id" : "0f8fad5b-d9cb-469f-a165-70867728950e",
            "Date" : "10/09/2021 00:04:54",
            "Name" : "Rent money",
            "Amount": 102,
            "Type": "Outgoing"
        },
        {
            "Id" : "0f8fad5b-d9cb-469f-a165-70867728950e",
            "Date" : "10/09/2021 00:04:54",
            "Name" : "Rent money",
            "Amount": 102,
            "Type": "Outgoing"
        },
    ]
}


     
     */
}

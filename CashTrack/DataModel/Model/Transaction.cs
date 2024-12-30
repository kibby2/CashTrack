using System;

namespace CashTrack.DataModel.Model
{
    public class Transaction
    {
        public Guid transactionId { get; set; }

        public User userID { get; set; }

        public Tag tagID { get; set; }

        public DateTime date { get; set; }

        public TransactionType transactionType { get; set; }

        public string title { get; set; }

        public double amount { get; set; }

        public string notes { get; set; }

        public string source { get; set; }

        public DateTime dueDate { get; set; }

        public double remainingBalance { get; set; }
    }

}

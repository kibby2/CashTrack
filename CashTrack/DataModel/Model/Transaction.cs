﻿using System;

namespace CashTrack.DataModel.Model
{
    public class Transaction
    {
        public Guid transactionId { get; set; }
        public Tag tagID { get; set; }
        public DateTime date { get; set; }
        public TransactionType transactionType { get; set; }
        public string title { get; set; }
        public double amount { get; set; }
        public string notes { get; set; }
        public DateTime? duedate { get; set; } // Nullable DateTime for debt transactions
        public double remainingBalance { get; set; }

        public string? status { get; set; } = "unpaid";
    }
}

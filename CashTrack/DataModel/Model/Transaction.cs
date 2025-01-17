using System;
using static MudBlazor.CategoryTypes;

namespace CashTrack.DataModel.Model
{
    public class Transaction
    {
        public Guid transactionId { get; set; }
        public List<string> tags { get; set; } = new List<string>(); 
        public Guid tagId { get; set; }
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

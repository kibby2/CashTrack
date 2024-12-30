using System;
using System.Transactions;
using CashTrack.DataModel.Model;

namespace CashTrack.DataAccess.Services.Interface
{
    public interface ITransactionService
    {
        Task<List<DataModel.Model.Transaction>> GetAllTransactions();
        Task<bool> AddTransaction(DataModel.Model.Transaction transaction);
        Task<bool> DeleteTransaction(Guid transactionId);
        Task<double> GetTotalCredits();
        Task<List<Tag>> GetAllTags();  
        Task<bool> AddTag(Tag tag);
    }

}

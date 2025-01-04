using CashTrack.DataModel.Model;

namespace CashTrack.DataAccess.Services.Interface
{
    public interface ITransactionService
    {
        Task<List<Transaction>> GetAllTransactions();
        Task< bool> AddTransaction(Transaction transaction);
        Task<double> GetBalance();
        Task<bool> DeleteTransaction(Guid transactionId);
        Task<bool> PayDebt(Guid transactionId);

    }
}


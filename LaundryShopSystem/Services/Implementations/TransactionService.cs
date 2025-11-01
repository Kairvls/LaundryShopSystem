using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LaundryShopSystem.Models;
using LaundryShopSystem.Repositories.Interfaces;
using LaundryShopSystem.Services.Interfaces;

namespace LaundryShopSystem.Services.Implementations
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _repo;

        public TransactionService(ITransactionRepository repo)
        {
            _repo = repo;
        }

        public long RecordPayment(Transaction transaction)
        {
            return _repo.AddTransaction(transaction);
        }

        public IEnumerable<Transaction> GetTransactionsByOrder(long orderId)
        {
            return _repo.GetTransactionsByOrder(orderId);
        }

        public void UpdateQRCode(long transactionId, string qrCodeUrl)
        {
            var transaction = _repo.GetTransactionById(transactionId);
            if (transaction != null)
            {
                transaction.QRCodeUrl = qrCodeUrl; // Make sure Transaction model has QRCodeUrl
                _repo.UpdateTransaction(transaction); // Repository handles saving changes
            }
        }

        public Transaction GetTransactionById(long transactionId)
        {
            return _repo.GetTransactionById(transactionId);
        }
    }
}

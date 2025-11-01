using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LaundryShopSystem.Models;

namespace LaundryShopSystem.Services.Interfaces
{
    public interface ITransactionService
    {
        long RecordPayment(Transaction transaction);
        IEnumerable<Transaction> GetTransactionsByOrder(long orderId);
        void UpdateQRCode(long transactionId, string qrCodeUrl);
        Transaction GetTransactionById(long transactionId);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LaundryShopSystem.Models;

namespace LaundryShopSystem.Repositories.Interfaces
{
	public interface ITransactionRepository
    {
        long AddTransaction(Transaction transaction);
        IEnumerable<Transaction> GetTransactionsByOrder(long orderId);
        Transaction GetTransactionById(long transactionId);
        void UpdateTransaction(Transaction transaction);
    }
}
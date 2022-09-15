namespace SupportBank
{
    class Ledger
    {
        public List<Transaction> LedgerTransactions;

        public Ledger()
        {
            LedgerTransactions = new List<Transaction>();
        }

        public void AddToLedger(Transaction transaction)
        {
            LedgerTransactions.Add(transaction);
        }
    }
    
}
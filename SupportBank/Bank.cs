namespace SupportBank
{
    class Bank
    {
        public List<Account> BankAccounts;
        public List<Transaction> BankTransactions;

        public Bank()
        {
            BankAccounts = new List<Account>();
            BankTransactions = new List<Transaction>();    
        }

        public bool AddNewBankAccount(Account newBankAccount)
        {
            foreach( Account existingBankAccount in BankAccounts)
            {
                if(existingBankAccount.AccountName == newBankAccount.AccountName)
                {
                    return false;
                }    
            }
            BankAccounts.Add(newBankAccount);
            return true;
        }

        public void AddToBankTransactions(Transaction transaction)
        {
            BankTransactions.Add(transaction);
        }
    }
}

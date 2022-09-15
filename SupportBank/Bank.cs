namespace SupportBank
{
    class Bank
    {
        public List<Account> BankAccounts = new List<Account>();

        public Bank()
        {
            BankAccounts = new List<Account>();    
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
    }
}

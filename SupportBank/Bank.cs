using NLog;
using NLog.Config;
using NLog.Targets;

namespace SupportBank
{
    class Bank
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        public List<Account> BankAccounts;
        public List<Transaction> BankTransactions;

        public Bank()
        {
            BankAccounts = new List<Account>();
            BankTransactions = new List<Transaction>();    
        }

        public bool doesAccountExist(string possibleBankAccountName)
        {
            foreach( Account existingBankAccount in BankAccounts)
            {
                if(existingBankAccount.AccountName == possibleBankAccountName)
                {
                    return true;
                }    
            }
            return false;
        }

        public bool AddNewBankAccount(Account newBankAccount)
        {
            if(doesAccountExist(newBankAccount.AccountName))
            {
                return false;
            }
            BankAccounts.Add(newBankAccount);
            return true;
        }

        public void AddToBankTransactions(Transaction transaction)
        {
            BankTransactions.Add(transaction);
        }

        public void UpdateBalance(Account bankAccount)
        {
            foreach(Transaction bankTransaction in BankTransactions)
            {
                string creditAccount = bankTransaction.TransactionCreditor;

                if (creditAccount == bankAccount.AccountName)
                {
                    bankAccount.AccountBalance += bankTransaction.TransactionAmount;
                }
                string debitAccount = bankTransaction.TransactionDebtor;

                if (debitAccount == bankAccount.AccountName)
                {
                    bankAccount.AccountBalance -= bankTransaction.TransactionAmount;
                }
            }
        }

        public void UpdateAllBalances()
        {
            foreach(Account bankAccount in BankAccounts)
            {
                UpdateBalance(bankAccount);
            }
        }

        public void PrintAllBalances()
        {
            Console.WriteLine($"\nAccount Name            Balance");
            Console.WriteLine($"------------            -------");

            foreach(Account bankAccount in BankAccounts)
            {
                string tabSpacing = new string('\t', (20-bankAccount.AccountName.Length-1)/4);
                //add C or D depending on sign of balance
                string creditOrDebit;
                if(bankAccount.AccountBalance<0)
                {
                    creditOrDebit = "D";
                }
                else
                {
                    creditOrDebit = "C";
                }
                //justify right (could be a conditional number of spaces?)
                Console.WriteLine($"{bankAccount.AccountName}{tabSpacing}£ {Math.Abs(bankAccount.AccountBalance):F2}\t{creditOrDebit}");
            }
        }

        public void PrintAccountNames()
        {
            foreach(Account bankAccount in BankAccounts)
            {
                Console.WriteLine($"{bankAccount.AccountName}");
            }
            Console.WriteLine("\n");
        }


        public void PrintAccountTransactions(string accountName)
        {   
            Console.WriteLine($"\n=========== Credits for {accountName}: ===========\n");
            Console.WriteLine("Date:           Payer:          Amount:         Narrative:");
            foreach (Transaction transaction in BankTransactions)
            {
                string transactionDateTime   = transaction.TransactionDateTime.ToString("dd/MM/yyyy");
                string transactionDebtor     = transaction.TransactionDebtor;
                string transactionCreditor   = transaction.TransactionCreditor;
                string transactionNarrative  = transaction.TransactionNarrative;
                decimal transactionAmount    = transaction.TransactionAmount;
                string tabSpacing = new string('\t', (16-transactionDebtor.Length-1)/4);

                if(accountName == transactionCreditor)
                {
                   Console.WriteLine($"{transactionDateTime}\t{transactionDebtor}{tabSpacing}£ {transactionAmount:F2}\t\t{transactionNarrative}");
                }   
            }

            Console.WriteLine($"\n=========== Debits for {accountName}: ===========\n");
            Console.WriteLine("Date:           Payee:          Amount:         Narrative:");
            foreach (Transaction transaction in BankTransactions)
            {
                string transactionDateTime   = transaction.TransactionDateTime.ToString("dd/MM/yyyy");
                string transactionDebtor     = transaction.TransactionDebtor;
                string transactionCreditor   = transaction.TransactionCreditor;
                string transactionNarrative  = transaction.TransactionNarrative;
                decimal transactionAmount    = transaction.TransactionAmount;
                string tabSpacing = new string('\t', (16-transactionCreditor.Length-1)/4);

                if(accountName == transactionDebtor)
                {
                   Console.WriteLine($"{transactionDateTime}\t{transactionCreditor}{tabSpacing}£ {transactionAmount:F2}\t\t{transactionNarrative}");
                }                
            }
        }
    }
}

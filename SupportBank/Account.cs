namespace SupportBank
{
    class Account
    {
        public string AccountName { get; }
        public decimal AccountCredit { get; set; }
        public decimal AccountDebit { get; set; }
        public decimal AccountBalance
        {
            get
            {
                return AccountCredit - AccountDebit;
            }
            
        }
        public Account(string accountName)
        {
            AccountName   = accountName;
            AccountCredit = 0;
            AccountDebit  = 0;
        }
  }
}

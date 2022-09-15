namespace SupportBank
{
    class Account
    {
        public string AccountName { get; }

        public decimal AccountBalance { get; set; }

        public Account(string accountName)
        {
            AccountName   = accountName;
            AccountBalance = 0;
        }
  }
}

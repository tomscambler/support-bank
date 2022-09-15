namespace SupportBank
{
    class Transaction
    {
        public DateTime TransactionDateTime{ get; }
        public string   TransactionDebtor{ get; }
        public string   TransactionCreditor{ get; }
        public string   TransactionNarrative{ get; }
        public decimal  TransactionAmount{ get;}

        public Transaction(string transactionDateTime, string transactionDebtor, string transactionCreditor, string transactionNarrative, string transactionAmount)
        {
            TransactionDateTime  = DateTime.Parse(transactionDateTime);
            TransactionDebtor    = transactionDebtor;
            TransactionCreditor  = transactionCreditor;
            TransactionNarrative = transactionNarrative;
            TransactionAmount    = Convert.ToDecimal(transactionAmount);
        }
    }   
}
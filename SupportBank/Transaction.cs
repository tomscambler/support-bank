namespace SupportBank
{
    class Transaction
    {
        public DateTime TransactionDateTime{ get; }
        public string   TransactionDebtor{ get; }
        public string   TransactionCreditor{ get; }
        public string   TransactionNarrative{ get; }
        public decimal  TransactionAmount{ get;}

        public Transaction(DateTime transactionDateTime, string transactionDebtor, string transactionCreditor, string transactionNarrative, decimal transactionAmount)
        {
            TransactionDateTime  = transactionDateTime;
            TransactionDebtor    = transactionDebtor;
            TransactionCreditor  = transactionCreditor;
            TransactionNarrative = transactionNarrative;
            TransactionAmount    = transactionAmount;
        }
    }   
}
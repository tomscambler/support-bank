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
            // try
            // {
                TransactionDateTime  = DateTime.Parse(transactionDateTime);
            // }
            // catch(FormatException e)
            // {
            //     TransactionDateTime  = DateTime.Parse("01/01/01");
            //     // throw new FormatException("That is not a date time", e);
            // }
            
            TransactionDebtor    = transactionDebtor;
            TransactionCreditor  = transactionCreditor;
            TransactionNarrative = transactionNarrative;

            // try
            // {
                TransactionAmount = Convert.ToDecimal(transactionAmount);
            // }
            // catch(FormatException e)
            // {
            //     TransactionAmount = Convert.ToDecimal(0.00);
            //     // throw new FormatException("That is not a decimal", e);
            // }
        }
    }   
}
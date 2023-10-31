namespace MintosParser.StatementTypes {
    public interface IStatementType {
        #region properties
        public string AdditionalDetails { get; set; }
        public string TransactionId {get; set; }
        public double Value {get; set; }
        public DateTime Date {get; set; }
        public string Currency {get; set; }
        string RawPaymentMethod {get; }
        public string OutputType { get; }
        #endregion
    }
}
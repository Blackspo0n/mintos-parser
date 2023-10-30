namespace MintosParser.StatementTypes {
    public interface IStatementType {
        #region properties
        public string AdditionalDetails { get; set; }
        public string transactionId {get; set; }
        public double value {get; set; }
        public DateTime date {get; set; }
        public string currency {get; set; }
        string RawPaymentMethod {get; }
        public string outputType { get; }
        #endregion
    }
}
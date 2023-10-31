using MintosParser.StatementTypes;

namespace MintosParser.OutputStatementTypes {
    public interface IOutputStatementType {
        #region properties
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public List<IStatementType> aggregatedStatementTypes {get; set;}
        public DateTime OutputDate {get; }
        // This is Decimal, because it can only be two diggests afterwards
        public decimal OutputValue { get; }
        public string Currency {get; }
        public string Notes { get; }
        public string Type { get; }
        #endregion

        #region method to implement
        public void AddStatementToAggregation (IStatementType statement);

        #endregion
    }
}
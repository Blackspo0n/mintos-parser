using MintosParser.StatementTypes;

namespace MintosParser.OutputStatementTypes {
    public abstract class AbstractOutputStatementType : IOutputStatementType {
        #region properties
        public DateTime FromDate { get; set;}
        public DateTime ToDate  {get; set;}
        public List<IStatementType> aggregatedStatementTypes {get; set;}
        public DateTime OutputDate {get => ToDate; }
        // This is Decimal, because it can only be two diggests afterwards
        public decimal OutputValue => (decimal)Math.Round(aggregatedStatementTypes.Sum(x => x.Value),2);
        public string Currency => aggregatedStatementTypes.OrderByDescending(x => x.Date).First().Currency;
        public abstract string Notes { get;}
        public abstract string Type { get;}
        #endregion

        #region method to implement
        public AbstractOutputStatementType() {
            aggregatedStatementTypes = new List<IStatementType>();
        }

        public void AddStatementToAggregation (IStatementType statement) {
            aggregatedStatementTypes.Add(statement);
        }
        #endregion
    }
}
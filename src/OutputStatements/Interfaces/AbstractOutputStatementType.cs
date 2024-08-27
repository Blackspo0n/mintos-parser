using MintosParser.StatementTypes;

namespace MintosParser.OutputStatementTypes {
    public abstract class AbstractOutputStatementType : IOutputStatementType {
        #region properties
        public DateTime FromDate { get; set;}
        public DateTime ToDate  {get; set;}
        public List<IStatementType> AggregatedStatementTypes {get; set;} = new List<IStatementType>();
        public DateTime OutputDate {get => ToDate; }
        // This is Decimal, because it can only be two diggests afterwards
        public decimal OutputValue => (decimal)Math.Round(AggregatedStatementTypes.Sum(x => x.Value),2);
        public string Currency => AggregatedStatementTypes.OrderByDescending(x => x.Date).First().Currency;
        public abstract string Notes { get;}
        public abstract string Type { get;}
        #endregion

        #region method to implement
        public AbstractOutputStatementType() {}

        public void AddStatementToAggregation (IStatementType statement) {
            AggregatedStatementTypes.Add(statement);
        }
        #endregion
    }
}
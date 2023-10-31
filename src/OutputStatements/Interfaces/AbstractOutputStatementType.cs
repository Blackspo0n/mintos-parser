using MintosParser.StatementTypes;

namespace MintosParser.OutputStatementTypes {
    public abstract class AbstractOutputStatementType : IOutputStatementType {
        #region properties
        public DateTime fromDate { get; set;}
        public DateTime toDate  {get; set;}
        public List<IStatementType> aggregatedStatementTypes {get; set;}
        public DateTime outputDate {get => toDate; }
        // This is Decimal, because it can only be two diggests afterwards
        public Decimal outputValue => (Decimal)Math.Round(aggregatedStatementTypes.Sum(x => x.value),2);
        public string currency => aggregatedStatementTypes.OrderByDescending(x => x.date).First().currency;
        public abstract string notes { get;}
        public abstract string type { get;}
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
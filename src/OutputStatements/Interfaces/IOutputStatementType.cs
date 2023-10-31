using System.Data;
using MintosParser.StatementTypes;

namespace MintosParser.OutputStatementTypes {
    public interface IOutputStatementType {
        #region properties
        public DateTime fromDate { get; set; }
        public DateTime toDate { get; set; }
        public List<IStatementType> aggregatedStatementTypes {get; set;}
        public DateTime outputDate {get; }
        // This is Decimal, because it can only be two diggests afterwards
        public Decimal outputValue { get; }
        public string currency {get; }
        public string notes { get; }
        public string type { get; }
        #endregion

        #region method to implement
        public void AddStatementToAggregation (IStatementType statement);

        #endregion
    }
}
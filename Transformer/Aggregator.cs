using System.Data;
using System.Reflection.Metadata.Ecma335;
using MintosParser.OutputStatementTypes;
using MintosParser.StatementTypes;
using NLog;

namespace MintosParser {
    class Aggregator {
        private static Logger logger = NLog.LogManager.GetCurrentClassLogger();

        // @TODO: Make Aggregration configurable
        public enum AggregrationSpan {
            daily = 1,
            monthly=2,
            quarterly=3,
        }
        public static AggregrationSpan aggration { get; set;}

        public static List<IOutputStatementType> Aggregate(List<IStatementType> statementTable)
        {
            var list = new List<IOutputStatementType>();

            var groupedStatements = statementTable.GroupBy(AggregationFilter());
            foreach (var group in groupedStatements) {
                var types = group.GroupBy(x => x.outputType);

                foreach(var type in types) {
                    logger.Info("Process payment type " + type.Key + " for aggregation " + group.Key);
                    IOutputStatementType? outputStatement = null;
                    foreach(var item in type) {
                        outputStatement = OutputStatementFactory.Create(item);
                        if(outputStatement == null) break; // break if we does not have any type for our output

                        outputStatement.AddStatementToAggregation(item);
                    }
                    if(outputStatement != null) list.Add(outputStatement);
                }
            }

            return list;
        }

        private static Func<IStatementType, string> AggregationFilter()
        {
            return x => x.date.ToString("yyyy.MM");
            // Todo: make Span configurable
        }
        /*

        public static DataTable Normalize(DataTable statementTable) {
            foreach(DataRow item in statementTable.AsEnumerable()) {
                //Portfolio Performance does not allow more than two diggest in the "Wert" field
                item["Wert"] = Math.Round((double)item["Wert"], 2);
                if(!DBNull.Value.Equals(item["Stück"])) item["Stück"] = Math.Round((double)item["Stück"], 15);
            }
            return statementTable;
        }*/
    }
}

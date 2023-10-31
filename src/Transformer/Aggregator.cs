using MintosParser.OutputStatementTypes;
using MintosParser.StatementTypes;

namespace MintosParser {
    class Aggregator {
        public enum AggregrationSpan {
            //none = -1 will implement it at a later stage
            daily = 1,
            monthly=2,
            quarterly=3,
            yearly = 4
        }
        public static AggregrationSpan Aggregation { get; set;} = AggregrationSpan.quarterly;

        public static List<IOutputStatementType> Aggregate(List<IStatementType> statementTable)
        {
            var list = new List<IOutputStatementType>();

            var groupedStatements = statementTable.GroupBy(AggregationFilter());
            foreach (var group in groupedStatements) {
                var types = group.GroupBy(x => x.outputType);

                foreach(var type in types) {
                    Console.WriteLine("Process payment type " + type.Key + " for aggregation " + group.Key);
                    IOutputStatementType? outputStatement = null;
                    foreach(var item in type) {
                        if(outputStatement == null) {
                            outputStatement = OutputStatementFactory.Create(item);
                            if(outputStatement == null) break; // break if we does not have any type for our output
                            else {
                                outputStatement.fromDate = AggregrationBeginDate(item.date);
                                outputStatement.toDate = AggregrationEndDate(item.date);
                            }
                        }
                        outputStatement.AddStatementToAggregation(item);
                    }
                    if(outputStatement != null) list.Add(outputStatement);
                }
            }

            return list;
        }

        private static Func<IStatementType, object> AggregationFilter()
        {
            return Aggregation switch
            {
                AggregrationSpan.daily => x => x.date.ToString("yyyy.MM.dd"),
                AggregrationSpan.monthly => x => x.date.ToString("yyyy.MM"),
                AggregrationSpan.quarterly => x => "quarter " +  (((x.date.Month - 1) / 3) + 1) + " of " + x.date.ToString("yyyy"),// more readable in console
                _ => x => x.date.ToString("yyyy"),
            };
        }

        private static DateTime AggregrationBeginDate(DateTime date) {
            return Aggregation switch
            {
                AggregrationSpan.daily => new DateTime(date.Year, date.Month, date.Day),
                AggregrationSpan.monthly => new DateTime(date.Year, date.Month, 1),
                AggregrationSpan.quarterly => new DateTime(date.Year, (((date.Month - 1) / 3 + 1) - 1) * 3 + 1, 1),
                _ => new DateTime(date.Year, 1, 1),
            };
        }

        private static DateTime AggregrationEndDate(DateTime date) {
            return Aggregation switch
            {
                AggregrationSpan.daily => new DateTime(date.Year, date.Month, date.Day).AddDays(1).AddMilliseconds(-1),
                AggregrationSpan.monthly => new DateTime(date.Year, date.Month, 1).AddMonths(1).AddDays(-1),
                AggregrationSpan.quarterly => new DateTime(date.Year, (((date.Month - 1) / 3 + 1) - 1) * 3 + 1, 1).AddMonths(3).AddDays(-1),
                _ => new DateTime(date.Year, 1, 1).AddYears(1).AddDays(-1),
            };
        }
    }
}
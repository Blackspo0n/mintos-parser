using System.Data;
using Microsoft.Extensions.Logging;
using MintosParser.StatementTypes;
using NLog;

namespace MintosParser {
    class Compactor {
        public static Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public static DataTable Compact(DataTable statementTable) {
            var returnTable = statementTable.Copy();

            var taxEntries = statementTable.AsEnumerable().Where(x => x["Typ"].ToString() == "Steuern");
            foreach(DataRow item in taxEntries) {
                //logger.Trace("Tax is " + item["Wert"].ToString());
                var receivedInterest = statementTable.AsEnumerable().Where(
                    x => x["Typ"].ToString().Contains("Zinsen") && 
                    x["ISIN"].ToString().Contains(item["ISIN"].ToString()) && 
                            Math.Abs(((DateTime)x["Orginales Datum"]- (DateTime)item["Orginales Datum"]).TotalSeconds) < 2 &&
                    x["Wertpapiername"].Equals(item["Wertpapiername"])
                );
                
                if(receivedInterest.LongCount() > 1) {
                    //logger.Trace("Tax position for " + receivedInterest.LongCount() + " interests");
                    var sumrow = returnTable.NewRow();
                    sumrow.ItemArray = (object?[])statementTable.AsEnumerable().Where(
                            x => x["Typ"].ToString().Contains("Zinsen") && 
                            x["ISIN"].ToString().Contains(item["ISIN"].ToString()) && 
                            Math.Abs(((DateTime)x["Orginales Datum"]- (DateTime)item["Orginales Datum"]).TotalSeconds) < 2 &&
                            x["Wertpapiername"].Equals(item["Wertpapiername"])
                        ).First().ItemArray.Clone();
                    sumrow["Wert"] = 0;

                    foreach(DataRow subItem in receivedInterest) {
                        sumrow["Wert"] = (double)sumrow["Wert"] + (double)subItem["Wert"];

                        returnTable.Rows.Remove(returnTable.AsEnumerable().Where(
                            x => x["Typ"].ToString().Contains("Zinsen") && 
                            x["ISIN"].ToString().Contains(item["ISIN"].ToString()) && 
                            Math.Abs(((DateTime)x["Orginales Datum"]- (DateTime)item["Orginales Datum"]).TotalSeconds) < 2 &&
                            x["Wertpapiername"].Equals(item["Wertpapiername"])
                        ).First() );
                    }

                    returnTable.Rows.Remove(returnTable.AsEnumerable().Where(
                            x => x["Typ"].ToString().Contains("Steuern") && 
                            x["ISIN"].ToString().Contains(item["ISIN"].ToString()) && 
                            Math.Abs(((DateTime)x["Orginales Datum"]- (DateTime)item["Orginales Datum"]).TotalSeconds) < 2 &&
                            x["Wertpapiername"].Equals(item["Wertpapiername"])
                        ).First());
                    
                    
                    //logger.Trace("Final sum of taxes " + sumrow["Wert"]);
                    returnTable.Rows.Add(sumrow);

                }
                else {
                    //logger.Trace("Tax has only one position!");
                    
                        returnTable.AsEnumerable().Where(
                            x => x["Typ"].ToString().Contains("Zinsen") && 
                            x["ISIN"].ToString().Contains(item["ISIN"].ToString()) && 
                            ((DateTime)x["Orginales Datum"]- (DateTime)item["Orginales Datum"]).TotalSeconds < 2 &&
                            x["Wertpapiername"].Equals(item["Wertpapiername"])
                        ).First()["Steuern"] = item["Wert"];

                        //logger.Trace("Final sum of taxes " + item["Wert"]);
                    
                    returnTable.Rows.Remove(returnTable.AsEnumerable().Single(
                        x => x["Typ"].ToString().Contains("Steuern") && 
                        x["ISIN"].ToString().Contains(item["ISIN"].ToString()) && 
                        ((DateTime)x["Orginales Datum"]- (DateTime)item["Orginales Datum"]).TotalSeconds < 2 &&
                        x["Wertpapiername"].Equals(item["Wertpapiername"])
                    ));
                }
            };
            
            return returnTable;
        }
    }
}

using System.Data;

namespace MintosParser.StatementTypes {

    public abstract class AbstractStatementType : IStatementType
    {
        #region properties
        public string AdditionalDetails { get; set; }
        public string RawPaymentMethod {get; private set; }
        public string transactionId { get; set ; }
        public double value { get; set; }
        public DateTime date { get; set; }
        public string currency {get; set;}
        public abstract string outputType { get;}

        #endregion
        public AbstractStatementType(DataRow row) {
            transactionId = row["Transaction ID:"].ToString() ?? String.Empty;
            date = DateTime.ParseExact(row["Date"].ToString() ?? String.Empty, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture);
            value = Double.Parse(row["Turnover"].ToString() ?? "0", System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture);
            RawPaymentMethod = row["Payment Type"].ToString() ?? String.Empty;
            currency = row["Currency"].ToString() ?? String.Empty;
            AdditionalDetails = row["Details"].ToString() ?? String.Empty;

        }
    }
}
using System.Data;

namespace MintosParser.StatementTypes {

    public abstract class AbstractStatementType : IStatementType
    {
        #region properties
        public string AdditionalDetails { get; set; }
        public string RawPaymentMethod {get; private set; }
        public string TransactionId { get; set ; }
        public double Value { get; set; }
        public DateTime Date { get; set; }
        public string Currency {get; set;}
        public abstract string OutputType { get;}

        #endregion
        public AbstractStatementType(DataRow row) {
            TransactionId = row["Transaction ID:"].ToString() ?? string.Empty;
            Date = DateTime.ParseExact(row["Date"].ToString() ?? string.Empty, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture);
            Value = double.Parse(row["Turnover"].ToString() ?? "0", System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture);
            RawPaymentMethod = row["Payment Type"].ToString() ?? string.Empty;
            Currency = row["Currency"].ToString() ?? string.Empty;
            AdditionalDetails = row["Details"].ToString() ?? string.Empty;

        }
    }
}
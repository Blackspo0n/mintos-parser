using System.Data;
using NLog;

namespace MintosParser.StatementTypes {

    public abstract class AbstractStatementType : IStatementType
    {
        #region properties
        public string AdditionalDetails { get; set; }
        public Logger logger = LogManager.GetCurrentClassLogger();
        public string RawPaymentMethod {get; private set; }
        public string transactionId { get; set ; }
        public double value { get; set; }
        public DateTime date { get; set; }
        public string currency {get; set;}
        public abstract string outputType { get;}
        public abstract string GetTransformerType();
        public abstract Dictionary<string,object> GetTransformerFields();

        #endregion
        public AbstractStatementType(DataRow row) {
            transactionId = row["Transaction ID:"].ToString();
            date = DateTime.ParseExact(row["Date"].ToString(), "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture);
            value = Double.Parse(row["Turnover"].ToString(), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture);
            RawPaymentMethod = row["Payment Type"].ToString();
            currency = row["Currency"].ToString();
            AdditionalDetails = row["Details"].ToString();

        }
    }
}
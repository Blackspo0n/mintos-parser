using System.Data;

namespace MintosParser.StatementTypes {

    public abstract class AbstractLoanType : AbstractStatementType, IStatementType
    {   
        public string ISIN { get; set; }
        public string LoanNumber { get; set; }
        public bool isBuyback { get; set; }
        public bool isPending { get; set; }
        public AbstractLoanType(DataRow row) : base(row) {
            ExtractDetails(row["Details"].ToString());
            if(row["Payment Type"].ToString().Contains("repurchase") || row["Payment Type"].ToString().Contains("rebuy")) {
                isBuyback = true;
            }
            else {
                isBuyback = false;
            }
            
            if(row["Payment Type"].ToString().Contains("pending")) {
                isPending = true;
            }
            else {
                isPending = false;
            }
        }        
        public void ExtractDetails(string details)
        {
            int startIndex = details.IndexOf("ISIN:");
            if (startIndex != -1)
            {
                startIndex += 5;
                int endIndex = details.IndexOf(" (Loan", startIndex);
                if (endIndex != -1)
                {
                    ISIN = details.Substring(startIndex, endIndex - startIndex).Trim();
                    LoanNumber = details.Substring(endIndex+7, 11);
                    AdditionalDetails = details.Substring(endIndex+19).Trim();
                }
            }
            else {
                AdditionalDetails = details;
            }
        }
        
        public abstract override Dictionary<string, object> GetTransformerFields();
    }
}
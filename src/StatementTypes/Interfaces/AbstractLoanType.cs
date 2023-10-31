using System.Data;

namespace MintosParser.StatementTypes {

    public abstract class AbstractLoanType : AbstractStatementType, IStatementType
    {   
        public string ISIN { get; set; }
        public string LoanNumber { get; set; }
        public bool IsBuyback { get; set; }
        public bool IsPending { get; set; }
        public AbstractLoanType(DataRow row) : base(row) {
            ISIN = string.Empty;
            LoanNumber = string.Empty;

            ExtractDetails(row["Details"].ToString()  ?? string.Empty);
            if((row["Payment Type"].ToString() ?? string.Empty).Contains("repurchase") || (row["Payment Type"].ToString() ?? string.Empty).Contains("rebuy")) {
                IsBuyback = true;
            }
            else {
                IsBuyback = false;
            }
            
            if((row["Payment Type"].ToString() ?? string.Empty).Contains("pending")) {
                IsPending = true;
            }
            else {
                IsPending = false;
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
        
    }
}
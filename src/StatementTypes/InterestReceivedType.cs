using System.Data;

namespace MintosParser.StatementTypes {

    class InterestReceivedType : AbstractLoanType, IStatementType
    {   
        
        public override string OutputType => "Interest";
        public InterestReceivedType(DataRow row) : base(row) {
        }

/*
        public override string GetTransformerType()  {
            return "Zinsen";
        }
        public override Dictionary<string, object> GetTransformerFields()
        {
            var dict = new Dictionary<string, object>();
            dict.Add("Notizen", LoanNumber + " " + AdditionalDetails);
            dict.Add("ISIN", ISIN);
            dict.Add("Wertpapiername", LoanNumber);
            return dict;
        }   
*/
    }
}
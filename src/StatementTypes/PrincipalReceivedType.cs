using System.Data;

namespace MintosParser.StatementTypes {

    class PrincipalReceivedType : AbstractLoanType, IStatementType
    {   
        
        public override string OutputType => "Movement";
        public PrincipalReceivedType(DataRow row) : base(row) {
        }
        /*
        public override string GetTransformerType()  {
            return "Verkauf";
        }

        public override Dictionary<string, object> GetTransformerFields()
        {
            var dict = new Dictionary<string, object>();
            dict.Add("Notizen", LoanNumber + " " + AdditionalDetails);
            dict.Add("ISIN", ISIN);
            dict.Add("Wertpapiername", LoanNumber);
            return dict;
        } */

    }
}
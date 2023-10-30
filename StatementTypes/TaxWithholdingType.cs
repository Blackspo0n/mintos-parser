using System.Data;

namespace MintosParser.StatementTypes {

    class TaxWithholdingType : AbstractLoanType, IStatementType
    {   
        
        public override string outputType => "Tax";
        public TaxWithholdingType(DataRow row) : base(row) {
        }

        public override string GetTransformerType()  {
            return "Steuern";
        }

        public override Dictionary<string, object> GetTransformerFields()
        {
            var dict = new Dictionary<string, object>();
            dict.Add("Notizen", LoanNumber + " " + AdditionalDetails);
            dict.Add("ISIN", ISIN);
            dict.Add("Wertpapiername", LoanNumber);
            return dict;
        }
    }
}
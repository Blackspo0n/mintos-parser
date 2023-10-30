using System.Data;
using System.Net.Mail;

namespace MintosParser.StatementTypes {

    class InvestmentType : AbstractLoanType, IStatementType
    {   
        
        public override string outputType => "Investment";
        public InvestmentType(DataRow row) : base(row) {
        }        
        /*
        public override string GetTransformerType()  {
            return "Kauf";
        }

        public override Dictionary<string, object> GetTransformerFields()
        {
            var dict = new Dictionary<string, object>();
            dict.Add("Notizen", LoanNumber + " " + AdditionalDetails);
            dict.Add("ISIN", ISIN);
            dict.Add("Wertpapiername", LoanNumber);
            return dict;
        }*/
    }
}
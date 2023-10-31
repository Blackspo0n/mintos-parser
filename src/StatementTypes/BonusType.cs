using System.Data;

namespace MintosParser.StatementTypes {

    class BonusType : AbstractStatementType, IStatementType
    {   
        
        public override string OutputType => "Account";

        public bool isCashback { get; set; }
        public BonusType(DataRow row) : base(row) {
            if(AdditionalDetails.Contains("Cashback bonus")) {
                isCashback = true;
            }
        }
/*
        public override string GetTransformerType()  {
            return "Zinsen";
        }

        public override Dictionary<string, object> GetTransformerFields()
        {
            var dict = new Dictionary<string, object>();
            dict.Add("Notizen",(isCashback) ? "Cashback bonus" : "Reference bonus");
            return dict;
        }
*/
    }
}
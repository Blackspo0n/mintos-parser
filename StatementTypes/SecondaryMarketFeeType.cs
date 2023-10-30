using System.Data;

namespace MintosParser.StatementTypes {

    class SecondaryMarketFeeType : AbstractLoanType, IStatementType
    {   
        
        public override string outputType => "Fee";
        public bool isDiscount { get; set; }
        public SecondaryMarketFeeType(DataRow row) : base(row) {
            if(row["Payment Type"].ToString().Contains("discount or premium")) isDiscount = true;
            else isDiscount = false;
        }
        public override string GetTransformerType()  {
            return (value > 0)? "Gebühren": "Gebührenrückerstattung";
        }

        public override Dictionary<string, object> GetTransformerFields()
        {
            var dict = new Dictionary<string, object>();
            dict.Add("Notizen", ((isDiscount)? "Sekundärmarktgebühren " : "Aufschlag/Abschlag") + LoanNumber + " " + AdditionalDetails);
            dict.Add("ISIN", ISIN);
            dict.Add("Wertpapiername", LoanNumber);
            return dict;
        } 
    }
}
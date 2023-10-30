using System.Data;

namespace MintosParser.StatementTypes {

    class SecondaryMarketType : AbstractLoanType, IStatementType
    {   
        
        public override string outputType => "SecondaryMarket";
        public enum MarketType {
            Buy = 0,
            Sell = 1,
        }        

        public MarketType side { get; set;}

        public SecondaryMarketType(DataRow row) : base(row) {
            if(value > 0) side = MarketType.Buy;
            else side = MarketType.Sell;
        }
        public override string GetTransformerType()  {
            return (side == MarketType.Buy) ? "Kauf" : "Verkauf";
        }

        public override Dictionary<string, object> GetTransformerFields()
        {
            var dict = new Dictionary<string, object>();
            dict.Add("Notizen", "Sekundärmarkt " + LoanNumber + " " + AdditionalDetails);
            dict.Add("ISIN", ISIN);
            dict.Add("Wertpapiername", LoanNumber);
            dict.Add("Stück",value);
            dict.Add("Wert", 1);
            return dict;
        } 
    }
}
using System.Data;

namespace MintosParser.StatementTypes {

    class DepotType : AbstractStatementType, IStatementType
    {   
        
        public override string outputType => "Account";
        public enum DepotMovementType {
            Deposits = 0,
            Withdraw = 1,
        }

        public DepotMovementType movementType { get; private set; }
        public DepotType(DataRow row) : base(row) {
            if(row["Payment Type"].ToString() == "Deposits") movementType = DepotMovementType.Deposits;
            else movementType = DepotMovementType.Withdraw;
        }      

        public override string GetTransformerType()  {
            return (movementType == DepotMovementType.Deposits) ? "Einlage" : "Entnahme";
        }

        public override Dictionary<string, object> GetTransformerFields()
        {
            var dict = new Dictionary<string, object>();
            dict.Add("Notizen",(movementType == DepotMovementType.Deposits) ? "Deposits" : "Withdraws");
            return dict;
        }

    }
}
using System.Data;

namespace MintosParser.StatementTypes {

    class StatementTypeFactory
    {
        
        public static IStatementType? GetStatementType (string? PaymentType, DataRow row) {
            switch (PaymentType) {
                default:
                    Console.WriteLine("Statement " + PaymentType + " found but is not jet supported");
                    return null; 
                case "Deposits":
                case "Withdraw":
                    return new DepotType(row);
                case "Bonus":
                case "Welcome bonus":
                case "Cashback bonus":
                    return new BonusType(row);
                case "Investment":
                    return new InvestmentType(row);
                case "Delayed interest income on transit rebuy":
                case "Interest received from pending payments":
                case "Interest received from loan repurchase":
                case "Interest received":
                    return new InterestReceivedType(row);
                case "Tax withholding":
                    return new TaxWithholdingType(row);
                case "Principal received from loan repurchase":
                case "Principal received":
                    return new PrincipalReceivedType(row);
                case "Secondary market transaction":
                    return new SecondaryMarketType(row);
                case "Secondary market transaction - discount or premium":
                case "Secondary market fee":
                    return new SecondaryMarketFeeType(row);

            }
        }
    }
}
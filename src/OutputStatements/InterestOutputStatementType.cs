using MintosParser.StatementTypes;

namespace MintosParser.OutputStatementTypes {
    public class InterestOutputStatementType : AbstractOutputStatementType, IOutputStatementType {
        #region properties
        public override string Notes { get {
            return "Zinsenzusammenfassung :" + Environment.NewLine +
            "Von: " + FromDate.ToString("dd.MM.yyyy") + " Bis: " + ToDate.ToString("dd.MM.yyyy") + 
            string.Join(Environment.NewLine, 
                aggregatedStatementTypes.Select(x =>
                    "[" + x.Date.ToString("HH:mm.ss dd.MM.yyyy") + "] " +
                    ((x.Value > 0) ? "Zinsen" : "Zinsbelastung") + " " +
                    x.Value.ToString() + " " + x.Currency +" " +
                    x.RawPaymentMethod + " (" + ((AbstractLoanType)x).LoanNumber + " " + ((AbstractLoanType)x).ISIN + ")"
                )
            );
        }}
        public override string Type {get {
            if(OutputValue > 0) return "Zinsen";
            return "Zinsbelastung";
        }}
        #endregion
    }
}
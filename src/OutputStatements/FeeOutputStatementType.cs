using System.Xml.Schema;
using MintosParser.StatementTypes;

namespace MintosParser.OutputStatementTypes {
    public class FeeOutputStatementType : AbstractOutputStatementType, IOutputStatementType {
        #region properties
        public override string notes { get {
            return "Gebührenzusammenfasssung :" + Environment.NewLine + 
            "Von: " + fromDate.ToString("dd.MM.yyyy") + " Bis: " + toDate.ToString("dd.MM.yyyy") + 
            string.Join(Environment.NewLine, 
                aggregatedStatementTypes.Select(x =>
                    "[" + x.date.ToString("HH:mm.ss dd.MM.yyyy") + "] " +
                    ((x.value > 0) ? "Gebührenerstattung" : "Gebühren") + " " + 
                    x.value.ToString() + " " + x.currency + " " +
                    x.RawPaymentMethod + " (" + ((AbstractLoanType)x).LoanNumber + " " + ((AbstractLoanType)x).ISIN + ")"
            )
            );
        }}
        public override string type {get {
            if(outputValue > 0) return "Gebührenerstattung";
            return "Gebühren";
        }}
        #endregion
    }
}
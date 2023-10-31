using System.Xml.Schema;
using MintosParser.StatementTypes;

namespace MintosParser.OutputStatementTypes {
    public class TaxOutputStatementType : AbstractOutputStatementType, IOutputStatementType {
        #region properties
        public override string notes { get {
            return "Steuerzusammenfassung:" + Environment.NewLine + 
            "Von: " + fromDate.ToString("dd.MM.yyyy") + " Bis: " + toDate.ToString("dd.MM.yyyy") + 
            string.Join(Environment.NewLine, 
                aggregatedStatementTypes.Select(x =>
                    "[" + x.date.ToString("HH:mm.ss dd.MM.yyyy") + "] " +
                    ((x.value > 0) ? "Steuerrrückerstattung" : "Steuern") + " " +
                    x.value.ToString() + " " + x.currency + " " +
                    x.RawPaymentMethod + " (" + ((AbstractLoanType)x).LoanNumber + " " + ((AbstractLoanType)x).ISIN + ")"
                )
            );
        }}
        public override string type {get {
            if(outputValue > 0) return "Steuerrrückerstattung";
            return "Steuern";
        }}
        #endregion
    }
}
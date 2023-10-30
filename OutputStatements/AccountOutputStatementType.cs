using System.Xml.Schema;
using MintosParser.StatementTypes;

namespace MintosParser.OutputStatementTypes {
    public class AccountOutputStatementType : AbstractOutputStatementType, IOutputStatementType {
        #region properties
        public override string notes { get {
            return "Kontobewegungen:" + Environment.NewLine + 
            "Von: " + fromDate.ToString("dd.MM.yyyy") + " Bis: " + fromDate.ToString("dd.MM.yyyy") + Environment.NewLine + 
            string.Join(Environment.NewLine, 
                aggregatedStatementTypes.Select(x =>
                    "[" + x.date.ToString("HH:mm.ss dd.MM.yyyy") + "] " +
                    ((x.value > 0) ? "Einlage" : "Entnahme") + " " +
                    x.value.ToString() + " " + x.currency + " " +
                    x.RawPaymentMethod + " (" + x.AdditionalDetails + ")"
                )
            );
        }}
        public override string type {get {
            if(outputValue > 0) return "Einlage";
            return "Entnahme";
        }}
        #endregion
    }
}
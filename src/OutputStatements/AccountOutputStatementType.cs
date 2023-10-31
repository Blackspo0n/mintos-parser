namespace MintosParser.OutputStatementTypes {
    public class AccountOutputStatementType : AbstractOutputStatementType, IOutputStatementType {
        #region properties
        public override string Notes { get {
            return "Kontobewegungen:" + Environment.NewLine + 
            "Von: " + FromDate.ToString("dd.MM.yyyy") + " Bis: " + ToDate.ToString("dd.MM.yyyy") + Environment.NewLine + 
            string.Join(Environment.NewLine, 
                aggregatedStatementTypes.Select(x =>
                    "[" + x.Date.ToString("HH:mm.ss dd.MM.yyyy") + "] " +
                    ((x.Value > 0) ? "Einlage" : "Entnahme") + " " +
                    x.Value.ToString() + " " + x.Currency + " " +
                    x.RawPaymentMethod + " (" + x.AdditionalDetails + ")"
                )
            );
        }}
        public override string Type {get {
            if(OutputValue > 0) return "Einlage";
            return "Entnahme";
        }}
        #endregion
    }
}
using MintosParser.OutputStatementTypes;
using MintosParser.StatementTypes;


namespace MintosParser {
    class Transformer {
        // public static string DepotName { get; set; }
        public static string AccountName { get; set; }
        
        public static void Transform(List<IOutputStatementType> statements, OutputStatementFile outputFile) {
            foreach(IOutputStatementType statement in statements) {
                var dr = outputFile.outputDataTable.NewRow();

                dr["Orginales Datum"] = statement.outputDate;
                dr["Datum"] = statement.outputDate.ToString("dd.MM.yyyy");
                dr["Uhrzeit"] = statement.outputDate.ToString("HH:mm:ss");
                dr["Wert"] = statement.outputValue;
                //dr["Depot"] = escapeCell(DepotName);
                dr["Konto"] = escapeCell(AccountName);
                dr["Typ"] = escapeCell(statement.type);
                dr["Buchungswährung"] = escapeCell(statement.currency);
                dr["Notiz"] = escapeCell(statement.notes);

                outputFile.outputDataTable.Rows.Add(dr);
            }
        }
        private static string escapeCell(string value)
        {
            var mustQuote = value.Any(x => x == ',' || x == '\"' || x == '\r' || x == '\n');
            if (!mustQuote) return value;

            return string.Format("\"{0}\"", value.Replace("\"", "\"\""));
        }
    }
}

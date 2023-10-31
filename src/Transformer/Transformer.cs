using MintosParser.OutputStatementTypes;

namespace MintosParser {
    class Transformer {
        // public static string DepotName { get; set; } = String.Empty;
        public static string AccountName { get; set; } = string.Empty;
        
        public static void Transform(List<IOutputStatementType> statements, OutputStatementFile outputFile) {
            foreach(IOutputStatementType statement in statements) {
                var dr = outputFile.OutputDataTable.NewRow();

                dr["Orginales Datum"] = statement.OutputDate;
                dr["Datum"] = statement.OutputDate.ToString("dd.MM.yyyy");
                dr["Uhrzeit"] = statement.OutputDate.ToString("HH:mm:ss");
                dr["Wert"] = statement.OutputValue;
                //dr["Depot"] = escapeCell(DepotName);
                dr["Konto"] = escapeCell(AccountName);
                dr["Typ"] = escapeCell(statement.Type);
                dr["Buchungswährung"] = escapeCell(statement.Currency);
                dr["Notiz"] = escapeCell(statement.Notes);

                outputFile.OutputDataTable.Rows.Add(dr);
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

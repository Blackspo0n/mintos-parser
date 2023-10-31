using System.Data;
using System.Text;

namespace MintosParser {
    class OutputStatementFile {
        #region properties
        private string Delimiter {get; set;} = string.Empty;
        private Encoding Encoding {get; set;} = Encoding.Default;
        public FileInfo Path { get; set; }
        public DataTable OutputDataTable { get; set; }
        #endregion
        public OutputStatementFile(FileInfo path) {
            Path = path;
            OutputDataTable = new DataTable();
        }

        public void SetParsingOptions(string delimiter, Encoding encoding) {
            this.Delimiter = delimiter;
            this.Encoding = encoding;
        }

        public void PrepareOutputFile() {
            OutputDataTable.Columns.Add(new DataColumn() {ColumnName = "Orginales Datum", DataType = typeof(DateTime), AllowDBNull = false});
            OutputDataTable.Columns.Add(new DataColumn() {ColumnName = "Datum", DataType = typeof(string), AllowDBNull = false});
            OutputDataTable.Columns.Add(new DataColumn() {ColumnName = "Uhrzeit", DataType = typeof(string), AllowDBNull = false});
            OutputDataTable.Columns.Add(new DataColumn() {ColumnName = "Typ", DataType = typeof(string), AllowDBNull = false});
            OutputDataTable.Columns.Add(new DataColumn() {ColumnName = "Buchungswährung", DataType = typeof(string), AllowDBNull = false});
            OutputDataTable.Columns.Add(new DataColumn() {ColumnName = "Wert", DataType = typeof(double), AllowDBNull = false});;
            //outputDataTable.Columns.Add(new DataColumn() {ColumnName = "Stück", DataType = typeof(Double), AllowDBNull = true});
            //outputDataTable.Columns.Add(new DataColumn() {ColumnName = "Gebühren", DataType = typeof(Double), AllowDBNull = true});
            //outputDataTable.Columns.Add(new DataColumn() {ColumnName = "Steuern", DataType = typeof(Double), AllowDBNull = true});
            OutputDataTable.Columns.Add(new DataColumn() {ColumnName = "Konto", DataType = typeof(string), AllowDBNull = true});
            //outputDataTable.Columns.Add(new DataColumn() {ColumnName = "Depot", DataType = typeof(String), AllowDBNull = true});
            //outputDataTable.Columns.Add(new DataColumn() {ColumnName = "ISIN", DataType = typeof(String), AllowDBNull = true});
            OutputDataTable.Columns.Add(new DataColumn() {ColumnName = "Notiz", DataType = typeof(string), AllowDBNull = true});
            //outputDataTable.Columns.Add(new DataColumn() {ColumnName = "Wertpapiername", DataType = typeof(String), AllowDBNull = true});
        }

        public void DoExport() {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(string.Join(Delimiter, OutputDataTable.Columns.Cast<DataColumn>().Select(x => x.ColumnName)));

            foreach (DataRow row in OutputDataTable.Rows) {
                sb.AppendLine(string.Join(Delimiter,  row.ItemArray));
            }

            File.WriteAllText(Path.FullName, sb.ToString(), Encoding);
        }

    }
}

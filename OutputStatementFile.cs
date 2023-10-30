using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace MintosParser {
    class OutputStatementFile {
        private string delimiter {get; set;}
        private Encoding encoding {get; set;}

        public FileInfo path { get; set; }

        public DataTable outputDataTable { get; set; }
        public OutputStatementFile(FileInfo path) {
            this.path = path;
            outputDataTable = new DataTable();
        }

        public void setParsingOptions(string delimiter, Encoding encoding) {
            this.delimiter = delimiter;
            this.encoding = encoding;
        }

        public void prepareOutputFile() {
            outputDataTable.Columns.Add(new DataColumn() {ColumnName = "Orginales Datum", DataType = typeof(DateTime), AllowDBNull = false});
            outputDataTable.Columns.Add(new DataColumn() {ColumnName = "Datum", DataType = typeof(String), AllowDBNull = false});
            outputDataTable.Columns.Add(new DataColumn() {ColumnName = "Uhrzeit", DataType = typeof(String), AllowDBNull = false});
            outputDataTable.Columns.Add(new DataColumn() {ColumnName = "Typ", DataType = typeof(String), AllowDBNull = false});
            outputDataTable.Columns.Add(new DataColumn() {ColumnName = "Buchungswährung", DataType = typeof(String), AllowDBNull = false});
            outputDataTable.Columns.Add(new DataColumn() {ColumnName = "Wert", DataType = typeof(Double), AllowDBNull = false});;
            //outputDataTable.Columns.Add(new DataColumn() {ColumnName = "Stück", DataType = typeof(Double), AllowDBNull = true});
            //outputDataTable.Columns.Add(new DataColumn() {ColumnName = "Gebühren", DataType = typeof(Double), AllowDBNull = true});
            //outputDataTable.Columns.Add(new DataColumn() {ColumnName = "Steuern", DataType = typeof(Double), AllowDBNull = true});
            outputDataTable.Columns.Add(new DataColumn() {ColumnName = "Konto", DataType = typeof(String), AllowDBNull = true});
            //outputDataTable.Columns.Add(new DataColumn() {ColumnName = "Depot", DataType = typeof(String), AllowDBNull = true});
            //outputDataTable.Columns.Add(new DataColumn() {ColumnName = "ISIN", DataType = typeof(String), AllowDBNull = true});
            outputDataTable.Columns.Add(new DataColumn() {ColumnName = "Notiz", DataType = typeof(String), AllowDBNull = true});
            //outputDataTable.Columns.Add(new DataColumn() {ColumnName = "Wertpapiername", DataType = typeof(String), AllowDBNull = true});
        }

        public void DoExport() {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(String.Join(delimiter, outputDataTable.Columns.Cast<DataColumn>().Select(x => x.ColumnName)));

            foreach (DataRow row in outputDataTable.Rows) {
                sb.AppendLine(String.Join(delimiter,  row.ItemArray));
            }

            File.WriteAllText(path.FullName, sb.ToString(), encoding);
        }

    }
}

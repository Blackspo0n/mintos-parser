using System.Data;
using System.Text;
using Microsoft.VisualBasic.FileIO;
using MintosParser.StatementTypes;

namespace MintosParser {
    class CSVParser {
        #region properties
        private DataTable FileTable { get; set; }
        public InputStatementFile CurrentFile { get; set; }
        public string Delimiter {get; private set;} = ",";
        public Encoding Encoding {get; private set; } = Encoding.Default;
        #endregion
        
        public CSVParser(InputStatementFile file) {
            CurrentFile = file;
            FileTable = new DataTable();
        }

        public void SetParsingOptions(string delimiter, Encoding encoding) {
            Delimiter = delimiter;
            Encoding = encoding;
        }

        public void LoadCSV () {
            Console.WriteLine("Load csv file ...");
            
            TextFieldParser Reader = new TextFieldParser(CurrentFile.Path.FullName, Encoding)
            {
                TextFieldType = FieldType.Delimited,
            };
            Reader.SetDelimiters(this.Delimiter);

            string[] headers = Reader.ReadFields();

            foreach(string header in headers) {
                FileTable.Columns.Add(header);
            }

            while (!Reader.EndOfData) {
                FileTable.Rows.Add(Reader.ReadFields());
            }

        }

        public List<IStatementType> parse() {
            Console.WriteLine("Parse CSV file ...");

            var list = new List<IStatementType>();

            foreach (DataRow row in FileTable.Rows) {
                IStatementType? type = StatementTypeFactory.GetStatementType(row["Payment Type"].ToString(), row);
                if(type != null) list.Add(type);
            }

            return list;
        }
    }
}
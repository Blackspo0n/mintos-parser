using System.Data;
using System.Text;
using Microsoft.VisualBasic.FileIO;
using MintosParser.StatementTypes;

namespace MintosParser {
    class CSVParser {
        
        private DataTable FileTable { get; set; }
        
        public InputStatementFile CurrentFile { get; set; }

        public string Delimiter {get; private set;} = ",";

        public Encoding Encoding {get; private set; } = Encoding.Default;
        public CSVParser(InputStatementFile file) {
            CurrentFile = file;
            FileTable = new DataTable();
        }

        public void SetParsingOptions(string delimiter, Encoding encoding) {
            Delimiter = delimiter;
            Encoding = encoding;
        }

        public void LoadCSV () {
            var Reader = new TextFieldParser(CurrentFile.path.FullName, Encoding)
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

            Console.WriteLine("CSV file loaded successfully");
        }

        public List<IStatementType> parse() {
            var list = new List<IStatementType>();
            Console.WriteLine("Parse CSV file");

            foreach (DataRow row in FileTable.Rows) {
                IStatementType type = StatementTypeFactory.GetStatementType(row["Payment Type"].ToString(), row);
                list.Add(type);
            }

            return list;
        }
    }
}
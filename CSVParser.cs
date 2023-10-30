using System.Data;
using System.Text;
using Microsoft.VisualBasic.FileIO;
using MintosParser.StatementTypes;
using NLog;

namespace MintosParser {
    class CSVParser {
        private Logger logger = LogManager.GetCurrentClassLogger();
        
        private DataTable fileTable { get; set; }
        
        public InputStatementFile currentFile { get; set; }

        public string delimiter {get; private set;}

        public Encoding encoding {get; private set; }
        public CSVParser(InputStatementFile file) {
            currentFile = file;
            fileTable = new DataTable();
            fileTable.TableName = "StatementReport";
        }

        public void setParsingOptions(string delimiter, Encoding encoding) {
            this.delimiter = delimiter;
            this.encoding = encoding;
        }

        public void loadCSV () {
            var Reader = new TextFieldParser(currentFile.path.FullName, this.encoding);

            Reader.TextFieldType = FieldType.Delimited;
            Reader.SetDelimiters(this.delimiter);

            string[] headers = Reader.ReadFields();

            foreach(string header in headers) {
                fileTable.Columns.Add(header);
            }

            while (!Reader.EndOfData) {
                fileTable.Rows.Add(Reader.ReadFields());
            }

            logger.Info("CSV file loaded successfully");
        }

        public List<IStatementType> parse() {
            var list = new List<IStatementType>();

            logger.Info("Parse CSV file");

            foreach (DataRow row in fileTable.Rows) {
                IStatementType type = StatementTypeFactory.GetStatementType(row["Payment Type"].ToString(), row);
                list.Add(type);
            }

            return list;
        }
    }
}
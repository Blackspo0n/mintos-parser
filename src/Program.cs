using MintosParser;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Text;


namespace mintosParser {
    class Program
    {
        #region static properties
        public static OutputStatementFile? OutputFile { get; set;}
        public static InputStatementFile? InputFile { get; set; }
        public static CSVParser? Parser { get; set; }
        public static Option<string> outputEncodingOption = new Option<string>(new string[]{"--output-encoding", "-oe"}, () => "utf-8", "Output encoding of the csv file");
        public static Option<Aggregator.AggregrationSpan> aggregationOption = new Option<Aggregator.AggregrationSpan>(
            new string[] { "--aggregation", "-ag" }, () => Aggregator.AggregrationSpan.monthly, "Aggregate the statement. The statemets are normalized to the end of the aggregation date."
        );
        public static Option<string> inputEncodingOption = new Option<string>(new string[]{"--input-encoding", "-ie"}, () => "utf-8", "Input encoding of the csv file");
        //public static Option<string> inputLanguageOption = new Option<string>(new string[]{"--input-language", "-il"}, (value) => "en", true, "Langauge of the input csv file. Supported currently are: en");
        public static Option<string> inputSeperatorOption = new Option<string>(new string[]{"--input-seperator", "-is"}, () => ",", "CSV seperator of the input file");
        public static Option<string> outputSeperatorOption = new Option<string>(new string[]{"--output-seperator", "-os"}, () => ";", "CSV seperator of the output file");
        //public static Option<string> DepotNameOption = new Option<string>(new string[]{"--depot-name","-d"}, (value) => "Mintos", true, "Depot name which will be used to reference the loans.");
        public static Option<string> AccountNameOption = new Option<string>(new string[]{"--account-name","-a"}, () => "Mintos", "Account name which is used for deposits and withdraws.");
        public static Argument<FileInfo> InputFileArgument = new Argument<FileInfo>("input file", "Mintos csv input path");
        public static Argument<FileInfo> OutputFileArgument = new Argument<FileInfo>("output file",() => new FileInfo(".\\pp-import.csv"),"Output Path for Portfolio Performance csv file");
        public static RootCommand rootCommand = new RootCommand("mintos-parser transforms mintos csv statement files into csv files that can be easily imported by Portfolio Performance") {
            aggregationOption, outputEncodingOption, inputEncodingOption, inputSeperatorOption, outputSeperatorOption,AccountNameOption, InputFileArgument, OutputFileArgument
        };

        #endregion
        static void Main(string[] args)
        {
            rootCommand.SetHandler(Runner);
            rootCommand.Invoke(args);
        }

        public static void Runner(InvocationContext context) {
            InputFile = new InputStatementFile(context.ParseResult.GetValueForArgument(InputFileArgument));
            OutputFile = new OutputStatementFile(context.ParseResult.GetValueForArgument(OutputFileArgument));
            Aggregator.Aggregation = context.ParseResult.GetValueForOption(aggregationOption);
            Console.WriteLine("Inputfile: " + InputFile.Path.FullName);
            Console.WriteLine("Outputfile: " + OutputFile.Path.FullName);
            Console.WriteLine("Use aggregation " + Aggregator.Aggregation.ToString());

            Parser = new CSVParser(InputFile);
            Parser.SetParsingOptions(context.ParseResult.GetValueForOption(inputSeperatorOption) ?? ",", Encoding.GetEncoding(context.ParseResult.GetValueForOption(inputEncodingOption)??"utf-8"));
            
            try {
                Parser.LoadCSV();
            }
            catch (Exception err) {
                Console.WriteLine(err.Message, err);
                return;
            }

            var list = Parser.parse();
            OutputFile.PrepareOutputFile();

            Transformer.AccountName = context.ParseResult.GetValueForOption(AccountNameOption) ?? string.Empty;
            //Transformer.DepotName = context.ParseResult.GetValueForOption(DepotNameOption) ?? String.Empty;
        
            var aggregatedList = Aggregator.Aggregate(list);
            Transformer.Transform(aggregatedList, OutputFile);
            
            OutputFile.SetParsingOptions(context.ParseResult.GetValueForOption(outputSeperatorOption) ?? string.Empty, Encoding.GetEncoding(context.ParseResult.GetValueForOption(outputEncodingOption)??"utf-8"));
            OutputFile.DoExport();
        }
    }
}
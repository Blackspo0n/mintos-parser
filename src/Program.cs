using MintosParser;
using System.CommandLine;
using System.Text;

namespace mintosParser
{
    class Program
    {
        #region static properties
        public static OutputStatementFile? OutputFile { get; set; }
        public static InputStatementFile? InputFile { get; set; }
        public static CSVParser? Parser { get; set; }
        public static Option<string> outputEncodingOption = new("--output-encoding")
        {
            DefaultValueFactory = _ => "utf-8",
            Description = "Output encoding of the csv file",
            Aliases = { "-oe" }
        };

        public static Option<bool> summarizeOption = new("--notes")
        {
            Aliases = { "-n" },
            Description = "Generate Notes with summarize every position in the given aggregation.",
            DefaultValueFactory = _ => false
        };

        public static Option<bool> excludeUnfinishedAggregation = new("--excludeNotFinishedAggregation") {
            Aliases = { "-eua" },
            Description = "Exclude aggregations which has the end date higher than today.",
            DefaultValueFactory = _ => false
        };

        public static Option<Aggregator.AggregrationSpan> aggregationOption = new("--aggregation", "-ag") {
            Description = "Aggregate the statement. The statemets are normalized to the end of the aggregation date.",
            DefaultValueFactory = _ => Aggregator.AggregrationSpan.monthly
        };

        public static Option<string> inputEncodingOption = new("--input-encoding")
        {
            DefaultValueFactory = _ => "utf-8",
            Description = "Input encoding of the csv file",
            Aliases = { "-ie" }
        };
        
        public static Option<string> inputSeperatorOption = new("--input-seperator") {
            DefaultValueFactory = _ => ",",
            Description = "CSV seperator of the input file",
            Aliases = { "-is" }
        };
        
        public static Option<string> outputSeperatorOption = new("--output-seperator") {
            DefaultValueFactory = _ => ";",
            Description = "CSV seperator of the output file",
            Aliases = { "-os" }
        };
        
        public static Option<string> AccountNameOption = new("--account-name") {
            Description = "Account name which is used for deposits and withdraws.",
            DefaultValueFactory = _ => "Mintos",
            Aliases = { "-a" }
        };

        public static Argument<FileInfo> InputFileArgument = new("input file") {
            Description = "Mintos csv input path"
        };

        public static Argument<FileInfo> OutputFileArgument = new("output file")
        {
            DefaultValueFactory = _ => new FileInfo(".\\pp-import.csv"),
            Description = "Output Path for Portfolio Performance csv file"
        };
        public static RootCommand rootCommand = new("mintos-parser transforms mintos csv statement files into csv files that can be easily imported by Portfolio Performance") {
            aggregationOption, outputEncodingOption, inputEncodingOption, inputSeperatorOption, outputSeperatorOption,AccountNameOption, InputFileArgument, OutputFileArgument,summarizeOption, excludeUnfinishedAggregation
        };

        #endregion
        static void Main(string[] args)
        {
            Runner(rootCommand.Parse(args));
        }

        public static void Runner(ParseResult result)
        {
            if (result.Errors.Count > 0)
            {
                foreach (var error in result.Errors)
                {
                    Console.WriteLine(error.Message);
                }
                return;
            }
     
            InputFile = new InputStatementFile(result.GetValue(InputFileArgument));
            OutputFile = new OutputStatementFile(result.GetValue(OutputFileArgument));

            Aggregator.Aggregation = result.GetValue(aggregationOption);
            Aggregator.removeUnfinishedAggregations = result.GetValue(excludeUnfinishedAggregation);
            Transformer.CreateNotes = result.GetValue(summarizeOption);


            Console.WriteLine("Inputfile: " + InputFile.Path.FullName);
            Console.WriteLine("Outputfile: " + OutputFile.Path.FullName);
            Console.WriteLine("Use aggregation " + Aggregator.Aggregation.ToString());

            Parser = new CSVParser(InputFile);
            Parser.SetParsingOptions(result.GetValue(inputSeperatorOption) ?? ",", Encoding.GetEncoding(result.GetValue(inputEncodingOption) ?? "utf-8"));

            try
            {
                Parser.LoadCSV();
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message, err);
                return;
            }

            var list = Parser.parse();
            OutputFile.PrepareOutputFile();

            Transformer.AccountName = result.GetValue(AccountNameOption) ?? string.Empty;
            //Transformer.DepotName = context.ParseResult.GetValueForOption(DepotNameOption) ?? String.Empty;

            var aggregatedList = Aggregator.Aggregate(list);
            Transformer.Transform(aggregatedList, OutputFile);

            OutputFile.SetParsingOptions(result.GetValue(outputSeperatorOption) ?? string.Empty, Encoding.GetEncoding(result.GetValue(outputEncodingOption) ?? "utf-8"));
            OutputFile.DoExport();
        }
    }
}
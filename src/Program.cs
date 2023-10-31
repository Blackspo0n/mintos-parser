﻿using MintosParser;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Text;


namespace mintosParser {
    class Program
    {
        #region static properties
        public static OutputStatementFile? outputFile { get; set;}
        public static InputStatementFile? inputFile { get; set; }
        public static CSVParser? parser { get; set; }
        public static Option<string> outputEncodingOption = new Option<string>(new string[]{"--output-encoding", "-oe"}, (value) => "utf-8", true, "Output Encoding of the csv file");
        public static Option<Aggregator.AggregrationSpan> aggregationOption = new Option<Aggregator.AggregrationSpan>(new string[] { "--aggregation", "-ag" }, (value) => Aggregator.AggregrationSpan.monthly, true, "Output Encoding of the csv file");
        public static Option<string> inputEncodingOption = new Option<string>(new string[]{"--input-encoding", "-ie"}, (value) => "utf-8", true, "Input Encoding of the csv file");
        //public static Option<string> inputLanguageOption = new Option<string>(new string[]{"--input-language", "-il"}, (value) => "en", true, "Langauge of the input csv file. Supported currently are: en");
        public static Option<string> inputSeperatorOption = new Option<string>(new string[]{"--input-seperator", "-is"}, (value) => ",", true, "CSV Seperator of the input file");
        public static Option<string> outputSeperatorOption = new Option<string>(new string[]{"--output-seperator", "-os"}, (value) => ";", true, "CSV Seperator of the output file");
        //public static Option<string> DepotNameOption = new Option<string>(new string[]{"--depot-name","-d"}, (value) => "Mintos", true, "Depot name which will be used to reference the loans.");
        public static Option<string> AccountNameOption = new Option<string>(new string[]{"--account-name","-a"}, (value) => "Mintos", true, "Account name which is used for Deposits and Withdraws.");
        public static Argument<FileInfo> InputFileArgument = new Argument<FileInfo>("input file", "Mintos CSV Input Path");
        public static Argument<FileInfo> OutputFileArgument = new Argument<FileInfo>("output file","Output Path for Portfolio Performance CSV File");
        public static RootCommand rootCommand = new RootCommand("Mintos CSV parser transforms mintos CSV statementfiles into CSV files that can be easily imported by Portfolio Performance") {
            aggregationOption,outputEncodingOption, inputEncodingOption, inputSeperatorOption, outputSeperatorOption,AccountNameOption, InputFileArgument, OutputFileArgument
        };

        #endregion
        static void Main(string[] args)
        {
            rootCommand.SetHandler(Runner);
            rootCommand.Invoke(args);
        }

        public static void Runner(InvocationContext context) {
            inputFile = new InputStatementFile(context.ParseResult.GetValueForArgument(InputFileArgument));
            outputFile = new OutputStatementFile(context.ParseResult.GetValueForArgument(OutputFileArgument));
            Console.WriteLine("Inputfile: " + inputFile.path.FullName);
            Console.WriteLine("Outputfile: " + outputFile.path.FullName);

            parser = new CSVParser(inputFile);
            parser.SetParsingOptions(context.ParseResult.GetValueForOption(inputSeperatorOption) ?? ",", Encoding.GetEncoding(context.ParseResult.GetValueForOption(inputEncodingOption)??"utf-8"));
            
            try {
                parser.LoadCSV();
            }
            catch (Exception err) {
                Console.WriteLine(err.Message, err);
                return;
            }

            var list = parser.parse();
            outputFile.PrepareOutputFile();

            Transformer.AccountName = context.ParseResult.GetValueForOption(AccountNameOption) ?? String.Empty;
            //Transformer.DepotName = context.ParseResult.GetValueForOption(DepotNameOption) ?? String.Empty;
        
            //outputFile.outputDataTable = Compactor.Compact(outputFile.outputDataTable);
            Aggregator.Aggregation = context.ParseResult.GetValueForOption(aggregationOption);

            Console.WriteLine("Use aggregation " + Aggregator.Aggregation.ToString());
            var aggregatedList = Aggregator.Aggregate(list);
            Transformer.Transform(aggregatedList, outputFile);
            //outputFile.outputDataTable = Aggregator.Normalize(outputFile.outputDataTable);
            
            outputFile.SetParsingOptions(context.ParseResult.GetValueForOption(outputSeperatorOption) ?? String.Empty, Encoding.GetEncoding(context.ParseResult.GetValueForOption(outputEncodingOption)??"utf-8"));
            outputFile.DoExport();
        }
    }

}
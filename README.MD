 # mintos Parser
🛠 In active development 🛠

This console application transforms a given account statement file from mintos into a functional portfolio performance file to import asinto anaccount (Konto in german).

The input statements must be csv files. This program includes the most statement types (e.g. taxas, interesst, bonus, deposits).
> It is pretty early in development. The goal is to provide additional parameters for the aggregation and even a solution to track each individuals loans. Also I want to add language parameters to be independend of the input format. 

 ## Usage
 
```cli
Usage:
  mintos-parser <input file> [<output file>] [options]

Arguments:
  <input file>   Mintos CSV Input Path
  <output file>  Output Path for Portfolio Performance CSV File [default: .\pp-import.csv]
```

## Options
To make it more flexible the program provides some options for formatting and configurating the output file.
```cli
  -ag, --aggregation <daily|monthly|quarterly|yearly>  Aggregate the statement. The statemets are normalized to the end of the aggregation date. [default: monthly]
  -oe, --output-encoding <output-encoding>             Output Encoding of the csv file [default: utf-8]
  -ie, --input-encoding <input-encoding>               Input Encoding of the csv file [default: utf-8]
  -is, --input-seperator <input-seperator>             CSV Seperator of the input file [default: ,]
  -os, --output-seperator <output-seperator>           CSV Seperator of the output file [default: ;]
  -a, --account-name <account-name>                    Account name which is used for Deposits and Withdraws. [default: Mintos]
```

## Examples
To generate a monthly report:
```cli 
mintos-parse.exe .\account-statement.csv .\pp-import.csv -ag monthly
```

To generate yearly report with different output seperator:
```cli 
mintos-parse.exe .\account-statement.csv .\pp-import.csv -ag yearly -os \t # stands for \t for tab
```

## License
Copyright &copy; 2023 by Mario Kellner. Released under the [MIT license](https://github.com/Blackspo0n/mintos-parser/blob/main/LICENSE).

## Road map
### The following features are already in an useable state:

* [X] Options for commandline
  * [X] Encoding and Delimiter for both output and input
* [X] Taxes and bonuses
* [X] All position displayed in notes
* [X] Agregate statements
  * [X] daily
  * [X] monthly
  * [X] quarterly
  * [X] yearly

### The following features are active in development:

* [ ] Lot of statement types are missed because I don´t have all in my own statement file
* [ ] Tracking individual loans (not aggregated) with a corresponding Depot depot
    > Actually this was the first thing I tried but due to the restriction that every depot investment needs a specified amount this wasn´t going to work. I desided to "ignore" the investments and the principal returned on my account for now.
* [ ] Language detection and usage for csv file
* [ ] Option for aggregation=none
    > I need a solution for this. Currently it would ending up into many position with 0,00 because PP would round it down.

## Development
This is a CSharp project. If you want to contribute or build it by your own you need at least .NET SDK and VS Code.
You can develop in Visual Studio Code too.

## Disclaimer
[Portfolio Performance](https://www.portfolio-performance.info) is a software product for individuals with finance background. Although I try my best to provide correct values, the restriction of the "Wert" field only allowing two additional diggest can provide rounding differences. Double check the official numbers first!

A am not affiliated with portfolio performance. I am just an enthusiast which want to track down my portfolio as correct as I can.
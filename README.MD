 # mintos Parser
🛠 In active development 🛠


This console application transforms a given account statement file from mintos into a functional portfolio performance file to import as an account (Konto in german).

The input statements must be csv files. This program includes the most statement types (e.g. taxas, interesst, bonus, deposits).

>At this stage of development the program only aggregate on per month base. Its pretty early in development. The goal is to provide additional parameters for the aggregation and even a solution to track each individuals loans.

 ## Usage
 
```cli
Usage:
  mintosparser <input file> <output file> [options]

Arguments:
  <input file>   Mintos CSV Input Path
  <output file>  Output Path for Portfolio Performance CSV File
```

## Options
To Make it more Flexible the programm provides some options for formating.
```cli
  --loglevel <loglevel>                       [default: Info]
  -oe, --output-encoding <output-encoding>    Output Encoding of the csv file [default: utf-8]
  -ie, --input-encoding <input-encoding>      Input Encoding of the csv file [default: utf-8]
  -is, --input-seperator <input-seperator>    CSV Seperator of the input file [default: ,]
  -os, --output-seperator <output-seperator>  CSV Seperator of the output file [default: ;]
  -a, --account-name <account-name>           Account name which is used for Deposits and Withdraws. [default: Mintos]
  --version                                   Show version information
```

## License
Copyright &copy; 2023 by Mario Kellner. Released under the [MIT license](https://github.com/Blackspo0n/mintos-parser/blob/main/LICENSE).

## Road map
### The following features are already in a useable state:

* [X] Aggregate Monthly statements
* [X] Options for commandline
  * [X] Encoding and Delimiter for both output and input
* [X] Taxes and bonuses
* [X] All position displayed in notes   

### The following features are active in development:

* [ ] Lot of statement types are missed because I don´t have all in my own statement file
* [ ] Tracking individual loans (not aggregated) with a corresponding Depot depot
    > actually this was the first thing I tried but due to the restriction that every depot security need a specified amount this wasn´t going to work. I desided to "ignore" the investments and the principal returned on my account for now.
* [ ] Agregate
  * [ ] daily
  * [ ] quartly
  * [ ] yearly
* [ ] Language detection and usage for csv file

## Development
This is a cSharp project. if you want to contribute or build it by your own you need at least .NET SDK and VS Code.
You can develop in Visual Studio Code too.

## Disclaimer
[Portfolio Performance](https://www.portfolio-performance.info) is a software product for individuals with finance background. Although I try my best to provide correct values, the restriction of the "Wert" field only allowing two additional diggest can provide rounding differences. Double check the official numbers first!

A am not affiliated with portfolio performance. I am just an enthusiast with want to track down my portfolio as correct as I can.


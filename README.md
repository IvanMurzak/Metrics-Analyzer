# Metrics Analyzer

![___](https://img.shields.io/badge/.NET_8.0-blue.svg)
![___](https://img.shields.io/badge/CSV-blue.svg)
![___](https://img.shields.io/badge/Data_Analyzer-blue.svg)



### Requirements

- 👉 [Install](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) `.NET 8.0 Runtime`


### Get started

 - Depends on your operation system open related folder.
 - Double click on the app file `Metrics-Analyzer`
 - Type command and press enter
 
 ```
 analyze app-companies app-financial-metrics
 ```

You may use `-h` or `--help` command to get description about command line interface.
Also, you may run this app through standard terminal with adding arguments. Example:

```
Metrics-Analyzer analyze app-companies app-financial-metrics
```


# Project structure

- `./Metrics-Analyzer/Commands` console commands, these classes will be executed as only user uses related command.
- `./Metrics-Analyzer/Console` helpful classes for beautiful printin to a console.
- `./Metrics-Analyzer/Data` models that app uses in runtime for processing data in more efficient way.
- `./Metrics-Analyzer/Data/CSV` models that represent CSV files.
- `./Metrics-Analyzer/Processors` bunch of data processors.
- `./Metrics-Analyzer/Program.cs` entry point into the app.

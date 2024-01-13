# Metrics Analyzer

![___](https://img.shields.io/badge/.NET_8.0-blue.svg)
![___](https://img.shields.io/badge/CSV-blue.svg)
![___](https://img.shields.io/badge/Data_Analyzer-blue.svg)

![WindowsTerminal_2ThUenHOUo](https://github.com/IvanMurzak/Metrics-Analyzer/assets/9135028/8d9b21e5-351e-4ccd-b9c2-1a7019104e16)

Demo project. This is the solution for this [task](https://github.com/IvanMurzak/Metrics-Analyzer/blob/main/Sanlo%20Coding%20Challenge_%20Invest%20in%20App%20Company%20Revenues.pdf)

### Requirements

- 👉 [Install](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) `.NET 8.0 Runtime`


### Get started

 - Depending on your operation system open a related folder.
 - Double click on the app file `Metrics-Analyzer`
 - Type command and press enter (make sure `.csv` files are in the same folder as the app file)
 
 ```
 analyze app-companies app-financial-metrics
 ```

You may use `-h` or `--help` command to get a description of the command line interface.
Also, you may run this app through a standard terminal with adding arguments. Example:

```
Metrics-Analyzer analyze app-companies app-financial-metrics
```


# Project structure

- `./Metrics-Analyzer/Commands` console commands, these classes will be executed as only a user uses a related command.
- `./Metrics-Analyzer/Console` helpful classes for beautiful printing to a console.
- `./Metrics-Analyzer/Data` models that the app uses in runtime for processing data in a more efficient way.
- `./Metrics-Analyzer/Data/CSV` models that represent CSV files.
- `./Metrics-Analyzer/Processors` bunch of data processors.
- `./Metrics-Analyzer/Program.cs` entry point into the app.

## GPT - Metrics Analyzer app icon

![app-icon](https://github.com/IvanMurzak/Metrics-Analyzer/assets/9135028/77dcbc3c-16be-4d3e-a450-8e8fa85d35ad)

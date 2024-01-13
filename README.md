# Metrics Analyzer

![___](https://img.shields.io/badge/.NET_8.0-blue.svg)
![___](https://img.shields.io/badge/CSV-blue.svg)
![___](https://img.shields.io/badge/Data_Analyzer-blue.svg)

![WindowsTerminal_2ThUenHOUo](https://github.com/IvanMurzak/Metrics-Analyzer/assets/9135028/8d9b21e5-351e-4ccd-b9c2-1a7019104e16)

Demo project. This is the solution for this [task](https://github.com/IvanMurzak/Metrics-Analyzer/blob/main/Sanlo%20Coding%20Challenge_%20Invest%20in%20App%20Company%20Revenues.pdf). 

### [GitHub link](https://github.com/IvanMurzak/Metrics-Analyzer).

## Requirements

- 👉 [Install](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) `.NET 8.0 Runtime`


## Get started

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
- `./Metrics-Analyzer/Data/CSV` models that represent CSV files.
- `./Metrics-Analyzer/Data/Runtime` models for runtime calculations. They are optimized to allow faster calculations.
- `./Metrics-Analyzer/Processors` contains all data processors of the project. Each specific processor contains nested class that represents data model for it's processing result.
- `./Metrics-Analyzer/Program.cs` entry point into the app.

# Q / A

> How long did you spend working on the problem? What was the most challenging part for you to
solve?

Three hours for the exact problem solving. Rest of the time I spent to make everything pretty. Such as: console output, README file, create builds for different platforms.

---

> How would you modify your data model to account for new risk signals that could be added to
improve accuracy of determining credit risk?

My data model is easily scalable for new parameters. I made `AppProcessor.Const.cs` that contains all constants and ranges for calculations. It could be easily updated as well. 

The project has two layers of models. The first one represents CSV format data. Another one represents data model that are more efficient for runtime calculation and has a bit different format. So to add a new fields into a model I will need to add in multiple places. Also, into a `DataParser.cs` that is responsible for data conversion between CSV and Runtime models.

---

> Discuss your solution’s runtime complexity.

The processing functions is here `AppProcessor.cs` line 25.

Just because I convert CSV data to my Runtime data model that has much more efficient format for runtime performance. The complexity for exact data processing is `O(N)`. The data conversion takes another one `O(N)`, but I am not sure it should be calculated, because the conversion could be recognized as part of data reading, which one is not included in complecity calculation as I know. The most time consuming part of this app is `printing data to a console` in pretty tree formated way.

If I had more time I would implement multithreaded solution that may increase perfromance for bigger data sets. Also, I would implement streaming data processing. That would reduce memory consumption. Because right now an entire data set should be loaded, processed, converted into result format and unloaded. That is a lot of RAM.

## GPT - Metrics Analyzer app icon

![app-icon](https://github.com/IvanMurzak/Metrics-Analyzer/assets/9135028/77dcbc3c-16be-4d3e-a450-8e8fa85d35ad)

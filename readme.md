# Der gierige Backpacker

This is a C# implementation of a greedy approximation for the [knapsack problem](https://en.wikipedia.org/wiki/Knapsack_problem) as part of the [get int {IT} coding challenge](https://www.get-in-it.de/coding-challenge).

## Installation

### Windows

- Download and install the [.NET Core 3.1 Runtime](https://download.visualstudio.microsoft.com/download/pr/d97cfaf4-b17f-46c7-9a11-7f0d25dfd8b0/f76d4fce8e38b289efb9403aab0a0c9f/dotnet-runtime-3.1.5-win-x64.exe)
- Download the [Windows version of the application](https://github.com/sebastianlay/der-gierige-backpacker/releases/download/1.0.0/der-gierige-backpacker-win-x64.zip) from the releases and extract the content

### Linux

- Download and install the [.NET Core 3.1 Runtime](https://docs.microsoft.com/de-de/dotnet/core/install/linux)
- Download the [Linux version of the application](https://github.com/sebastianlay/der-gierige-backpacker/releases/download/1.0.0/der-gierige-backpacker-linux-x64.zip) from the releases and extract the content

## Usage

- Open a console or PowerShell and navigate to the folder with the extracted application
- Run the application with the following optional arguments:

```
der-gierige-backpacker:
  A program to solve the bounded knapsack problem

Usage:
  der-gierige-backpacker [options]

Options:
  --filepath <filepath>    Path to the CSV file containing the items [default: code_for_bwi.csv]
  --trucks <trucks>        Comma-separated list of maximum truck capacities (in g) [default: 1027600,1014300]
  --version                Show version information
  -?, -h, --help           Show help and usage information
```

## Credits

This application uses the following libraries:

- [CsvHelper](https://joshclose.github.io/CsvHelper/) for reading and mapping the content of a CSV file
- [System.CommandLine.DragonFruit](https://github.com/dotnet/command-line-api) for parsing the command line arguments

## Solution

This greedy approximation for the bounded knapsack problem is way faster than brute forcing all possible combinations. However it does not guarantee to get the optimal solution.
The solution for this particular CSV file can be found [in a separate file](https://github.com/sebastianlay/der-gierige-backpacker/blob/1.0.0/solution.md).
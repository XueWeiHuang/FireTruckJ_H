# FireTruck

UVA208 solved in C#. This is one of UVA challenges. Most of solutions are based on Java or C++. I solved it C# WPF. The logic I implemented
was to conduct DFS frist to find eligible routes, and then I used Floydd algorithm to make judgements which is serving as pruning. it got
rid of unnecessary routes to greatly improve the application performance and meet the requirement of processing time. Simple DFS will do
the work, but Floydd pruning further improves the program and make it faster. 
Altough this program can be optimized in MVVM pattern, the main idea here is to optimize the performance rather than design pattern.

### Prerequisites

1. Visual Studio 2017
2. Most recent .NET Framework

### Installing

1. Download or clone the repo
2. Ensure the data file for map is inclued within program
3. execute the program


## Authors

* [Xuewei Huang](https://github.com/XueWeiHuang)



## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details

## Acknowledgments

*Xuewei Huang

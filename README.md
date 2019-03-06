# Crawler

[![CircleCI](https://circleci.com/gh/kitforbes/Crawler/tree/master.svg?style=svg)](https://circleci.com/gh/kitforbes/Crawler/tree/master)

A simple web crawler.

## Usage

From a Windows operating system, execute either `.\build.cmd` or `.\build.ps1`. This will build and run a Docker container based on `microsoft/dotnet`.

On Linux or Mac, execute the below commnds to build and run the container.

```bash
docker build -t crawler ./Crawler
dotnet run --rm crawler --project ./Crawler
```

### Requirements

* Docker v18+

## Development Requirements

* DotNet Core SDK v2.2+
* Docker v18+

## TODO

* Add a class library to hold classes and functions to extract out of the console application
* Add `xUnit` test project to test the class library
* Add `build.sh` for Linux/Mac development
* Add `Vagrantfile` to create a development instance containing development dependencies
* Add CLI options to control the URL being crawled and whether or not to include sub domains

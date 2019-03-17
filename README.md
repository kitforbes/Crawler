# Crawler

[![CircleCI](https://circleci.com/gh/kitforbes/Crawler/tree/master.svg?style=svg)](https://circleci.com/gh/kitforbes/Crawler/tree/master)

A simple web crawler to construct a sitemap-like JSON string.

## Overview

This project will start from a single domain (currently hard coded to https://chris-forbes.com), and find all internal links, external links and images on the page before continuing to one of the internal links. It currently treats sub-domains as external links.

Asynchronous behaviour was disabled, as this resulted in an inconsistent number of visited pages on subsequent tests. Identifying the reason for this could help improve performance and execution time of the application.

An actual test project has not yet been created due to time constraints. The primary method for testing is the traditional "does it compile?" approach by running the application through a continuous integration system ([CircleCI](https://circleci.com/)).

Given additional time, I would:
- add unit tests with an `xUnit` project.
- ~~add support for URL specification over the command line.~~
- add support for subdomains over the command line.
- replace the `if/else` statements for URL interpretation with a `switch` statment, relying on regular expressions.
- add a build script for Linux/Mac.
- add a `Vagrantfile` to describe an appropriate development environment.

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

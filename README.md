# Sitecore Azure Setup

This repository contains an WebRole Entry Point that provides methods to run Sitecore code when a role instance is initialized.

[![NuGet version](https://img.shields.io/nuget/v/Sitecore.Azure.Setup.svg)](https://www.nuget.org/packages/Sitecore.Azure.Setup/)

## Preconfigured Items:

+ The App_Data directory with included Sitecore's `\Data` content;
+ Patched the `dataFolder` variable that looks at the `\App_Data` directory;
+ Patched the `<initialize>` pipeline with a processor tp ebuld search indexes.

## Features:

+ Rebuild Search Indexes on very first start of a WebRole Instance.

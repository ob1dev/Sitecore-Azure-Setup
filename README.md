# Sitecore Azure Startup

This repository contains an WebRole Entry Point that provides methods to run Sitecore code when a role instance is initialized.

## Preconfigured Items:

+ The App_Data folder with updated path to dataFolder variable;
+ Sitecore settings for scaled environment;
+ Sitecore SQL Server Session State provider.

## Features:

+ Grand permission in the file system on a WebRole Instance;
+ Rebuild Search Indexes on very first start of a WebRole Instance.

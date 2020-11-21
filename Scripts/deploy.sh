#!/bin/sh
API_KEY = $1

dotnet nuget push ./YY.TechJournalReaderAssistant/bin/Release/YY.TechJournalReaderAssistant.*.nupkg -k $1 -s https://api.nuget.org/v3/index.json --skip-duplicate
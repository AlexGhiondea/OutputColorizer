#!/bin/bash

dotnet restore

dotnet build

dotnet test --no-build test/OutputColorizer.Tests.csproj

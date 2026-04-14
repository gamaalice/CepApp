# CEP Integration App (.NET)

Desktop application built with .NET WinForms that consumes the ViaCEP API and stores results in a local SQLite database.

## Features

- Search address by CEP
- Consume ViaCEP API
- Display Rua, Cidade, UF
- Save results to SQLite
- List saved addresses

## Tech

- C#
- .NET WinForms
- HttpClient
- SQLite
- API: viacep

## Run

```bash
dotnet run
Build Executable
dotnet publish -c Release -r win-x64 --self-contained true

Executable:

/bin/Release/net8.0-windows/win-x64/publish/CepApp.exe

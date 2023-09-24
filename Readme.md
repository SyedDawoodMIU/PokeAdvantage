# PokeAdvantage

## Overview

PokeAdvantage is a console utility designed to help Pokemon trainers understand their strengths and weaknesses. Using modern design patterns and best practices, this utiltity fetches data from a Pokemon API and processes it to give its strengths and weaknesses according to Pokemon types.

## Technologies Used

- C#
- .NET Core
- RESTful APIs
- Design Patterns (Strategy, Singleton, Observer, Builder, Adapter)
- Dependency Injection

## Features

- Fetch and display a Pokemon's strengths and weaknesses based on its type.
- Extensible and modular architecture.
- Error handling and logging.
- Unit tests.
  
## Prerequisites

- .NET Core SDK
- IDE like Visual Studio or Visual Studio Code
- Git (for source code management)

## Installation

1. Clone the repository from GitHub.

```bash
git clone https://github.com/SyedDawoodMIU/Stuller.git
```

2. Navigate to the project directory.

```bash
cd PokeAdvantage
```

3. Restore the NuGet packages.

```bash
dotnet restore
```

4. Build the project.

```bash
dotnet build
```

5. Run the project.

```bash
dotnet run
```

## Testing

To run the unit tests, navigate to the `PokeAdvantage.Tests` directory and execute the following command:

```bash
dotnet test
```

## Contributing

Please create pull requests or issue for major changes.

## License

MIT License

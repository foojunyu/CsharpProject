# Malaysia Bulletins C# Application

A C# console application that fetches and displays various Malaysian government bulletins from the data.gov.my Open Data API.

## Features

- Fetches multiple types of Malaysian bulletins including:
  - Earthquake Data
  - Weather Forecasts
  - Insect Control Bulletins
  - Public Services Bulletins
  - Fuel Price Updates
  - Air Quality Index Data
- Clean architecture with separation of concerns (Models, Services)
- Async/await for efficient API calls
- Comprehensive error handling
- Extensible design for adding more bulletin types

## Prerequisites

- .NET 8.0 SDK or later
- Internet connection to access data.gov.my API

## Installation

1. Clone the repository:
```bash
git clone https://github.com/foojunyu/CsharpProject.git
cd CsharpProject/MalaysiaBulletins
```

2. Restore dependencies:
```bash
dotnet restore
```

## Building

Build the application using:
```bash
dotnet build
```

## Running

Execute the application with:
```bash
dotnet run
```

The application will:
1. Display all available bulletin types
2. Fetch data from each bulletin endpoint
3. Display sample records for each bulletin type

## Project Structure

```
MalaysiaBulletins/
├── Models/
│   ├── ApiResponse.cs          - Generic API response wrapper
│   ├── BulletinData.cs         - Bulletin data model
│   ├── BulletinMetadata.cs     - Bulletin metadata model
│   └── DataGovMyResponse.cs    - API-specific response model
├── Services/
│   ├── IBulletinService.cs     - Service interface
│   └── BulletinService.cs      - Service implementation
├── Program.cs                   - Application entry point
└── MalaysiaBulletins.csproj    - Project configuration
```

## API Information

This application uses the official Malaysia Open Data API:
- **Base URL**: https://api.data.gov.my
- **Documentation**: https://developer.data.gov.my/
- **No API key required** for basic usage

## Bulletin Types

The application currently supports the following bulletin types:

| ID | Title | Agency | Category |
|----|-------|--------|----------|
| earthquake | Earthquake Data | Malaysian Meteorological Department | Weather & Environment |
| weather_forecast | Weather Forecast | Malaysian Meteorological Department | Weather & Environment |
| kawalan_serangga | Insect Control Bulletin | Ministry of Health | Health & Safety |
| public_services_bulletin | Public Services Bulletin | Various Government Agencies | Public Services |
| fuelprice | Fuel Price Bulletin | Ministry of Finance | Economy & Finance |
| air_quality | Air Quality Index Bulletin | Department of Environment | Weather & Environment |

## Extending the Application

To add more bulletin types, update the `_knownBulletins` list in `Services/BulletinService.cs`:

```csharp
new BulletinMetadata
{
    Id = "your_bulletin_id",
    Title = "Your Bulletin Title",
    Description = "Description of the bulletin",
    Agency = "Government Agency Name",
    Category = "Category Name"
}
```

## Error Handling

The application includes comprehensive error handling for:
- Network connectivity issues
- API endpoint errors
- JSON parsing errors
- Empty or invalid responses

## Dependencies

- Microsoft.Extensions.Http (10.0.1) - HTTP client factory
- System.Text.Json (built-in) - JSON serialization

## License

This project is open source and available under the MIT License.

## About Data Source

This application uses data from Malaysia's official open data portal (data.gov.my), which provides access to various government datasets including bulletins, statistics, and real-time information from federal and state agencies.

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## Support

For issues related to the API data, please refer to:
- Official API Documentation: https://developer.data.gov.my/
- Data Catalogue: https://data.gov.my/data-catalogue

For issues related to this application, please open an issue on GitHub.

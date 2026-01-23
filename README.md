# ASSET TRACKER

## Features

### Core Functionality
- Add, edit, delete, and view assets
- Automatic currency conversion per office
- Sorting by ID, brand, category, date, or office
- Highlighting of soon‑to‑expire assets
- Summary reports (per office, total value of assets per office and number of assets per office)

### Technical
- Built with C# and .NET
- Uses Entity Framework Core with SQL Server

#### Database
The application uses SQL Server via EF Core.
Tables include:
- Assets
- Categories
- Offices

Each asset references:
- a Category
- an Office

## How to Run
- Clone the repository
- Run `update-database` from the Package Manager Console.
- Run the project

The database will be created automatically and seeded on the first run.

## Future Improvements
- Export reports to CSV
- Add search/filtering
- Add pagination for large datasets

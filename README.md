# BookStore---backend
Back end for book store application
# Book store

## Run locally

- Clone the repository
- Open SalesMonitor.sln with an up to date version of VisualStudio 2019
- Select 'IIS Express' next to the green play symbol and press F5

## Configuration
- Change **ConnectionStrings:DefaultConnection** to point to your local server db

## Creating databse
- Use db backup file and import it to db you put in connection string
- Or open package manager console and run following commands (`add-migration MyFirstMigration` and after it `Update-Database`)

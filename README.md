# Tuxboard Examples

Tuxboard is a lightweight dashboard library specifically for the ASP.NET Core platform. It was meant to be a Lego-style way to build dashboards.

I'm continually adding more examples through the articles on my [blog](https://www.danylkoweb.com/tuxboard) and using this repository for the "dropzone." (don't worry...there's more coming)

The library is located [here](https://github.com/jdanylko/Tuxboard) and is available through NuGet [here](https://www.nuget.org/packages/Tuxboard.Core).

## Technology Stack
- ASP.NET Core 6.0 or higher (using C#)
- Entity Framework Core

## Examples
The repository contains the following examples:
- **01-SimpleDashboard**<br/>The simplest way to create a static dashboard; VERY basic; no JavaScript with no features ([related post](https://www.danylkoweb.com/Blog/introducing-tuxboard-SY)) <br/><br/>
- **02-WidgetsExample**<br/>A simple dashboard with a simple widget ([related post](https://www.danylkoweb.com/Blog/dashboard-modularity-TD))<br/><br/>
- **03-DragWidgets**<br/>Using TypeScript to move Widgets around on the dashboard ([related post](https://www.danylkoweb.com/Blog/moving-widgets-in-tuxboard-TE))<br/><br/>
- **04-Tuxbar**<br/>Demonstrates how to create a complimentary toolbar for Tuxboard ([related post](https://www.danylkoweb.com/Blog/creating-a-tuxbar-for-tuxboard-TL))<br/><br/>
- **05-Layout-1**<br/>Create a simple layout dialog so users can adjust how their dashboard is structured ([related post](https://www.danylkoweb.com/Blog/managing-layouts-in-tuxboard-simple-layout-dialog-U2))<br/><br/>
- **06-Layout-2**<br/>Create an advanced layout dialog for more complex layouts ([related post](https://www.danylkoweb.com/Blog/managing-layouts-in-tuxboard-advanced-layout-dialog-U3))<br/><br/>
- **07-Add-Widgets**<br/>Create an Add Widget dialog ([related post](https://www.danylkoweb.com/Blog/adding-widgets-with-a-tuxboard-dialog-U4))<br/><br/>
- **08-Widget-Toolbar**<br/>Adding buttons and dropdown to a widget's header ([related post](https://www.danylkoweb.com/Blog/using-widget-toolbars-or-deleting-widgets-U6))<br/><br/>
- **09-User-Dashboard**<br/>Create user-specific dashboards when users log in ([related post](https://www.danylkoweb.com/Blog/creating-user-specific-dashboards-U7))<br/><br/>
- **10-Default-Dashboards**<br/>Create role-specific dashboards when a user logs in ([related post](https://www.danylkoweb.com/Blog/creating-default-dashboards-using-roles-U8))<br/><br/>
- **11-Default-Widgets**<br/>Create role-specific widgets ([related post](https://www.danylkoweb.com/Blog/creating-default-widgets-using-roles-UA))<br/><br/>
- **12-Creating-Widgets**<br/>Create various types of widgets (coming soon)<br/><br/>

## Running Examples in Docker containers

Each example has a Dockerfile that can be used to build a Docker image that can be used to run the examples in a Docker container.

Here is how to run these examples, using the Simple Dashboard as an example.


1. Set an environment variable TUXBOARDCONFIG__CONNECTIONSTRING. On Linux/Mac terminals, this would be like this, replacing the IP address, database name, username and password of your SQL Server database: `export TUXBOARDCONFIG__CONNECTIONSTRING='Data Source=IP_ADDRESS;Initial Catalog=DATABASE_NAME;Integrated Security=false;MultipleActiveResultSets=True;TrustServerCertificate=True;User Id=USERNAME;Password=PASSWORD'`
2. Go into the 01-SampleDashboard folder and create a new file called development.env containing the following:
```
TUXBOARDCONFIG__CONNECTIONSTRING=CONNECTION STRING FROM STEP 2
ASPNETCORE_ENVIRONMENT=Development
```
3. Go into the 01-SimpleDashboard folder and run the following command `docker build -t simpledashboard -f Dockerfile .`
4. Run `dotnet ef migrations add InitialCreate` to initial the database migrations.
5. Run `dotnet ef database update` to apply the database migrations to the database.
6. Run the docker container with the following command `docker run --name dash -d --env-file development.env -p 8080:8080 simpledashboard`

To run a different example, delete the database created in step #3 and run steps #2 - #6 changing the name of the docker container from simpledashboard to another name to match the example.
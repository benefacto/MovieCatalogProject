Prerequisites: 
-- Updated Visual Studio including:
Microsoft .NET Framework 4.7.1 Developer Pack (Runtime & Targeting Pack),
IIS Express, and Windows Communication Foundation
-- A modern, clean web browser without potentially interfering add-ons:
i.e. Google Chrome, Mozilla Firefox, or Microsoft Edge

To run the application, perform the following steps:
1. Select "Clone or download" button on Github repo and then "Download Zip"
2. Extract ZIP archive to desired directory
3. Run Visual Studio as administrator and open MovieCatalog.sln file
4. Select top menu "Build" and click "Rebuild Solution"
5. Wait for rebuild to be confirmed as successful in bottom output window
6. Open the Service.svc.cs file in the MovieCatalogService project,
then select top menu "Debug" and click "Start Without Debugging"
5. Open the _ViewStart.cshtml file in the MovieCatalogService project,
then select top menu "Debug" and click "Start Without Debugging"
6. Navigate to http://localhost/app/ in web browser
7. View movies in grid, sort, or double click on cells and type to update fields

Current level of functionality:
-- Reads MovieCatalog.json file and displays movies to user
-- Perform updates on movies read from MovieCatalog.json file
-- Only interaction with movies in grid is currently supported
(hyperlinks are placeholders for future pages/functionality)

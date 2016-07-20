# MediumSample
AppOnlySPOSample

## Description ##
This is an example to demonstrate how to communicate SPO throught REST API using App-only authentication under Azure AD

## Prerequsive ##
* Json.Net
* ADAL library

## Configuration ##
* Create Azure AD Application
* Configure Azure AD App to have app-only authenciation by following [Build service and daemon apps in Office 365](https://msdn.microsoft.com/en-us/office/office365/howto/building-service-apps-in-office-365 "Build service and daemon apps in Office 365")
* Provide a setting.settingjson file for your Azure AD configuration. You can hard code settings in your code.
* Open the solution in VS2015 and run it. 

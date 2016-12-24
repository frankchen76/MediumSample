# MediumSample
AppOnlySPOSample

## Description ##
This is an example to demonstrate how to communicate SPO throught REST API using App-only authentication under Azure AD

## Prerequsive ##
* Json.Net
* ADAL library

## Prepare Certificate ##
* Run the following command line to create certificate in "My" location
```makecert -r -pe -n "CN=TestCN AzureADAppOnlyCert" -b 12/01/2016 -e 12/30/2020 -len 2048 -ss my```
* Generate .PFX with private key
* Genreate .cer
* you can find the generated certficate from cer folder. the password is 1234

## Configuration ##
* Create Azure AD Application
* Configure Azure AD App to have app-only authenciation by following [Build service and daemon apps in Office 365](https://msdn.microsoft.com/en-us/office/office365/howto/building-service-apps-in-office-365 "Build service and daemon apps in Office 365")
* Provide a setting.settingjson file for your Azure AD configuration. You can hard code settings in your code with the following format.    
```javascript
{    
  "tenantId": "[tenant id]",
  "clientId": "[clientid]",
  "resourceId": "https://[tenantname].sharepoint.com/",
  "resourceUrl": "https://[tenantname].sharepoint.com/_api/web/Lists/getbytitle('[listname]')",
  "certficatePath": "..\\..\\cert\\AzuerADAppOnlyTest.pfx",
  "certificatePassword": "[cert private key]"
}
```
* Open the solution in VS2015 and run it. 

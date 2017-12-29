# MediumSample
SPOSiteCollectionReadOnly

## Description ##
This is an example to demonstrate how to set a SPO site collection read-only by using a site policy and CSOM

## Prerequsive ##
* Json.Net
* CredentialsManagement
* Microsoft.SharePointOnline.CSOM

## Configuration ##
* Open the solution from Visual Studio 2015/2017
* Create a sposettings.json with the following format: 
```javascript
{
  "credentialTarget": "[name in windows credential managment]",
  "url": "[site url]",
  "policyName": "[Site Policy used for site collection]"
}

* Change sposettings.json property to select "Copy always" or "Copy if newer" for "Copy to Output directory".
* Create a generic credential under control panel -> Credential Manager.
* Make sure you have a site policy available either on your site collection or published by Content Type Hub site collection
* Press F5 to run your code
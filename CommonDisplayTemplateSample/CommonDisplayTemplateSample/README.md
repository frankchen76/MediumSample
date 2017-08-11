# MediumSample
CommonDisplayTemplateSample

## Description ##
This is an example to demonstrate how to inject customization into a common item body display template. 

## Prerequsive ##
* jQuery
* SharePoint Display Template

## Configuration ##
* Goto your on-premises or SPO search center
* Copy DisplayTemplate\Item_HybridSearchItem_Body.html to [search center]\_catalogs\masterpage\Display Templates\Search
* Copy PublishingImages\*.gif and *.jpg to [search center]\Style%20Library\Search if you want to see the progress images.
* Go to your search center and type "IsExternalContent:true" in the search box to get all hybrid search result. you will see the logics injected in the common item body display template.

##folder structure
* DisplayTemplate: includes display template
* PublishingIamges: includes images needed for display template. 
* SampleCode: include the code snippets
* WebPart: include the modified the Search Result Web Part Xml definition 


Sitefinity-Custom-Checkout sample project.

ABOUT

In this sample you can see how you can create your own custom checkout widget. 

It is highly recommended to not use this sample as it is since it has limitations for example:

1) always picks the first payment method there is no payment method seleciton
2) changing of country and/or zip codes doesnt update the available shipping options
3) the combo box with credit card information is hardcoded and is not taken from any payment method.


INSTALLATION 

1) you need to update the reference dlls to the versions inside your sitefinity web application bin folder: Telerik.Web.UI, Telerik.Sitefinity, Telerik.Sitefinity.Model, Telerik.Sitefinity.Ecommerce, Telerik.OpenAccess
2) you need to rebuild the project with the fixed reference librararies. The latest sample update is for version Sitefinity version 6.2
3) you need to copy the generated dll inside your web applicaiton bin folder.
4) you need to register the Sitefinity-Custom-Checkout a.k.a. One step checkout widget as described here : http://www.sitefinity.com/documentation/documentationarticles/installation-and-administration-guide/system-settings/registering-a-new-widget-in-sitefinity

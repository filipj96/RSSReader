# RSSReader
This project involves creating a RSS feed reader that uses a multitier architecture to seperate the and in this case the "Repository pattern",
1. Bussiness layer.
2. Data access layer.
3. Data layer.

More about it here: https://en.wikipedia.org/wiki/Multitier_architecture

The use of this software design gives an array of benefits which are:
1. You have the data access layer modifying the data layer creating an abstraction layer between bussiness layer and the data layer. This makes it easy to scale the system 
without having to change too much code. 

Example: If you have a Microsoft SQL database and want to change it to mySQL you only have to modify the data layer and no other layer needs to be modified.

2. Easy to test! With the data access layer you get the methods for modifying the data layer making it really simple to test your code whenever needed.

There are also several more benefits with this pattern.

Application functionality:
1. The application built is using C# .NET with Winform as the presentation of components.
2. The data layer accesses local files both XML and JSON to store/retrieve data. (With this pattern we can with ease implement a database instead!)
3. Automatic fetching of RSS feeds using asynchronous threading to update the data real time.
4. Modifying all data which includes (Updating, Deleting, Adding).

This requirement specification was created for the C# OOP class for Fall 2020.

# C#, HTML, Nancy and Razor project: HAIR SALON

_*Epicodus C# week-3 Project, 12-09-16*_

by Annie Sonna.


##Description

This webpage is an app for a hair salon. The owner should be able to add a list of the stylists, and for each stylist, add clients who see that stylist. The stylists work independently, so each client only belongs to a single stylist.  This webpage demonstrates database usage with one-to-many relationships.


###Objective from Epicodus page

Participation in creating and presenting a project, and having the project fulfill the following user stories requirements:
1. As a salon employee, I need to be able to see a list of all our stylists.
2. As an employee, I need to be able to select a stylist, see their details, and see a list of all clients that belong to that stylist.
3. As an employee, I need to add new stylists to our system when they are hired.
4. As an employee, I need to be able to add new clients to a specific stylist.
5. As an employee, I need to be able to update a stylist's details.
6. As an employee, I need to be able to update a client's details.
7. As an employee, I need to be able to delete a stylist if they're no longer employed here.
8. As an employee, I need to be able to delete a client if they no longer visit our salon.


##Specifications:

I1. Input 1
 - See the specDoc.txt file for all the specifications related to this website.

##Setup/Installation requirements

1. Clone this repository to desktop.
2. Use powershell under window machine to navigate to the cloned project folder.
3. Run the following command "dnu restore"
4. You will need a database called "hair_salon" with the "clients" and "stylists" tables.
5. Connect to you server and use the following command to create the database:
     # CREATE DATABASE hair_salon;
     # GO
     # USE hair_salon;
     # GO
     # CREATE TABLE stylists (id INT IDENTITY(1,1)), name VARCHAR(255));
     # GO
     # CREATE TABLE clients (id INT IDENTITY(1,1)), name VARCHAR(255), stylist_id INT IDENTITY(1,1));
     # GO
6. Create a backup of above database called "hair_salon_test" and restore it.
7. When writing your test, you can use the following command line on PowerShell for testing: "dnx test".  
8. Run "dnx kestel" command to run this app
9. In your browser, navigate to http://localhost:5004/
10. Then you are ready to start using this webpage!

## Known Bugs
TBD.


## Technologies Used

1. html
2. github
3. Atom
4. Nancy Web Application
5. SQL Server Management
6. C#
7. Xunit
8. Kestrel Server
9. DNX


## Link to the project on GitHub Pages

https://github.com/asonna/HairSalon-CsharReview3


## Copyright and license information

Copyright (c) 2016 Annie Nguimzong Sonna

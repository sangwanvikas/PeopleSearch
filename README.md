Why?
--------
Since I have not used ASP.NET MVC in a while, it is a warm up project, designed and developed by myself. I decided to use Entity Framework, which was new to me until I eplored in this project.

Please go through ReadMe.pdf document for more insight!

What?
--------
"The People Search Application"
•	The application accepts search input in a text box and then displays in a pleasing style a list of people where any part of their first or last name matches what was typed in the search box.
•	This project stores seed data while establishing connection with database (User connects to database through UI).
•	Registration Page allows user to add new person in database.
•	Simulated search being slow, hence I have made the UI gracefully handle the delay with loader.

Technical details-
•	An ASP.NET MVC Application 
•	Used Ajax to respond to search request (no full page refresh) using JSON for both the request and the response
•	Used Entity Framework Code First to talk to the database
•	Added Unit Tests for appropriate parts of the application using Moq.

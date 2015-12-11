# SE2Project
##### Client Management System project for Software Engineering 2 team

  This project was done as a class project, but it was for an actual customer which means that we (the team) got a bit of real-world
experience in requirements gathering and analysis and with working with a customer. We used the Scrum methodology over the course of
the project and I acted as the team lead (or Scrum Master). In addition to development, I also relayed tasks to the team members, 
worked with anyone who was having trouble, fixed issues with the development environment, and created reports at the end of each sprint.
As for the development aspect, our goal was to create a Client Management System for the customer. This system had to keep a database of
all the clients and allow the user to add clients, delete clients, view and edit client information, and filter the clients based on
basically any attribute that a client had. We opted to use a Model-View-Presenter architecture, allowing us to effectively layer the 
application which made it simple for everyone to work on different parts of it at one time. For the UI, we used a data binding pattern
to pass data between it and the presentation layer. We also used a sort of publisher-subscriber pattern to relay events between different
layers in the application. Most of my tasks were UI related, but I basically had a hand in every portion of the project, from the
filtering mechanism to the data modeling and database access methods. One of the most challenging aspects of the project for me was the
filtering. The customer had close to 1,000 clients that they would need to search through by a variety of categories, so performance and
abstraction were of the utmost importance. I implemented the filtering system by using reflection to get every property of every data
model that could be filtered on at runtime, which made the system extremely dynamic in the fact that if a property was added or deleted
from an object, the filter options would update automatically to reflect the change. I also allowed multiple filters to be strung 
together and cached the clients that passed the applied filters so that a) the user could get as broad or as precise as they wanted with
their searches, and b) the filtering system was extremely efficient, even with multiple applied filters. Although we didn't get this 
project finished in the semester that we had to work on it, it proved to be an incredible learning experience as far as working with a 
customer, team management, Agile processes, database management, and deployment. 

**Technologies Used:**
  * C# and .NET for the language/framework
  * Windows Presentation Foundation (WPF) for the UI
  * SQLite for the database
  * Visual Studio for the IDE

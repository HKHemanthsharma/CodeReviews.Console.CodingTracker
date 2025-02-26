Hi This Coding Tracker Project is simply a CRUD application that let's you log your coding time. 
This application allows you to log time for today. or add time logs for any specific date. 
<h1>Frameworks and libraries used:</h1>
<h3>To interact with DB</h3>
I used good old ADO.NET for interacting with Database. and used Dapper along with it which simplified the CRUD operations a little.
i haven't used any ORM before and it was very refreshing and exciting to see just how much i can get done with just one line of code in Dapper.
for example, i can query multiple rows from DB and map it to a model class in my code. and with just .ToList() i can create list of objects with each object carrying each row data in it's properties.
this would have taken many lines of code and loops to implement the same functionality with plain ADO.NET
I am really looking forward to using More of Dapper and also learning much more powerful ORM Framework called EntityFramework.
Although i feel like it's important to write as much as ADO.Net as possible so that you'll have an idea of what happens under the hood in these ORM frameworks.
<h3>Configuartion Manager</h3>
I Don't know if i can classify it here but i used app.config XML file and configurationManager to get the required connection string. instead of HardCoding it. this allows me manage my connectionstrings Better.
I can just manage and change the connection strings at one place.
<h3>Output Console</h3>
Another very exciting thing while doing this project is spectre.console library. This library is so cool. you can represent your data that read from DataBase so well to the console. 
In my earlier CRUD project habitLogger. i did a lousy job with presenting the data to console. it was a hassle to represent the data that's half presentable to the console. but with this spectre.console library,
it was very easy to represent the data very beautifully in whatever style that you like very easily.
although i need to accept that it's a bit of steep learning curve for me to use spectre.console library. I used documentation and other helping agents to write almost all the console output codes cause it was not that
intuitive to write the code. i need to implement spectre.console in my next projects so that i can get comfortable with that.


  

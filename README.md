# ChatProject
My client-server chat room application written in C#.

## Stuff I am using

### Google Protocal Buffers
Used for sending custom protocals over a tcp socket. The protocal buffer turns message objects into byte code to be sent and then
then converts it back to the message object once it reaches the other side.

### GTK#
A C# wrapper around the GTK+. Used to make the graphical interface, was chosen because of it's cross-platform capabilities

### nHibernate
A C# port of the java ORM Hibernate. Helps interface with the MySQL database

### MySQL
SQL database storing persistant data on the server-side.

### A bit of Documentation
Basic UML Diagram: https://www.draw.io/?lightbox=1&highlight=0000ff&edit=_blank&layers=1&nav=1&title=ChatClient%20UML%20Diagram.html#Uhttps%3A%2F%2Fdrive.google.com%2Fuc%3Fid%3D0B5vHCQaD-2OEUXBjUnRRbHdtTkk%26export%3Ddownload

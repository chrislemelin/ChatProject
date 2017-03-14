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

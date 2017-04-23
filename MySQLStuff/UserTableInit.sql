Create table Chat.Users (
 ID int NOT NULL AUTO_INCREMENT,
 Username varchar(255) NOT NULL,
 Password int NOT NULL,
 PRIMARY KEY (ID)
)

Create table Chat.Rooms
(
 ID int NOT NULL AUTO_INCREMENT,
 Title varchar(255) NOT NULL,
 Owner int,
 PRIMARY KEY (ID),
 FOREIGN KEY (Owner) REFERENCES Chat.Users(Owner)
)

INSERT INTO Chat.Users (Username,Password)
VALUES ('testUser',10);

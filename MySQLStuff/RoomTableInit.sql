Create table Chat.Rooms
(
    ID int NOT NULL AUTO_INCREMENT,
    Title varchar(10000) NOT NULL,
    Owner int,
    PRIMARY KEY (ID),
    FOREIGN KEY (Owner) REFERENCES Chat.Users(ID)
)

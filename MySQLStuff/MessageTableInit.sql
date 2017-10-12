Create table Chat.Messages
(
    ID int NOT NULL AUTO_INCREMENT,
    Author int,
    Message varchar(10000),
    TimeStamp TimeStamp,
    PRIMARY KEY (ID),
    FOREIGN KEY (Author) REFERENCES Chat.Users(ID)
)

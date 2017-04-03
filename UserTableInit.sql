Create table Chat.users (
 ID int NOT NULL AUTO_INCREMENT,
 Username varchar(255) NOT NULL,
 Password int NOT NULL,
 PRIMARY KEY (ID)
)

INSERT INTO Chat.users (ID,Username,Password)
VALUES (1,'Lars',10);

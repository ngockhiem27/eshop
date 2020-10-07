INSERT INTO ROLE(ID, NAME, CREATED_AT) VALUES(1, 'ADMIN', CURRENT_TIMESTAMP(2));
INSERT INTO ROLE(ID, NAME, CREATED_AT) VALUES(2, 'MOD', CURRENT_TIMESTAMP(2));

INSERT INTO MANAGER(ID, FIRSTNAME, LASTNAME, EMAIL, CREATED_AT, PASSWORD_HASH, ROLE_ID)
VALUES(1, 'Khiem', 'DN', 'khiem@eshop.com', CURRENT_TIMESTAMP(2), '4D1A237B7C51547533FE5564301CA2B0', 1);
INSERT INTO MANAGER(ID, FIRSTNAME, LASTNAME, EMAIL, CREATED_AT, PASSWORD_HASH, ROLE_ID)
VALUES(2, 'John', 'Smith', 'john@eshop.com', CURRENT_TIMESTAMP(2), '4D1A237B7C51547533FE5564301CA2B0', 2);
INSERT INTO MANAGER(ID, FIRSTNAME, LASTNAME, EMAIL, CREATED_AT, PASSWORD_HASH, ROLE_ID)
VALUES(3, 'Ken', 'Sam', 'ken@eshop.com', CURRENT_TIMESTAMP(2), '4D1A237B7C51547533FE5564301CA2B0', 2);


CREATE OR REPLACE PACKAGE ESHOP_MANAGER_API AS

PROCEDURE SP_MANAGER_GETALLMANAGER 
(
    MNG_CURSOR OUT SYS_REFCURSOR
);

PROCEDURE SP_MANAGER_GETMANAGERBYID
(
    MNG_ID IN MANAGER.ID%TYPE,
    MNG_CURSOR OUT SYS_REFCURSOR
);

PROCEDURE SP_MANAGER_GETMANAGERBYEMAIL
(
    MNG_EMAIL IN MANAGER.EMAIL%TYPE,
    MNG_CURSOR OUT SYS_REFCURSOR
);

PROCEDURE SP_MANAGER_AUTHENTICATEMANAGER
(
    MNG_EMAIL IN MANAGER.EMAIL%TYPE,
    MNG_PASSWORD_HASH IN MANAGER.PASSWORD_HASH%TYPE,
    MNG_CURSOR OUT SYS_REFCURSOR
);

PROCEDURE SP_MANAGER_DELETEMANAGER 
(
    MNG_ID IN MANAGER.ID%TYPE
);

PROCEDURE SP_MANAGER_ADDMANAGER 
(
    MNG_ROLE_ID IN MANAGER.ROLE_ID%TYPE,
    MNG_FIRSTNAME IN MANAGER.FIRSTNAME%TYPE,
    MNG_LASTNAME IN MANAGER.LASTNAME%TYPE,
    MNG_EMAIL IN MANAGER.EMAIL%TYPE,
    MNG_PASSWORD_HASH IN MANAGER.PASSWORD_HASH%TYPE,
    MNG_CURSOR OUT SYS_REFCURSOR
);

PROCEDURE SP_MANAGER_UPDATEMANAGER 
(
    MNG_ID IN MANAGER.ID%TYPE,
    MNG_ROLE_ID IN MANAGER.ROLE_ID%TYPE,
    MNG_FIRSTNAME IN MANAGER.FIRSTNAME%TYPE,
    MNG_LASTNAME IN MANAGER.LASTNAME%TYPE,
    MNG_EMAIL IN MANAGER.EMAIL%TYPE,
    MNG_PASSWORD_HASH IN MANAGER.PASSWORD_HASH%TYPE,
    MNG_CURSOR OUT SYS_REFCURSOR
);

END ESHOP_MANAGER_API;
/

CREATE OR REPLACE PACKAGE BODY ESHOP_MANAGER_API AS

PROCEDURE SP_MANAGER_GETALLMANAGER 
(
    MNG_CURSOR OUT SYS_REFCURSOR
)
AS
BEGIN
OPEN MNG_CURSOR FOR 
SELECT MANAGER.*, ROLE.NAME AS ROLE_NAME FROM MANAGER INNER JOIN ROLE ON MANAGER.ROLE_ID = ROLE.ID;
END;

PROCEDURE SP_MANAGER_GETMANAGERBYID 
(
    MNG_ID IN MANAGER.ID%TYPE,
    MNG_CURSOR OUT SYS_REFCURSOR
)
AS
BEGIN
OPEN MNG_CURSOR FOR 
SELECT MANAGER.*, ROLE.NAME AS ROLE_NAME FROM MANAGER INNER JOIN ROLE ON MANAGER.ROLE_ID = ROLE.ID WHERE MANAGER.ID=MNG_ID;
END;

PROCEDURE SP_MANAGER_GETMANAGERBYEMAIL 
(
    MNG_EMAIL IN MANAGER.EMAIL%TYPE,
    MNG_CURSOR OUT SYS_REFCURSOR
)
AS
BEGIN
OPEN MNG_CURSOR FOR 
SELECT MANAGER.*, ROLE.NAME AS ROLE_NAME FROM MANAGER INNER JOIN ROLE ON MANAGER.ROLE_ID = ROLE.ID WHERE MANAGER.EMAIL=MNG_EMAIL;
END;

PROCEDURE SP_MANAGER_AUTHENTICATEMANAGER
(
    MNG_EMAIL IN MANAGER.EMAIL%TYPE,
    MNG_PASSWORD_HASH IN MANAGER.PASSWORD_HASH%TYPE,
    MNG_CURSOR OUT SYS_REFCURSOR
)
AS
BEGIN
OPEN MNG_CURSOR FOR 
SELECT MANAGER.*, ROLE.NAME AS ROLE_NAME FROM MANAGER INNER JOIN ROLE ON MANAGER.ROLE_ID = ROLE.ID WHERE MANAGER.EMAIL=MNG_EMAIL AND MANAGER.PASSWORD_HASH=MNG_PASSWORD_HASH;
END;


PROCEDURE SP_MANAGER_DELETEMANAGER 
(
    MNG_ID IN MANAGER.ID%TYPE
)
AS
BEGIN
DELETE FROM MANAGER WHERE MANAGER.ID=MNG_ID;
END;

PROCEDURE SP_MANAGER_ADDMANAGER
(
    MNG_ROLE_ID IN MANAGER.ROLE_ID%TYPE,
    MNG_FIRSTNAME IN MANAGER.FIRSTNAME%TYPE,
    MNG_LASTNAME IN MANAGER.LASTNAME%TYPE,
    MNG_EMAIL IN MANAGER.EMAIL%TYPE,
    MNG_PASSWORD_HASH IN MANAGER.PASSWORD_HASH%TYPE,
    MNG_CURSOR OUT SYS_REFCURSOR
)
AS
MNG_ID INT;
BEGIN
INSERT INTO MANAGER(ROLE_ID, FIRSTNAME, LASTNAME, EMAIL, PASSWORD_HASH, CREATED_AT)
VALUES(MNG_ROLE_ID, MNG_FIRSTNAME, MNG_LASTNAME, MNG_EMAIL, MNG_PASSWORD_HASH, CURRENT_TIMESTAMP(2))
RETURNING ID INTO MNG_ID;
OPEN MNG_CURSOR FOR
SELECT MANAGER.*, ROLE.NAME AS ROLE_NAME FROM MANAGER INNER JOIN ROLE ON MANAGER.ROLE_ID = ROLE.ID WHERE MANAGER.ID=MNG_ID;
END;

PROCEDURE SP_MANAGER_UPDATEMANAGER 
(
    MNG_ID IN MANAGER.ID%TYPE,
    MNG_ROLE_ID IN MANAGER.ROLE_ID%TYPE,
    MNG_FIRSTNAME IN MANAGER.FIRSTNAME%TYPE,
    MNG_LASTNAME IN MANAGER.LASTNAME%TYPE,
    MNG_EMAIL IN MANAGER.EMAIL%TYPE,
    MNG_PASSWORD_HASH IN MANAGER.PASSWORD_HASH%TYPE,
    MNG_CURSOR OUT SYS_REFCURSOR
)
AS
BEGIN
UPDATE MANAGER SET
    MANAGER.ROLE_ID = MNG_ROLE_ID, 
    MANAGER.FIRSTNAME = MNG_FIRSTNAME, 
    MANAGER.LASTNAME = MNG_LASTNAME, 
    MANAGER.EMAIL = MNG_EMAIL, 
    MANAGER.PASSWORD_HASH = MNG_PASSWORD_HASH
WHERE MANAGER.ID = MNG_ID;
OPEN MNG_CURSOR FOR
SELECT MANAGER.*, ROLE.NAME AS ROLE_NAME FROM MANAGER INNER JOIN ROLE ON MANAGER.ROLE_ID = ROLE.ID WHERE MANAGER.ID=MNG_ID;
END;

END ESHOP_MANAGER_API;
/
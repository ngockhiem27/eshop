CREATE TABLE ROLE 
(
  ID INT GENERATED BY DEFAULT AS IDENTITY
, NAME VARCHAR2(255) NOT NULL 
, CREATED_AT TIMESTAMP NOT NULL 
, CONSTRAINT PK_ROLE PRIMARY KEY (ID)
);

CREATE TABLE MANAGER 
(
  ID INT GENERATED BY DEFAULT AS IDENTITY
, ROLE_ID INT NOT NULL 
, FIRSTNAME VARCHAR2(255) NOT NULL 
, LASTNAME VARCHAR2(255) NOT NULL 
, EMAIL VARCHAR2(255) NOT NULL 
, PASSWORD_HASH VARCHAR2(255) NOT NULL 
, CREATED_AT TIMESTAMP NOT NULL 
, CONSTRAINT PK_MANAGER PRIMARY KEY (ID)
, CONSTRAINT FK_ROLE FOREIGN KEY (ROLE_ID) REFERENCES ROLE(ID) 
);


CREATE TABLE CATEGORY 
(
  ID INT GENERATED BY DEFAULT AS IDENTITY
, NAME VARCHAR2(255) NOT NULL 
, CREATED_AT TIMESTAMP NOT NULL 
, CONSTRAINT PK_CATEGORY PRIMARY KEY (ID)
);

CREATE TABLE PRODUCT 
(
  ID INT GENERATED BY DEFAULT AS IDENTITY
, NAME VARCHAR2(255) NOT NULL 
, REGULAR_PRICE NUMBER NOT NULL
, DISCOUNT_PRICE NUMBER DEFAULT 0
, CREATED_AT TIMESTAMP NOT NULL 
, UPDATED_AT TIMESTAMP NOT NULL
, CONSTRAINT PK_PRODUCT PRIMARY KEY (ID)
);

CREATE TABLE PRODUCT_CATEGORY
(
  CATEGORY_ID INT NOT NULL
, PRODUCT_ID INT NOT NULL
, CONSTRAINT PK_PRODUCT_CATEGORY PRIMARY KEY (CATEGORY_ID, PRODUCT_ID)
, CONSTRAINT FK_CATEGORY FOREIGN KEY (CATEGORY_ID) REFERENCES CATEGORY(ID)
, CONSTRAINT FK_PRODUCT FOREIGN KEY (PRODUCT_ID) REFERENCES PRODUCT(ID)
);

CREATE TABLE CUSTOMER 
(
  ID INT GENERATED BY DEFAULT AS IDENTITY
, FIRSTNAME VARCHAR2(255) NOT NULL 
, LASTNAME VARCHAR2(255) NOT NULL 
, EMAIL VARCHAR2(255) NOT NULL 
, PASSWORD_HASH VARCHAR2(255) NOT NULL 
, CREATED_AT TIMESTAMP NOT NULL 
, CONSTRAINT PK_CUSTOMER PRIMARY KEY (ID)
);

CREATE TABLE ORDERS
(
  ID INT GENERATED BY DEFAULT AS IDENTITY
, CUSTOMER_ID INT NOT NULL
, TOTAL NUMBER NOT NULL
, CREATED_AT TIMESTAMP NOT NULL
, CONSTRAINT PK_ORDERS PRIMARY KEY (ID)
, CONSTRAINT FK_CUSTOMER FOREIGN KEY (CUSTOMER_ID) REFERENCES CUSTOMER(ID)
);

CREATE TABLE ORDER_ITEMS
(
  ID INT GENERATED BY DEFAULT AS IDENTITY
, ORDER_ID INT NOT NULL
, PRODUCT_ID INT NOT NULL
, QUANTITY INT NOT NULL
, ORDER_PRICE NUMBER NOT NULL
, CONSTRAINT PK_ORDER_ITEMS PRIMARY KEY (ID)
, CONSTRAINT FK_ORDERS FOREIGN KEY (ORDER_ID) REFERENCES ORDERS(ID)
, CONSTRAINT FK_PRODUCTS FOREIGN KEY (PRODUCT_ID) REFERENCES PRODUCT(ID)
);

CREATE UNIQUE INDEX manager_email_i ON MANAGER(EMAIL);
CREATE UNIQUE INDEX customer_email_i ON CUSTOMER(EMAIL);
CREATE UNIQUE INDEX category_name_i ON CATEGORY(NAME);
CREATE UNIQUE INDEX product_name_i ON PRODUCT(NAME);
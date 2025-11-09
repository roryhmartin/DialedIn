-- CREATE TABLE users (
-- id INT AUTO_INCREMENT Primary KEY,
-- first_name VARCHAR(50) NOT NULL,
-- last_name varchar(50) NOT NULL,
-- email VARCHAR(100) NOT NULL,
-- password_hash VARCHAR(255) NOT NULL,
-- created_date DATETIME DEFAULT CURRENT_TIMESTAMP
-- );

INSERT INTO users (first_name, last_name, email, password_hash)
VALUES ('James', 'Lafleur', 'soyer@example.com', 'password123');

CREATE TABLE roles (
Id INT AUTO_INCREMENT PRIMARY KEY,
Name VARCHAR(50) NOT NULL UNIQUE,
Description VARCHAR (255) NULL
);
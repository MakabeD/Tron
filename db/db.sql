
CREATE DATABASE CyberGameDB;
GO

USE CyberGameDB;
GO

CREATE TABLE Users (
    user_id INT IDENTITY(1,1) PRIMARY KEY,
    username NVARCHAR(50) NOT NULL UNIQUE,
    password_hash NVARCHAR(255) NOT NULL,
    role NVARCHAR(20) DEFAULT 'operator',
    created_at DATETIME DEFAULT GETDATE()
);
GO


CREATE TABLE Sections (
    section_id INT IDENTITY(1,1) PRIMARY KEY,
    name NVARCHAR(100) NOT NULL UNIQUE,
    description NVARCHAR(255)
);
GO


CREATE TABLE Events (
    event_id INT IDENTITY(1,1) PRIMARY KEY,
    section_id INT NOT NULL,
    name NVARCHAR(100) NOT NULL UNIQUE,
    description NVARCHAR(255),
    severity NVARCHAR(20) CHECK (severity IN ('Low', 'Medium', 'High')),
    detected_at DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (section_id) REFERENCES Sections(section_id)
);
GO


CREATE TABLE Feedback (
    feedback_id INT IDENTITY(1,1) PRIMARY KEY,
    user_id INT NOT NULL,
    event_id INT NULL,
    rating INT CHECK (rating BETWEEN 1 AND 5),
    comment NVARCHAR(255),
    created_at DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (user_id) REFERENCES Users(user_id),
    FOREIGN KEY (event_id) REFERENCES Events(event_id)
);
GO

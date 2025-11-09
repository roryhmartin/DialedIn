-- CREATE TABLE ClockEntries (
-- 	Id INT AUTO_INCREMENT PRIMARY KEY,
--     UserId INT  NOT NULL,
--     ClockInTime DATETIME NOT NULL,
--     ClockOutTime DATETIME NULL,
--     ApprovedBy INT NULL,
--     ApprovedOn DATETIME NULL,
--     IsAmended BOOLEAN DEFAULT FALSE,
--     FOREIGN KEY (UserId) REFERENCES Users(Id),
--     FOREIGN KEY (ApprovedBy) REFERENCES Users(Id)
-- );

-- INSERT INTO ClockEntries(UserId, ClockInTime)
-- VALUES (2, NOW());

-- UPDATE ClockEntries
-- SET ClockOutTime = NOW()
-- WHERE Id = 1;

-- UPDATE ClockEntries
-- SET ApprovedBy = 1,
-- 	ApprovedOn = NOW()
-- WHERE Id = 1;


SELECT * FROM ClockEntries;
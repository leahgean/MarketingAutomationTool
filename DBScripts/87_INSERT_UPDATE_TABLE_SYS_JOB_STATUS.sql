INSERT INTO Sys_Job_Status(STATUS_ID, STATUS)
VALUES(102, 'RUNNINGIMPORT')

UPDATE Sys_Job_Status
SET [STATUS]='PARSINGEXCEL'
WHERE Status_ID = 101

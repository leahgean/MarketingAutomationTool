ALTER TABLE Campaign
ADD SendingOption INT NULL --0 Send Now , 1--Scheduled


ALTER TABLE Campaign
ADD SendingSchedule DATETIME NULL
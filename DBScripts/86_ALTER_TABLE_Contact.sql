ALTER TABLE Contact
ADD CONSTRAINT df_CreatedDate
DEFAULT GETDATE() FOR CreatedDate; 
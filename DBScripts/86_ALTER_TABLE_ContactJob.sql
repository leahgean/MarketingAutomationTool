select * from contactjob with (nolock)

ALTER TABLE CONTACTJOB
ADD CurrentRowParsedInExcel INT DEFAULT(0)


select * from contactjob_exception with (nolock)
CREATE TABLE [dbo].[Customer]
(
	[iCustomer_Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [vFirst_Name] VARCHAR(15) NULL, 
    [vLast_Name] VARCHAR(15) NULL, 
    [vEmail_Id] VARCHAR(40) NULL, 
    [vPassword] VARCHAR(15) NULL, 
    [vCode] VARCHAR(50) NULL, 
    [IsActive] BIT NULL, 
    [nGender] NCHAR(6) NULL, 
    [vMobile] VARCHAR(14) NULL
)

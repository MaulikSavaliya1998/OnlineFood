CREATE TABLE [dbo].[User] (
    [iUserId]    INT          IDENTITY (1, 1) NOT NULL,
    [vFirstName] VARCHAR (15) NOT NULL,
    [vLastName]  VARCHAR (15) NOT NULL,
    [vEmailId]   VARCHAR (40) NOT NULL,
    [vPassword]   VARCHAR (15) NOT NULL,
    [vCode]       VARCHAR (50) NOT NULL,
    [IsActive]    BIT          DEFAULT ((0)) NOT NULL,
    [nGender]     NCHAR (6)    NULL,
    [vMobile]     VARCHAR (14) NULL,
    PRIMARY KEY CLUSTERED ([iUserId] ASC)
);


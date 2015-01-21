CREATE TABLE [dbo].[Supplier]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ShortCode] NVARCHAR(50) NOT NULL, 
    [ShortName] NVARCHAR(50) NOT NULL, 
    [LongName] NVARCHAR(150) NOT NULL, 
    [Description] NVARCHAR(250) NOT NULL, 
    [IsActive] BIT NOT NULL
)

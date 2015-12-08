CREATE TABLE [dbo].[Address]
(
	[AddressID] INT Identity(1,1) NOT NULL , 
	[Line1] nvarchar(100) NOT NULL,
	[Line2] nvarchar(100)  NULL,
	[City] nvarchar(100) NOT NULL,
	[State] nvarchar(50) NOT NULL,
	[Zip] nvarchar(10) NOT NULL,
	[BeginDate] date NOT NULL,
	[EndDate] date  NULL,
	[PatientID] INT NOT NULL,

	[CreatedDateUtc] DATETIME  NOT NULL DEFAULT GETUTCDATE(),
	[LastModifiedDateUtc] DATETIME  NOT NULL DEFAULT GETUTCDATE(),
	[LastModifiedBy] INT  NOT NULL,
	CONSTRAINT [PK_Address_AddressID] PRIMARY KEY CLUSTERED ([AddressID] ASC),
	   
	CONSTRAINT [FK_Address_Patient] FOREIGN KEY ([PatientID]) REFERENCES Patient ([PatientID])  ON DELETE CASCADE

)

CREATE TABLE [dbo].[Patient]
(
	[PatientID] INT Identity(1,1) NOT NULL , 
	[FirstName] nvarchar(255) NOT NULL,
	[LastName] nvarchar(255) NOT NULL,
	[DOB] date NOT NULL,
	[SSN] nvarchar(10)  NULL,
	[Email] nvarchar(255)  NULL,
	[Phone] nvarchar(10)  NULL,

	[CreatedDateUtc] DATETIME  NOT NULL DEFAULT GETUTCDATE(),
	[LastModifiedDateUtc] DATETIME  NOT NULL DEFAULT GETUTCDATE(),
	[LastModifiedBy] INT  NOT NULL,
	CONSTRAINT [PK_Patient_PatientID] PRIMARY KEY CLUSTERED ([PatientID] ASC),
	
)

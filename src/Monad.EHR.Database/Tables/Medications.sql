CREATE TABLE [dbo].[Medications]
(
	[MedicationsID] INT Identity(1,1) NOT NULL , 
	[Name] nvarchar(100) NOT NULL,
	[Quantity] int NOT NULL,
	[BeginDate] date NOT NULL,
	[EndDate] date  NULL,
	[PatientID] INT NOT NULL,

	[CreatedDateUtc] DATETIME  NOT NULL DEFAULT GETUTCDATE(),
	[LastModifiedDateUtc] DATETIME  NOT NULL DEFAULT GETUTCDATE(),
	[LastModifiedBy] INT  NOT NULL,
	CONSTRAINT [PK_Medications_MedicationsID] PRIMARY KEY CLUSTERED ([MedicationsID] ASC),
	   
	CONSTRAINT [FK_Medications_Patient] FOREIGN KEY ([PatientID]) REFERENCES Patient ([PatientID])  ON DELETE CASCADE

)

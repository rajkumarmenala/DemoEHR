CREATE TABLE [dbo].[PatientHeight]
(
	[PatientHeightID] INT Identity(1,1) NOT NULL , 
	[Height] numeric(10,2) NOT NULL,
	[Date] date NOT NULL,
	[PatientID] INT NOT NULL,

	[CreatedDateUtc] DATETIME  NOT NULL DEFAULT GETUTCDATE(),
	[LastModifiedDateUtc] DATETIME  NOT NULL DEFAULT GETUTCDATE(),
	[LastModifiedBy] INT  NOT NULL,
	CONSTRAINT [PK_PatientHeight_PatientHeightID] PRIMARY KEY CLUSTERED ([PatientHeightID] ASC),
	   
	CONSTRAINT [FK_PatientHeight_Patient] FOREIGN KEY ([PatientID]) REFERENCES Patient ([PatientID])  ON DELETE CASCADE

)

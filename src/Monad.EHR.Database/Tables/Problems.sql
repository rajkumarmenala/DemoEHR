CREATE TABLE [dbo].[Problems]
(
	[ProblemsID] INT Identity(1,1) NOT NULL , 
	[Description] nvarchar(255) NOT NULL,
	[Date] date NOT NULL,
	[PatientID] INT NOT NULL,

	[CreatedDateUtc] DATETIME  NOT NULL DEFAULT GETUTCDATE(),
	[LastModifiedDateUtc] DATETIME  NOT NULL DEFAULT GETUTCDATE(),
	[LastModifiedBy] INT  NOT NULL,
	CONSTRAINT [PK_Problems_ProblemsID] PRIMARY KEY CLUSTERED ([ProblemsID] ASC),
	   
	CONSTRAINT [FK_Problems_Patient] FOREIGN KEY ([PatientID]) REFERENCES Patient ([PatientID])  ON DELETE CASCADE

)

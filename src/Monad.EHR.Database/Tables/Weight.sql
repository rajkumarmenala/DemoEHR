CREATE TABLE [dbo].[Weight]
(
	[WeightID] INT Identity(1,1) NOT NULL , 
	[Date] date NOT NULL,
	[Wt] numeric(10,2) NOT NULL,
	[PatientID] INT NOT NULL,

	[CreatedDateUtc] DATETIME  NOT NULL DEFAULT GETUTCDATE(),
	[LastModifiedDateUtc] DATETIME  NOT NULL DEFAULT GETUTCDATE(),
	[LastModifiedBy] INT  NOT NULL,
	CONSTRAINT [PK_Weight_WeightID] PRIMARY KEY CLUSTERED ([WeightID] ASC),
	   
	CONSTRAINT [FK_Weight_Patient] FOREIGN KEY ([PatientID]) REFERENCES Patient ([PatientID])  ON DELETE CASCADE

)

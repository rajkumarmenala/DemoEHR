CREATE TABLE [dbo].[Resource]
(
	[ResourceID] INT Identity(1,1) NOT NULL , 
    [Name]  NVARCHAR (255) NOT NULL,
	[Description] NVARCHAR(MAX) NULL,
	[ResourceTypeID] INT  NOT NULL ,
	[CreatedDateUtc] DATETIME  NOT NULL DEFAULT GETUTCDATE(),
	[LastModifiedDateUtc] DATETIME  NOT NULL DEFAULT GETUTCDATE(),
	[LastModifiedBy] INT  NOT NULL,
	CONSTRAINT [PK_Resource_ResourceID] PRIMARY KEY CLUSTERED ([ResourceID] ASC),
	CONSTRAINT [FK_Resource_ResourceType] FOREIGN KEY ([ResourceTypeID])  REFERENCES ResourceType ([ResourceTypeID] )  ON DELETE CASCADE
)

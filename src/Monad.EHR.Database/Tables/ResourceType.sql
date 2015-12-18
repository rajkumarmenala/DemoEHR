CREATE TABLE [dbo].[ResourceType]
(
	[ResourceTypeID] INT Identity(1,1) NOT NULL , 
    [Name]  NVARCHAR (255) NOT NULL,
	[Description] NVARCHAR(MAX) NULL,
	CONSTRAINT [PK_ResourceType_ResourceTypeID] PRIMARY KEY CLUSTERED ([ResourceTypeID] ASC)
)

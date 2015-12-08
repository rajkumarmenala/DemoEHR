
CREATE TABLE [dbo].[Activity](
	[ActivityID] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](100) NULL,
	[Value] [varchar](50) NOT NULL,
	[CreatedDateUtc] DATETIME  NOT NULL DEFAULT GETUTCDATE(),
	[LastModifiedDateUtc] DATETIME  NOT NULL DEFAULT GETUTCDATE(),
	[LastModifiedBy] INT  NOT NULL,
 CONSTRAINT [PK_Activity_ActivityID] PRIMARY KEY CLUSTERED ([ActivityID] ASC)
 )

GO
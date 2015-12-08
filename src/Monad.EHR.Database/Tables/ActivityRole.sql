
CREATE TABLE [dbo].[ActivityRole](
	[ActivityRoleID] [int] IDENTITY(1,1) NOT NULL,
	[ActivityID] [int] NOT NULL,
	[RoleID] [nvarchar](128) NOT NULL,
	[CreatedDateUtc] DATETIME  NOT NULL DEFAULT GETUTCDATE(),
	[LastModifiedDateUtc] DATETIME  NOT NULL DEFAULT GETUTCDATE(),
	[LastModifiedBy] INT  NOT NULL,
	CONSTRAINT [PK_ActivityRole_ActivityRoleID] PRIMARY KEY CLUSTERED ([ActivityRoleID] ASC),
	CONSTRAINT [FK_ActivityRole_Activity] FOREIGN KEY ([ActivityID]) REFERENCES Activity ([ActivityID])  ON DELETE CASCADE,
	CONSTRAINT [FK_ActivityRole_Role] FOREIGN KEY ([RoleID]) REFERENCES AspNetRoles ([Id])  ON DELETE CASCADE,
)
GO
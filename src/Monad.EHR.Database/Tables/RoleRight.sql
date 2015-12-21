
CREATE TABLE [RoleRight](
	[RoleRightID] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [nvarchar](128) NOT NULL,
	[ActivityId] int NOT NULL,
	[ResourceId] int NOT NULL,
	[CreatedDateUtc] [datetime] NOT NULL DEFAULT (getutcdate()),
	[LastModifiedDateUtc] [datetime] NOT NULL DEFAULT (getutcdate()),
	[LastModifiedBy] [int] NOT NULL,
	CONSTRAINT [PK_RoleRight_RoleRightID] PRIMARY KEY CLUSTERED ([RoleRightID] ASC),
) 

GO

ALTER TABLE [dbo].[RoleRight]  WITH CHECK ADD  CONSTRAINT [FK_RoleRight_dbo.AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO


ALTER TABLE [dbo].[RoleRight]  WITH CHECK ADD  CONSTRAINT [FK_RoleRight_Activity_ActivityId] FOREIGN KEY([ActivityId])
REFERENCES [dbo].[Activity] ([ActivityID])
ON DELETE CASCADE
GO


ALTER TABLE [dbo].[RoleRight]  WITH CHECK ADD  CONSTRAINT [FK_RoleRight_Resource_ResourceId] FOREIGN KEY([ResourceId])
REFERENCES [dbo].[Resource] ([ResourceID])
--ON DELETE CASCADE
GO




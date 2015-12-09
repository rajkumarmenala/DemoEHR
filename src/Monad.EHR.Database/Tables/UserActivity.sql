
CREATE TABLE [dbo].[UserActivity](
	[UserActivityID] [int] IDENTITY(1,1) NOT NULL,
	[ActivityID] [int] NOT NULL,
	[UserID] [nvarchar](128) NOT NULL,
	[CreatedDateUtc] DATETIME  NOT NULL DEFAULT GETUTCDATE(),
	[LastModifiedDateUtc] DATETIME  NOT NULL DEFAULT GETUTCDATE(),
	[LastModifiedBy] INT  NOT NULL,
	CONSTRAINT [PK_UserActivity_UserActivityID] PRIMARY KEY CLUSTERED ([UserActivityID] ASC),
	CONSTRAINT [FK_UserActivity_Activity] FOREIGN KEY ([ActivityID]) REFERENCES Activity ([ActivityID])  ON DELETE CASCADE,
	CONSTRAINT [FK_UserActivitye_User] FOREIGN KEY ([UserID]) REFERENCES [AspNetUsers] ([Id])  ON DELETE CASCADE,
)
GO
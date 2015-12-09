CREATE TABLE [dbo].[AspNetRoles](
  [Id] [nvarchar](128) NOT NULL,
  [Name] [nvarchar](256) NOT NULL,
  [ConcurrencyStamp] nvarchar(max),
  [NormalizedName] nvarchar(max),
  [CreatedDateUtc] DATETIME  NOT NULL DEFAULT GETUTCDATE(),
  [LastModifiedDateUtc] DATETIME  NOT NULL DEFAULT GETUTCDATE(),
  [LastModifiedBy] INT  NOT NULL,
 CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED 
(
  [Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO


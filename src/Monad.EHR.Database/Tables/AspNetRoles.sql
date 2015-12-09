
CREATE TABLE [AspNetRoles] (
    [ConcurrencyStamp] nvarchar(max),
    [Id] [nvarchar](128) NOT NULL,
    [Name] nvarchar(max),
    [NormalizedName] nvarchar(max),
    CONSTRAINT [PK_AspNetRoles] PRIMARY KEY NONCLUSTERED ([Id])
);

GO
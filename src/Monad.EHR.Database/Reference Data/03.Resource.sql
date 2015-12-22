--INSERT INTO [Resource]([Name],[Description] ,[ResourceTypeID], [CreatedDateUtc],[LastModifiedDateUtc],[LastModifiedBy])  
--Select top 1 N'Project' as Name , N'Project' as Description , ResourceTypeID,  GETUTCDATE() as reatedDateUtc, GETUTCDATE() as LastModifiedDateUtc, -1 as  LastModifiedBy From ResourceType where Name = 'Form'

--INSERT INTO [Resource]([Name],[Description] ,[ResourceTypeID], [CreatedDateUtc],[LastModifiedDateUtc],[LastModifiedBy])  
--Select top 1 N'Form' as Name , N'Form' as Description , ResourceTypeID,  GETUTCDATE() as reatedDateUtc, GETUTCDATE() as LastModifiedDateUtc, -1 as  LastModifiedBy From ResourceType where Name = 'Form'

--INSERT INTO [Resource]([Name],[Description] ,[ResourceTypeID], [CreatedDateUtc],[LastModifiedDateUtc],[LastModifiedBy])  
--Select top 1 N'FormField' as Name , N'FormField' as Description , ResourceTypeID,  GETUTCDATE() as reatedDateUtc, GETUTCDATE() as LastModifiedDateUtc, -1 as  LastModifiedBy From ResourceType where Name = 'Form'


--INSERT INTO [Resource]([Name],[Description] ,[ResourceTypeID], [CreatedDateUtc],[LastModifiedDateUtc],[LastModifiedBy])  
--Select top 1 N'Project' as Name , N'Project' as Description , ResourceTypeID,  GETUTCDATE() as reatedDateUtc, GETUTCDATE() as LastModifiedDateUtc, -1 as  LastModifiedBy From ResourceType where Name = 'API'

--INSERT INTO [Resource]([Name],[Description] ,[ResourceTypeID], [CreatedDateUtc],[LastModifiedDateUtc],[LastModifiedBy])  
--Select top 1 N'Form' as Name , N'Form' as Description , ResourceTypeID,  GETUTCDATE() as reatedDateUtc, GETUTCDATE() as LastModifiedDateUtc, -1 as  LastModifiedBy From ResourceType where Name = 'API'

--INSERT INTO [Resource]([Name],[Description] ,[ResourceTypeID], [CreatedDateUtc],[LastModifiedDateUtc],[LastModifiedBy])  
--Select top 1 N'FormField' as Name , N'FormField' as Description , ResourceTypeID,  GETUTCDATE() as reatedDateUtc, GETUTCDATE() as LastModifiedDateUtc, -1 as  LastModifiedBy From ResourceType where Name = 'API'


--INSERT INTO [dbo].[RoleRight]
--           ([RoleId]
--           ,[ActivityId]
--           ,[ResourceId]
--           ,[CreatedDateUtc]
--           ,[LastModifiedDateUtc]
--           ,[LastModifiedBy])

--           select role.Id as RoleId ,
--		   a.ActivityID as ActivityId,
--		   r.ResourceID as ResourceId ,
--		   GETUTCDATE() as reatedDateUtc, 
--		   GETUTCDATE() as LastModifiedDateUtc, -1 as  LastModifiedBy 
--		   from AspNetRoles role , Activity a  , [Resource] r 

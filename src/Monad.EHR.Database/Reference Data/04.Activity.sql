--INSERT INTO [Activity]([Description], [Value] ,[ResourceTypeID], [CreatedDateUtc],[LastModifiedDateUtc],[LastModifiedBy])  
--Select top 1 N'Add' as Description , N'Add' as [Value] , ResourceTypeID,  GETUTCDATE() as reatedDateUtc, GETUTCDATE() as LastModifiedDateUtc, -1 as  LastModifiedBy From ResourceType where Name = 'Form'

--INSERT INTO [Activity]([Description], [Value] ,[ResourceTypeID], [CreatedDateUtc],[LastModifiedDateUtc],[LastModifiedBy])  
--Select top 1 N'Edit' as Description , N'Edit' as [Value] , ResourceTypeID,  GETUTCDATE() as reatedDateUtc, GETUTCDATE() as LastModifiedDateUtc, -1 as  LastModifiedBy From ResourceType where Name = 'Form'

--INSERT INTO [Activity]([Description], [Value] ,[ResourceTypeID], [CreatedDateUtc],[LastModifiedDateUtc],[LastModifiedBy])  
--Select top 1 N'Delete' as Description , N'Delete' as [Value] , ResourceTypeID,  GETUTCDATE() as reatedDateUtc, GETUTCDATE() as LastModifiedDateUtc, -1 as  LastModifiedBy From ResourceType where Name = 'Form'


--INSERT INTO [Activity]([Description], [Value] ,[ResourceTypeID], [CreatedDateUtc],[LastModifiedDateUtc],[LastModifiedBy])  
--Select top 1 N'Read' as Description , N'Read' as [Value] , ResourceTypeID,  GETUTCDATE() as reatedDateUtc, GETUTCDATE() as LastModifiedDateUtc, -1 as  LastModifiedBy From ResourceType where Name = 'Form'


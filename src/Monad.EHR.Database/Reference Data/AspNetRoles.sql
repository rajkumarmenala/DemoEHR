INSERT INTO [AspNetRoles]([Id],[Name] ,[NormalizedName],[CreatedDateUtc],[LastModifiedDateUtc],[LastModifiedBy])  VALUES ( newid(),'Administrator' ,'Administrator' ,GETUTCDATE(), GETUTCDATE(), -1)
GO 
INSERT INTO [AspNetRoles]([Id],[Name] ,[NormalizedName],[CreatedDateUtc],[LastModifiedDateUtc],[LastModifiedBy])  VALUES ( newid(),'Clinician' ,'Clinician' ,GETUTCDATE(), GETUTCDATE(), -1)
GO 
INSERT INTO [AspNetRoles]([Id],[Name] ,[NormalizedName],[CreatedDateUtc],[LastModifiedDateUtc],[LastModifiedBy])  VALUES ( newid(),'Nurse','Nurse' ,GETUTCDATE(), GETUTCDATE(), -1)


GO
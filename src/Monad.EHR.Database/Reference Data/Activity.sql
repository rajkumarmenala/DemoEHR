INSERT INTO [Activity]([Description],[Value] ,[CreatedDateUtc],[LastModifiedDateUtc],[LastModifiedBy])  VALUES ( N'Patient', N'Patient', GETUTCDATE(), GETUTCDATE(), -1)

GO 

INSERT INTO [Activity]([Description],[Value] ,[CreatedDateUtc],[LastModifiedDateUtc],[LastModifiedBy])  VALUES ( N'Address', N'Address', GETUTCDATE(), GETUTCDATE(), -1)

GO 

INSERT INTO [Activity]([Description],[Value] ,[CreatedDateUtc],[LastModifiedDateUtc],[LastModifiedBy])  VALUES ( N'Medications', N'Medications', GETUTCDATE(), GETUTCDATE(), -1)

GO 

INSERT INTO [Activity]([Description],[Value] ,[CreatedDateUtc],[LastModifiedDateUtc],[LastModifiedBy])  VALUES ( N'Problems', N'Problems', GETUTCDATE(), GETUTCDATE(), -1)

GO 

INSERT INTO [Activity]([Description],[Value] ,[CreatedDateUtc],[LastModifiedDateUtc],[LastModifiedBy])  VALUES ( N'BP', N'BP', GETUTCDATE(), GETUTCDATE(), -1)

GO 

INSERT INTO [Activity]([Description],[Value] ,[CreatedDateUtc],[LastModifiedDateUtc],[LastModifiedBy])  VALUES ( N'PatientHeight', N'PatientHeight', GETUTCDATE(), GETUTCDATE(), -1)

GO 

INSERT INTO [Activity]([Description],[Value] ,[CreatedDateUtc],[LastModifiedDateUtc],[LastModifiedBy])  VALUES ( N'Weight', N'Weight', GETUTCDATE(), GETUTCDATE(), -1)

GO 


--INSERT INTO [dbo].[ActivityRole]
--           ([ActivityID]
--           ,[RoleID]
--           ,[CreatedDateUtc]
--           ,[LastModifiedDateUtc]
--           ,[LastModifiedBy])

--select a.ActivityID, r.Id as RoleID, GETUTCDATE() as CreatedDateUtc, 
--GETUTCDATE() as LastModifiedDateUtc, -1 as LastModifiedBy from Activity a , AspNetRoles r 
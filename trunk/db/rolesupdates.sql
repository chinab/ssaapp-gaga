if exists (select * from sysobjects where id = object_id(N'[sproc_RolePermissions_GetCount]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [sproc_RolePermissions_GetCount]
GO

/* Procedure sproc_RolePermissions_GetCount*/
CREATE PROCEDURE sproc_RolePermissions_GetCount
AS
SELECT
	COUNT(*)
FROM
	[RolePermissions]
GO


if exists (select * from sysobjects where id = object_id(N'[sproc_RolePermissions_Get]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [sproc_RolePermissions_Get]
GO

/* Procedure sproc_RolePermissions_Get*/
CREATE PROCEDURE sproc_RolePermissions_Get
AS
SELECT
	--[RolePermissionID],
	--[RoleName],
	--[PageName],
	--[Accessable]

*
FROM
	[RolePermissions]
GO


if exists (select * from sysobjects where id = object_id(N'[sproc_RolePermissions_GetByRolePermissionID]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [sproc_RolePermissions_GetByRolePermissionID]
GO

/* Procedure sproc_RolePermissions_GetByRolePermissionID*/
CREATE PROCEDURE sproc_RolePermissions_GetByRolePermissionID
@RolePermissionID int
AS
SELECT
	--[RolePermissionID],
	--[RoleName],
	--[PageName],
	--[Accessable]

*
FROM
	[RolePermissions]
WHERE
	[RolePermissionID] = @RolePermissionID
GO

/* Procedure sproc_RolePermissions_GetByRolePermissionID*/
CREATE PROCEDURE sproc_RolePermissions_GetByRolePermissionName
@RoleName nvarchar(256)
AS
SELECT
	--[RolePermissionID],
	--[RoleName],
	--[PageName],
	--[Accessable]

*
FROM
	[RolePermissions]
WHERE
	[RoleName] = @RoleName
GO


if exists (select * from sysobjects where id = object_id(N'[sproc_RolePermissions_GetPaged]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [sproc_RolePermissions_GetPaged]
GO

/* Procedure sproc_RolePermissions_GetPaged*/
CREATE PROCEDURE sproc_RolePermissions_GetPaged
	@RecPerPage INT,
	@PageIndex INT
AS

DECLARE @FirstRec INT
DECLARE @LastRec INT

SET @FirstRec = (@PageIndex - 1)*@RecPerPage + 1
SET @LastRec = @PageIndex *@RecPerPage 

-- create temp table to paging
CREATE TABLE #tmp_paging_index
(
	recID		INT IDENTITY(1,1) NOT NULL,
	messageID	int
)

-- insert temp records
INSERT INTO #tmp_paging_index(messageID)
SELECT [RolePermissionID]
FROM [RolePermissions]


-- query out
SELECT *
FROM [RolePermissions]
WHERE [RolePermissionID]
IN (
	SELECT messageID 
	FROM #tmp_paging_index 
	WHERE recID >= @FirstRec AND recID <= @LastRec
)
DROP TABLE #tmp_paging_index

GO



if exists (select * from sysobjects where id = object_id(N'[sproc_RolePermissions_Add]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [sproc_RolePermissions_Add]
GO

/* Procedure sproc_RolePermissions_Add*/
CREATE PROCEDURE sproc_RolePermissions_Add
	@RolePermissionID int OUTPUT
	,@RoleName nvarchar(256)
	,@PageName nvarchar(50)
	,@Accessable bit

AS

	INSERT INTO [RolePermissions]
	(
		[RoleName],
		[PageName],
		[Accessable]
	)
	VALUES
	(
		@RoleName,
		@PageName,
		@Accessable
	)
	SELECT
		@RolePermissionID = @@Identity

GO
if exists (select * from sysobjects where id = object_id(N'[sproc_RolePermissions_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [sproc_RolePermissions_Update]
GO

/* Procedure sproc_RolePermissions_Update*/
CREATE PROCEDURE sproc_RolePermissions_Update
	@RoleName nvarchar(256),
	@PageName nvarchar(50),
	@Accessable bit,
	@RolePermissionID int

AS
UPDATE [RolePermissions]
SET
	[RoleName] = @RoleName,
	[PageName] = @PageName,
	[Accessable] = @Accessable
WHERE
	[RolePermissionID] = @RolePermissionID
GO

if exists (select * from sysobjects where id = object_id(N'[sproc_RolePermissions_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [sproc_RolePermissions_Delete]
GO

/* Procedure sproc_RolePermissions_Delete*/
CREATE PROCEDURE sproc_RolePermissions_Delete
	@RolePermissionID int
AS
DELETE
FROM
	[RolePermissions]
WHERE
	[RolePermissionID] = @RolePermissionID
GO


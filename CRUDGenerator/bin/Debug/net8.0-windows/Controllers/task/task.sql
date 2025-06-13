GO

GO

CREATE  VIEW [dbo].adv_task_select AS
SELECT
    t0.taskid AS taskid,
    t0.projectid AS projectid,
    t0.refno AS refno,
    t0.tasktitle AS tasktitle,
    t0.taskdesc AS taskdesc,
    t0.taskdatetime AS taskdatetime,
    t0.priority AS priority,
    t0.importance AS importance,
    t0.tasktype AS tasktype,
    t0.startdate AS startdate,
    t0.duedate AS duedate,
    t0.task_status AS task_status,
    t0.assigneddate AS assigneddate,
    t0.isassigned AS isassigned,
    t0.iscancelled AS iscancelled,
    t0.cancelledby AS cancelledby,
    t0.cancelledon AS cancelledon,
    t0.commitmentdate AS commitmentdate,
    t0.is_locked AS is_locked,
    t0.created_by AS created_by,
    t0.created_on AS created_on,
    t0.updated_on AS updated_on,
    t0.updated_by AS updated_by,
    t0.update_count AS update_count,
    t0.deleted_on AS deleted_on,
    t0.deleted_by AS deleted_by,
    t0.insertsessionid AS insertsessionid,
    t0.updatesessionid AS updatesessionid  FROM dbo.task t0
Where t0.deleted_on IS NULL


GO

GO
GO

GO

CREATE proc [dbo].[adp_task_select]
(
@result_type int, --1:total_row_count , 2:actual data
@page_number int,
@page_size int ,
@search_column varchar(50),          
@search_text varchar(50)  ,          
@sort_col_name varchar(50),          
@sort_type varchar(15)          
--@extra_whereclause VARCHAR(50) = null 
)            
as 
begin 
set nocount on 
set xact_abort on 
declare @ErrorDetails varchar(max),@ErrorSeverity smallint, @ErrorState smallint 
begin try 
begin tran 



IF(@result_type = 2)
BEGIN
Select* from(
SELECT
CASE WHEN c.name = 'type_code' THEN 'type'
ELSE c.[name] END AS  ColumnName
,CASE WHEN c.name = 'created_on' THEN 'Created On'
WHEN c.name = 'created_by' THEN 'Created By'
WHEN c.name = 'updated_on' THEN 'Updated On'
WHEN c.name = 'updated_by' THEN 'Updated By'
WHEN c.name = 'update_count' THEN 'Update Count'
WHEN c.name = 'is_locked' THEN 'Locked'
ELSE c.[name] END AS ColumnCaption 
,CASE WHEN c.name = 'created_on' THEN 100
WHEN c.name = 'created_by' THEN 101
WHEN c.name = 'updated_on' THEN 102
WHEN c.name = 'updated_by' THEN 103
WHEN c.name = 'update_count' THEN 104
WHEN c.name = 'is_locked' THEN 97
ELSE c.column_id END  AS ColumnOrder
,CONVERT(BIT, 0) IsEnable 
,CASE WHEN c.name in ('name','short_name') THEN 150 WHEN c.name in ('Edit','Delete','is_locked') THEN 80 ELSE 150 END AS width
, CASE WHEN c.name in ('is_locked') THEN 'checkbox' WHEN c.name in ('Edit','Delete') THEN 'button' ELSE 'textbox' END  [control]
, CASE WHEN c.name in ('row_no', 'created_on', 'created_by', 'updated_on', 'updated_by', 'update_count', 'deleted_on', 'deleted_by') THEN 0 ELSE 1 END AS IsVisible
, CASE WHEN c.name = 'Code' THEN 1 ELSE 0 END AS [is_primary]
,1 AS[sorting]
,(CASE WHEN c.name = 'Edit' THEN '<i class="bi bi-pencil-square"></i>'
WHEN c.name = 'Delete' THEN '<i class="bi bi-trash"></i>'
ELSE NULL END)  AS control_content
,null AS control_tooltip
,null AS[shortcut_key]
FROM( 
Select c1.name, c1.column_id, c1.user_type_id 
From sys.columns c1  
JOIN sys.types t ON t.user_type_id = c1.user_type_id 
WHERE c1.OBJECT_ID = OBJECT_ID('dbo.task')  
Union All 
Select 'Edit', 200, -1 
Union All 
Select 'Delete', 201, -1 
) c 
) tbl 
Order By ColumnOrder 
END
exec p_adm_common_select 
@result_type ,--@result_type 
@page_number,--@page_number 
@page_size,--@page_size 
@search_column,--@search_column 
@search_text,--@search_text 
@sort_col_name,--@sort_col_name 
@sort_type,--@sort_type 
'[dbo].adv_task_select',--@table_name 
'*',--@select_columns 
'' 



commit tran
end try 
begin catch
rollback tran 
select @ErrorDetails = ERROR_Message(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE()
raiserror(@ErrorDetails, @ErrorSeverity, @ErrorState)
end catch
end
GO


GO

GO
GO

GO

CREATE PROCEDURE [dbo].adp_task_insert_or_update
    @taskid INT,
    @projectid INT,
    @refno NVARCHAR(50),
    @tasktitle NVARCHAR(255),
    @taskdesc NVARCHAR(500),
    @taskdatetime DATETIME,
    @priority INT,
    @importance INT,
    @tasktype INT,
    @startdate DATETIME,
    @duedate DATETIME,
    @task_status INT,
    @assigneddate DATETIME,
    @isassigned BIT,
    @iscancelled BIT,
    @cancelledby INT,
    @cancelledon DATETIME,
    @commitmentdate DATETIME,
    @is_locked BIT,
    @insertsessionid INT,
    @updatesessionid INT,
    @login_code INT
AS
BEGIN
SET NOCOUNT ON
SET XACT_ABORT ON
DECLARE @ErrorDetails VARCHAR(MAX)
DECLARE @ErrorSeverity SMALLINT
DECLARE @ErrorState SMALLINT
BEGIN TRY
BEGIN TRAN

 IF(@taskid = 0)
BEGIN
Select @taskid = ISNULL(MAX(taskid), 0) + 1 from [dbo].task
    INSERT INTO [dbo].task (
        taskid,
        projectid,
        refno,
        tasktitle,
        taskdesc,
        taskdatetime,
        priority,
        importance,
        tasktype,
        startdate,
        duedate,
        task_status,
        assigneddate,
        isassigned,
        iscancelled,
        cancelledby,
        cancelledon,
        commitmentdate,
        is_locked,
        insertsessionid,
        updatesessionid,
        created_on,
        created_by    )
    VALUES (
        @taskid,
        @projectid,
        @refno,
        @tasktitle,
        @taskdesc,
        @taskdatetime,
        @priority,
        @importance,
        @tasktype,
        @startdate,
        @duedate,
        @task_status,
        @assigneddate,
        @isassigned,
        @iscancelled,
        @cancelledby,
        @cancelledon,
        @commitmentdate,
        @is_locked,
        @insertsessionid,
        @updatesessionid,
        GETDATE(),
        @login_code    );
END
ELSE
BEGIN
    UPDATE [dbo].task
    SET
        projectid = @projectid,
        refno = @refno,
        tasktitle = @tasktitle,
        taskdesc = @taskdesc,
        taskdatetime = @taskdatetime,
        priority = @priority,
        importance = @importance,
        tasktype = @tasktype,
        startdate = @startdate,
        duedate = @duedate,
        task_status = @task_status,
        assigneddate = @assigneddate,
        isassigned = @isassigned,
        iscancelled = @iscancelled,
        cancelledby = @cancelledby,
        cancelledon = @cancelledon,
        commitmentdate = @commitmentdate,
        is_locked = @is_locked,
        insertsessionid = @insertsessionid,
        updatesessionid = @updatesessionid,
updated_on = GETDATE(),
updated_by = @login_code,
update_count = ISNULL(update_count, 0) + 1

    WHERE taskid = @taskid;
END
Select * from [dbo].task Where taskid= @taskid
COMMIT TRAN
END TRY
BEGIN CATCH
ROLLBACK TRAN
SELECT @ErrorDetails = ERROR_Message(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE()
RAISERROR(@ErrorDetails, @ErrorSeverity, @ErrorState)
END CATCH
END
GO


GO

GO
GO

GO

CREATE PROCEDURE [dbo].adp_task_read
    @taskid INT
AS
BEGIN
SET NOCOUNT ON
SET XACT_ABORT ON
DECLARE @ErrorDetails VARCHAR(MAX),@ErrorSeverity SMALLINT, @ErrorState SMALLINT
BEGIN TRY
BEGIN TRAN

SELECT * 
  FROM adv_task_select 
    WHERE taskid = @taskid;
COMMIT TRAN
END TRY
BEGIN CATCH
ROLLBACK TRAN
SELECT @ErrorDetails = ERROR_Message(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE()
RAISERROR(@ErrorDetails, @ErrorSeverity, @ErrorState)
END CATCH
END
GO


GO

GO
GO

GO

CREATE PROCEDURE [dbo].adp_task_delete
    @taskid INT,
    @login_code INT
AS
BEGIN
SET NOCOUNT ON
SET XACT_ABORT ON
DECLARE @ErrorDetails VARCHAR(MAX),@ErrorSeverity SMALLINT, @ErrorState SMALLINT
BEGIN TRY
BEGIN TRAN

   UPDATE dbo.task
    SET
   deleted_on=GETDATE(),deleted_by=@login_code
    WHERE taskid = @taskid;
COMMIT TRAN
END TRY
BEGIN CATCH
ROLLBACK TRAN
SELECT @ErrorDetails = ERROR_Message(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE()
RAISERROR(@ErrorDetails, @ErrorSeverity, @ErrorState)
END CATCH
END
GO


GO

GO

CREATE PROCEDURE sp_update_projectmember_token
    @user_id INT,
    @session_id INT,
    @token NVARCHAR(max)
AS
BEGIN
    IF EXISTS (SELECT 1 FROM sessioninfo WHERE sessionid = @session_id AND userid = @user_id)
    BEGIN
        UPDATE sessioninfo
        SET sessiontoken = @token, lastupdateddate = GETDATE()
        WHERE sessionid = @session_id AND userid = @user_id;

        SELECT 'Token updated successfully.' AS message;
    END
    ELSE
    BEGIN
        SELECT 'Session not found for the given user.' AS message;
    END
END;

GO
CREATE PROCEDURE sp_project_member_login
    @user_name VARCHAR(20),
    @password NVARCHAR(255)
AS
BEGIN
    BEGIN TRY
        DECLARE @stored_password NVARCHAR(255), 
                @stored_user_name NVARCHAR(255), 
                @stored_user_id INT, 
                @session_id INT, 
                @session_token NVARCHAR(255), 
				@sessionstarttime DATETIME,
                @current_timestamp DATETIME = GETDATE(),
                @token_expiry DATETIME;

        SELECT 
            @stored_password = password, 
            @stored_user_id = projectmember,
            @stored_user_name = name
        FROM projectmember 
        WHERE name = @user_name;

        IF @stored_user_id IS NULL
        BEGIN
            THROW 50001, 'User does not exist.', 1;
        END

        IF @stored_password != @password
        BEGIN
            THROW 50002, 'Invalid password.', 1;
        END

        INSERT INTO sessioninfo 
            (userid, sessiontoken,sessionstarttime, sessionendtime, sessionstatus, createddate) 
        VALUES 
            (@stored_user_id, NULL,@current_timestamp, DATEADD(HOUR, 1, @current_timestamp), 1, @current_timestamp);

        SET @session_id = SCOPE_IDENTITY();

        SET @token_expiry = DATEADD(HOUR, 1, @current_timestamp);

        SELECT 
            'Login successful. Session created.' AS message,
            @stored_user_id AS user_id,
            @session_id AS session_id,
            @stored_user_name AS user_name,
            @token_expiry AS token_expiry,
            @session_token AS Token,
			@sessionstarttime AS  session_expiry;
    END TRY
    BEGIN CATCH
        DECLARE @error_message NVARCHAR(4000), @error_severity INT, @error_state INT;
        SELECT 
            @error_message = ERROR_MESSAGE(),
            @error_severity = ERROR_SEVERITY(),
            @error_state = ERROR_STATE();

        THROW @error_severity, @error_message, @error_state;
    END CATCH
END;

GO

create PROCEDURE sp_upsert_projectmember
    @name NVARCHAR(500),
    @emailid NVARCHAR(255),
    @password NVARCHAR(500),
    @is_locked BIT=0
AS
BEGIN
    DECLARE @existing_member_id INT;
    
    -- Check if the member already exists by email
    SELEC
T @existing_member_id = projectmember
    FROM projectmember
    WHERE emailid = @emailid;
    
    IF @existing_member_id IS NOT NULL
    BEGIN
        -- Update existing member
        UPDATE projectmember
        SET
            name = @name,
			emaili
d=@emailid,
            password = @password,
            is_locked = @is_locked,
            updated_on = GETDATE(),
            updated_by = @existing_member_id,
            update_count = update_count + 1
        WHERE projectmember = @existing_member_
id;
    END
    ELSE
    BEGIN
        -- Insert new member
        INSERT INTO projectmember (
            name, emailid,password,is_locked,created_on)
        VALUES (
            @name,
            @emailid,
            @password,
            @is_locke
d,
            GETDATE()
        );
    END
END;
GO

CREATE PROCEDURE sp_InsertOrUpdateCompany
    @companyid INT = NULL, 
    @company_name NVARCHAR(100),
    @zipcode NVARCHAR(20),
    @countryid INT,
    @stateid INT,
    @cityid INT,
    @email NVARCHAR(255),
    @contactno NVARCHAR(255),
    @pan NVARCHAR(100),
    @website NVARCHAR(100),
    @created_by INT = NULL,
    @updated_by INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF @companyid IS NULL
    BEGIN
        INSERT INTO companymaster 
        (
            companyname, zipcode, countryid, stateid, cityid, 
            email, contactno, pan, website, is_locked, 
            created_by, created_on, update_count
        )
        VALUES 
        (
            @company_name, @zipcode, @countryid, @stateid, @cityid, 
            @email, @contactno, @pan, @website, 0, 
            @created_by, GETDATE(), 0
        );

        SELECT SCOPE_IDENTITY() AS companyid;
    END
    ELSE
    BEGIN
        UPDATE companymaster
        SET 
            companyname = @company_name,
            zipcode = @zipcode,
            countryid = @countryid,
            stateid = @stateid,
            cityid = @cityid,
            email = @email,
            contactno = @contactno,
            pan = @pan,
            website = @website,
            updated_by = @updated_by,
            updated_on = GETDATE(),
            update_count = ISNULL(update_count, 0) + 1
        WHERE companyid = @companyid;

        SELECT @companyid AS companyid;
    END
END;
GO

CREATE PROCEDURE GetLocationData
    @ParentID INT = NULL 
AS
BEGIN
    SELECT id as value, name as display, type, parent_id
    FROM CountryStateCity
    WHERE (@ParentID IS NULL AND type = 'Country') 
       OR (@ParentID IS NOT NULL AND parent_id = @ParentID); 
END;

GO
USE [ipts_db]
GO

/****** Object:  Table [dbo].[CountryStateCity]    Script Date: 17-02-2025 00:24:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CountryStateCity](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](100) NOT NULL,
	[parent_id] [int] NULL,
	[type] [varchar](10) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[CountryStateCity]  WITH CHECK ADD  CONSTRAINT [fk_parent] FOREIGN KEY([parent_id])
REFERENCES [dbo].[CountryStateCity] ([id])
GO

ALTER TABLE [dbo].[CountryStateCity] CHECK CONSTRAINT [fk_parent]
GO

ALTER TABLE [dbo].[CountryStateCity]  WITH CHECK ADD CHECK  (([type]='City' OR [type]='State' OR [type]='Country'))
GO







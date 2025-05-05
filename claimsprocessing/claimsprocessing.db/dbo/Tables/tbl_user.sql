CREATE TABLE [dbo].[tbl_user] (
    [user_id]       INT            IDENTITY (1, 1) NOT NULL,
    [user_fname]    NVARCHAR (50)  NOT NULL,
    [user_mname]    NVARCHAR (50)  NULL,
    [user_lname]    NVARCHAR (50)  NOT NULL,
    [user_fullname] NVARCHAR (150) NULL,
    [user_email]    NVARCHAR (255) NOT NULL,
    [user_password] NVARCHAR (255) NOT NULL,
    [created_on]    DATETIME       CONSTRAINT [DF_tbl_user_created_on] DEFAULT (getdate()) NOT NULL,
    [modified_on]   DATETIME       NULL,
    CONSTRAINT [PK_tbl_user] PRIMARY KEY CLUSTERED ([user_id] ASC)
);


GO

CREATE UNIQUE INDEX [IX_tbl_user_user_email] ON [dbo].[tbl_user] ([user_email])

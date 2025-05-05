CREATE TABLE [dbo].[tbl_claim]
(
	[claim_id]      INT            IDENTITY (1, 1) NOT NULL, 
    [claim_user_id] INT            NOT NULL, 
    [claim_type]    NVARCHAR (256) NOT NULL, 
    [claim_amount]  NUMERIC        NOT NULL, 
    [claim_status]  NVARCHAR (256) NOT NULL, 
    [created_on]    DATETIME       CONSTRAINT [DF_tbl_claim_created_on] DEFAULT (getdate()) NOT NULL, 
    [modified_on]   DATETIME       NULL,
    CONSTRAINT [PK_tbl_claim] PRIMARY KEY CLUSTERED ([claim_id] ASC),
    CONSTRAINT [FK_tbl_claim_tbl_user_claim_user_id_user_id] FOREIGN KEY ([claim_user_id]) REFERENCES tbl_user([user_id]) ON DELETE CASCADE
)

CREATE TABLE [dbo].[tbl_claim_status_update]
(
	[claim_status_update_id] INT           IDENTITY (1, 1) NOT NULL,
    [claim_id]               INT           NOT NULL, 
    [claim_status]           NVARCHAR (10) NOT NULL, 
    [created_on]             DATETIME      CONSTRAINT [DF_tbl_claim_status_update_created_on] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_tbl_claim_status_update] PRIMARY KEY CLUSTERED ([claim_status_update_id] ASC),
    CONSTRAINT [FK_tbl_claim_status_update_tbl_claim_claim_id_claim_id] FOREIGN KEY ([claim_id]) REFERENCES tbl_claim([claim_id])
)

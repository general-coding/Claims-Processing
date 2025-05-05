CREATE TRIGGER [dbo].[trg_tbl_claim_insert]
	ON [dbo].[tbl_claim]
	FOR INSERT
	AS
	BEGIN
		insert into [dbo].[tbl_claim_status_update]
		(
			claim_id, 
			claim_user_id,
			claim_type,
			claim_amount,
			claim_status
		)
		select 
			claim_id, 
			claim_user_id,
			claim_type,
			claim_amount,
			claim_status
		from 
			inserted
	END

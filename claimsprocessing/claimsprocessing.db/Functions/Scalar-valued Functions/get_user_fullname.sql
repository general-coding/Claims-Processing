CREATE FUNCTION [dbo].[get_user_fullname]
(
	@user_id int
)
RETURNS nvarchar(150)
AS
BEGIN
	declare @user_fullname nvarchar(150)
	set @user_fullname = (
							select
								case when user_mname is null or user_mname = ''
								then user_fname + ' ' + user_lname
								else user_fname + ' ' + user_mname + ' ' + user_lname
							end
							from
								dbo.tbl_user
							where
								[user_id] = @user_id
						)

	RETURN @user_fullname
END

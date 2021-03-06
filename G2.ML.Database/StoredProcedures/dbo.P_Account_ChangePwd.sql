
/****** Object:  StoredProcedure [dbo].[P_Account_ChangePwd]    Script Date: 8/19/2017 9:39:13 PM ******/
DROP PROCEDURE [dbo].[P_Account_ChangePwd]
GO
/****** Object:  StoredProcedure [dbo].[P_Account_ChangePwd]    Script Date: 8/19/2017 9:39:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[P_Account_ChangePwd]
	@UserID Int,
	@OldPassword Varchar(100),
	@NewPassword Varchar(100)
As
Begin
Begin Try
	Set NoCount On

	Declare @iUserID Int = 0
	Declare @cOldPassword varchar(100) = ''
		
	Select @iUserID = UserID
		,@cOldPassword = [Password]
	From UserDetails With(Nolock) Where UserID = @UserID;
	
	IF @iUserID = 0
	Begin
		Raiserror('Invalid User',16,1);
	End
	
	If @OldPassword != @cOldPassword
	Begin
		Raiserror('Current Password Does not Match',16,1);
	End

	If @NewPassword = @OldPassword
	Begin
		Raiserror('Old and New Password can not be same',16,1);
	End

	Update UserDetails With (Rowlock)
	Set Password = @NewPassword		
	Where UserID = @UserID;
	
End Try
Begin Catch
	DECLARE @ErrorMessage NVARCHAR(4000);
    DECLARE @ErrorSeverity INT;
    DECLARE @ErrorState INT;
	
	SET @ErrorMessage = ERROR_MESSAGE();
	SET @ErrorSeverity = ERROR_SEVERITY();
	SET @ErrorState = ERROR_STATE();

	Raiserror(@ErrorMessage,@ErrorSeverity,@ErrorState)
End Catch
End


GO

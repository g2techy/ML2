
/****** Object:  StoredProcedure [dbo].[P_Account_SetPwdAttemptCnt]    Script Date: 8/19/2017 9:39:13 PM ******/
DROP PROCEDURE [dbo].[P_Account_SetPwdAttemptCnt]
GO
/****** Object:  StoredProcedure [dbo].[P_Account_SetPwdAttemptCnt]    Script Date: 8/19/2017 9:39:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[P_Account_SetPwdAttemptCnt]
	@UserID Int,
	@IsValidPassword Int
As
Begin
BEGIN TRAN InnerTran;
Begin Try
	Set NoCount On
	
	Update UserDetails With (Rowlock)
	Set PwdAttemptCnt = (Case When @IsValidPassword = 1 Then 0 Else IsNUll(PwdAttemptCnt,0) + 1 End)		
	Where UserID = @UserID;
	
	Commit TRAN InnerTran;
End Try
Begin Catch
	Rollback TRAN InnerTran;

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

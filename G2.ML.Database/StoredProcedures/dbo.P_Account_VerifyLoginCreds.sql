
/****** Object:  StoredProcedure [dbo].[P_Account_VerifyLoginCreds]    Script Date: 8/19/2017 9:39:13 PM ******/
DROP PROCEDURE [dbo].[P_Account_VerifyLoginCreds]
GO
/****** Object:  StoredProcedure [dbo].[P_Account_VerifyLoginCreds]    Script Date: 8/19/2017 9:39:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--Created By : Jitendra Chaudhari (21-03-2017)
--Drop Procedure P_Account_VerifyLoginCreds
/* 
Exec P_Account_VerifyLoginCreds 'admin','admin'
Select * from UserDetails
*/    
CREATE Procedure [dbo].[P_Account_VerifyLoginCreds]
	@UserName Varchar(50),
	@Password Varchar(50)
As    
Begin    
Begin Try    
	Set NoCount On    
     
	Declare @cPassword Varchar(50) = ''
	Declare @iUserID Int = 0
	Declare @iPwdAttemptCnt int  = 0;
	Declare @IsDeleted Int;

	Select @iUserID = UserID    
		  ,@cPassword = [Password]
		  ,@iPwdAttemptCnt = IsNull(PwdAttemptCnt,0)
		  ,@IsDeleted = IsNull(IsDeleted, 0)
	from UserDetails UD With(Nolock)    
	Where UD.UserName = @UserName;

	IF @iUserID = 0
	Begin
		Raiserror('Invalid Login',16,1); 
	End
	
	IF @IsDeleted = 1
	Begin
		Raiserror('Your account is currently disabled.',16,1); 
	End

	IF @iPwdAttemptCnt >= 5
	Begin
		Raiserror('Your account has been locked due to maximum invalid password attempts.',16,1); 
	End

	If @Password <> @cPassword collate sql_latin1_general_cp1_cs_as
	Begin		
		Exec P_Account_SetPwdAttemptCnt @iUserID, 0;
		Raiserror('Invalid username / password',16,1); 
	End
	
    Select UD.UserID, UD.UserName, UD.FirstName, UD.LastName
	From UserDetails UD With (NoLock)
	Where UD.UserID = @iUserID;

	Exec P_Account_SetPwdAttemptCnt @iUserID, 1;

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

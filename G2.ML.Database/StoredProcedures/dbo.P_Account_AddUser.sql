
/****** Object:  StoredProcedure [dbo].[P_Account_AddUser]    Script Date: 8/19/2017 9:39:13 PM ******/
DROP PROCEDURE [dbo].[P_Account_AddUser]
GO
/****** Object:  StoredProcedure [dbo].[P_Account_AddUser]    Script Date: 8/19/2017 9:39:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jitendra Chaudhari
-- Create date: 04-05-2016
-- Description:	Insert user details
-- =============================================
CREATE PROCEDURE [dbo].[P_Account_AddUser](
	@UserID int output,
	@UserName varchar(20),
	@Password varchar(20),
	@FirstName varchar(100),
	@LastName varchar(100),
	@CompanyName varchar(200) = '',
	@CompanyAddress varchar(1000) = '',
	@PhoneNo varchar(100) = '',
	@MobileNo varchar(100) = ''
)
AS
	Declare @UpperLimit int = 999999;
	Declare @LowerLimit int = 100000;
BEGIN
	SET NOCOUNT ON;
    
    SELECT @UserID = ROUND(((@UpperLimit - @LowerLimit -1) * RAND() + @LowerLimit), 0);
    
    While ((Select COUNT(1) From UserDetails UD Where UD.UserID = @UserID) > 0)
    BEGIN
		SELECT @UserID = ROUND(((@UpperLimit - @LowerLimit -1) * RAND() + @LowerLimit), 0);
    END
    
    SET IDENTITY_INSERT UserDetails ON;
    
    Insert Into UserDetails(UserID
	    ,Password
		,UserName
		,FirstName
		,LastName
		,CompanyName
		,CompanyAddress
		,PhoneNo
		,MobileNo)
    Values(@UserID, @Password, @UserName, @FirstName, @LastName, @CompanyName, @CompanyAddress, @PhoneNo, @MobileNo);
    
    SET IDENTITY_INSERT UserDetails OFF;
        
    /*Add default broker...*/
    Exec P_Buyer_AddUpdate @ClientID = @UserID
						  ,@BuyerID = 0
						  ,@BuyerCode = N'Self'
						  ,@FirstName = @FirstName
						  ,@LastName = @LastName
						  ,@PhoneNo = @PhoneNo
						  ,@MobileNo = @MobileNo
						  ,@BuyerTypes = N'3';
	
END



GO

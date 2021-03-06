
/****** Object:  StoredProcedure [dbo].[P_Account_GetLoggedInUserDetails]    Script Date: 8/19/2017 9:39:13 PM ******/
DROP PROCEDURE [dbo].[P_Account_GetLoggedInUserDetails]
GO
/****** Object:  StoredProcedure [dbo].[P_Account_GetLoggedInUserDetails]    Script Date: 8/19/2017 9:39:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jitendra Chaudhari
-- Create date: 04-05-2016
-- Description:	Get Logged in user details
-- =============================================
CREATE PROCEDURE [dbo].[P_Account_GetLoggedInUserDetails](
	@UserName varchar(100)
)
AS
   
BEGIN
	
	Select UD.UserID, 
		   UD.UserName, 
		   (UD.FirstName + ' ' + UD.LastName) As FullName 
	  From UserDetails UD 
	 Where UD.UserName = @UserName;	
        
END



GO

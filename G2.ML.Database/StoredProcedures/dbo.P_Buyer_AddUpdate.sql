
/****** Object:  StoredProcedure [dbo].[P_Buyer_AddUpdate]    Script Date: 8/19/2017 9:39:13 PM ******/
DROP PROCEDURE [dbo].[P_Buyer_AddUpdate]
GO
/****** Object:  StoredProcedure [dbo].[P_Buyer_AddUpdate]    Script Date: 8/19/2017 9:39:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Jitendra Chaudhari
-- Create date: 09-04-2016
-- Description:	Insert/Update buyer details
-- =============================================
CREATE PROCEDURE [dbo].[P_Buyer_AddUpdate](
	@ClientID int,
	@BuyerID int output,
	@BuyerCode varchar(20),
	@FirstName varchar(100),
	@LastName varchar(100),
	@PhoneNo varchar(20) = '',
	@MobileNo varchar(20) = '',
	@BuyerTypes varchar(100)
)
AS
	Declare @NewBuyerID int;
BEGIN
	SET NOCOUNT ON;

	IF (@BuyerID = 0)
		Begin
		
		Insert into BuyerDetails(UserID
		  ,BuyerCode
		  ,FirstName
		  ,LastName
		  ,PhoneNo
		  ,MobileNo
		  ,IsDeleted)
		Values(@ClientID, @BuyerCode, @FirstName, @LastName, @PhoneNo, @MobileNo, 0);
      
        Select @NewBuyerID = SCOPE_IDENTITY();
        
		End
	Else
		Begin			
			Update BuyerDetails 
				Set BuyerCode = @BuyerCode,
					FirstName = @FirstName,
					LastName = @LastName,
					PhoneNo = @PhoneNo,
					MobileNo = @MobileNo
			 Where BuyerID = @BuyerID
			   And UserID = @ClientID;
			
			Select @NewBuyerID = @BuyerID;
		End
		
	If @BuyerTypes != '' 
	Begin
		Delete From BuyerTypeMapping 
		Where BuyerID = @NewBuyerID;
				
		Insert Into BuyerTypeMapping(BuyerID, BuyerTypeID, Status)
		Select @NewBuyerID, Item, 1 From F_Cols_To_Rows(@BuyerTypes,',');
		
	End;
	    
	Set @BuyerID = @NewBuyerID;
	
END



GO

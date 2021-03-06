
/****** Object:  StoredProcedure [dbo].[P_Sale_AddUpdate]    Script Date: 8/19/2017 9:39:13 PM ******/
DROP PROCEDURE [dbo].[P_Sale_AddUpdate]
GO
/****** Object:  StoredProcedure [dbo].[P_Sale_AddUpdate]    Script Date: 8/19/2017 9:39:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jitendra Chaudhari
-- Create date: 25-03-2016
-- Description:	Insert/Update sales details
-- =============================================
CREATE PROCEDURE [dbo].[P_Sale_AddUpdate](
	@ClientID int,
	@SaleID int output,
	@DueDays int,
	@SaleDate Date,
	@Saller int,
	@Buyer int,
	@TotalWeight float,
	@RejectionWeight float,
	@UnitPrice float,
	@LessPer float
)
AS
BEGIN
	SET NOCOUNT ON;

	IF (@SaleID = 0)
		Begin
		
		Insert into SalesDetails([SaleDate]
		  ,[UserID]
		  ,[SallerID]
		  ,[BuyerID]
		  ,[Weight]
		  ,[RejectionWt]
		  ,[UnitPrice]
		  ,[DueDays]
		  ,[RefNo]
		  ,[LessPer])
		Values(@SaleDate, @ClientID, @Saller, @Buyer, @TotalWeight, @RejectionWeight, @UnitPrice, @DueDays,
		  dbo.F_Sale_GenerateRefNo(@SaleDate), @LessPer);
      
        Set @SaleID = SCOPE_IDENTITY();
        
		End
	Else
		Begin			
			Update SalesDetails 
				Set [SaleDate] = @SaleDate,
					[SallerID] = @Saller,
					[BuyerID] = @Buyer,
					[Weight] = @TotalWeight,
					[RejectionWt] = @RejectionWeight,
					[UnitPrice] = @UnitPrice,
					[DueDays] = @DueDays,
					[LessPer] = @LessPer
			 Where [SaleID] = @SaleID
			   And [UserID] = @ClientID;
						
		End	    
END

GO


/****** Object:  StoredProcedure [dbo].[P_Sale_UpdateStatus]    Script Date: 8/19/2017 9:39:13 PM ******/
DROP PROCEDURE [dbo].[P_Sale_UpdateStatus]
GO
/****** Object:  StoredProcedure [dbo].[P_Sale_UpdateStatus]    Script Date: 8/19/2017 9:39:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jitendra Chaudhari
-- Create date: 05-05-2016
-- Description:	Update sales status column for given SaleID
-- =============================================
CREATE PROCEDURE [dbo].[P_Sale_UpdateStatus]( 
	@SaleID int,
	@Status int = 0
)
AS
    Declare @NetAmout float;
    Declare @PayAmount float;
    Declare @NewStatus int;
    Declare @OldStaus int;    
BEGIN
	
	Select @OldStaus = Status From SalesDetails
	Where SaleID = @SaleID;
	
	If @OldStaus = 4
	BEGIN
		Return;
	END
	
	IF @Status <> 0 
	BEGIN	
		Update SalesDetails Set Status = @Status
		 Where SaleID = @SaleID;
	END
	ELSE
	BEGIN
		Select @NetAmout = ISNull(V.NetSaleAmount,0),
			   @PayAmount = ISNull(V.TotalPayAmount,0)
		  From V_Sale_SaleList V
		Where V.SaleID = @SaleID;
		
		If @PayAmount >= @NetAmout 
			Select @NewStatus = 3;
		Else If @PayAmount = 0 
			Select @NewStatus = 1;
		Else
			 Select @NewStatus = 2;
		
		Update SalesDetails Set Status = @NewStatus
		 Where SaleID = @SaleID;
		
	END	
	
END


GO

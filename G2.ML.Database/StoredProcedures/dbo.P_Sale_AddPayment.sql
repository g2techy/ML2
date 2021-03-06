
/****** Object:  StoredProcedure [dbo].[P_Sale_AddPayment]    Script Date: 8/19/2017 9:39:13 PM ******/
DROP PROCEDURE [dbo].[P_Sale_AddPayment]
GO
/****** Object:  StoredProcedure [dbo].[P_Sale_AddPayment]    Script Date: 8/19/2017 9:39:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Jitendra Chaudhari
-- Create date: 26-03-2016
-- Description:	Insert brokerage details for given SaleID
-- =============================================
CREATE PROCEDURE [dbo].[P_Sale_AddPayment]( 
	@PayID int output,
	@SaleID int,
	@PayDate Date,
	@PayAmount float,
	@CourierFrom nvarchar(100),
	@CourierTo nvarchar(100)
)
AS
BEGIN
	
	Insert INto PaymentDetails(SaleID, PayDate, PayAmount, PayCourierFrom, PayCourierTo)
	Values(@SaleID, @PayDate, @PayAmount, @CourierFrom, @CourierTo);
	
	Exec P_Sale_UpdateStatus @SaleID = @SaleID;
	
	Set @PayID = SCOPE_IDENTITY();
	
END


GO

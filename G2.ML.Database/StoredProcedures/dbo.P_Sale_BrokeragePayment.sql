
/****** Object:  StoredProcedure [dbo].[P_Sale_BrokeragePayment]    Script Date: 8/19/2017 9:39:13 PM ******/
DROP PROCEDURE [dbo].[P_Sale_BrokeragePayment]
GO
/****** Object:  StoredProcedure [dbo].[P_Sale_BrokeragePayment]    Script Date: 8/19/2017 9:39:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Jitendra Chaudhari
-- Create date: 03-04-2017
-- Description:	Update brokerage payment details for given BDID
-- =============================================
CREATE PROCEDURE [dbo].[P_Sale_BrokeragePayment]( 
	@BDID int,
	@PayDate Date,
	@PayComments nvarchar(1000)
)
AS
BEGIN
	
	Update BrokerageDetails
	   Set IsPaid = 1,
	       PayDate = @PayDate,
		   PayComments = @PayComments
	 Where BDID = @BDID;
			
END


GO

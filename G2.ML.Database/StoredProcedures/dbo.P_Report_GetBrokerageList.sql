
/****** Object:  StoredProcedure [dbo].[P_Report_GetBrokerageList]    Script Date: 8/19/2017 9:39:13 PM ******/
DROP PROCEDURE [dbo].[P_Report_GetBrokerageList]
GO
/****** Object:  StoredProcedure [dbo].[P_Report_GetBrokerageList]    Script Date: 8/19/2017 9:39:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Jitendra Chaudhari
-- Create date: 05-05-2016
-- Description:	Search sale list
-- =============================================
CREATE PROCEDURE [dbo].[P_Report_GetBrokerageList](
	@ClientID int,
	@StartDate varchar(100) = '',
	@EndDate varchar(100) = '',
	@SallerID int = 0,
	@BuyerID int = 0,
	@Status int = 0
)
AS
  Declare @v_Query varchar(4000);
  Declare @v_WhereClause varchar(4000);

BEGIN
	
  Set @v_WhereClause  = ' And 1 = 1';
  
  Set @v_Query = 'Select RefNo
				  ,Replace(Convert(varchar(11),SaleDate,106), '' '', ''-'')  As [Sale Date]
				  ,Saller
				  ,Buyer
				  ,cast(TotalWeight as Decimal(10,2)) As [Total Wt]
				  ,cast(RejectionWt as Decimal(10,2))  As [Rejection %]
				  ,cast(SelectionWt as Decimal(10,2))  AS [Selection Wt]
				  ,cast(UnitPrice as Decimal(10,2)) as [Unit Price]
				  ,cast(LessPer as Decimal(10,2)) As [Less %]
				  ,cast(NetSaleAmount as Decimal(10,2)) As [Net Sale Amount]
				  
				  ,cast(SelfBrokerage as Decimal(10,2)) As [Self Brokerage %]
				  ,cast(SelfBrokerageAmount as Decimal(10,2)) As [Self Brokerage Amount]
				  ,cast(OtherBrokerage as Decimal(10,2)) As [Other Brokerage %]
				  ,cast(OtherBrokerageAmount as Decimal(10,2)) As [Other Brokerage Amount]
				  
				  ,DueDays As [Due Days]
				  ,cast(TotalPayAmount as Decimal(10,2)) As [Total Pay Amount]
				  ,Replace(Convert(varchar(11),PayDate,106), '' '', ''-'')  As [Pay Date]
				  ,Status
			 From V_Sale_SaleList_With_Brokerage V
			Where V.UserID = ' + Cast(@ClientID as Varchar);
				 
  If @StartDate != '' And @EndDate != ''
  BEGIN
	Set @v_WhereClause = ' And V.SaleDate Between ''' + @StartDate + ''' And ''' + @EndDate + '''';
  END
  Else If @StartDate != ''
  BEGIN
	Set @v_WhereClause = ' And V.SaleDate >= ''' + @StartDate + '''';
  END
  Else If @EndDate != ''
  BEGIN
	Set @v_WhereClause = ' And V.SaleDate <= ''' + @EndDate + '''';
  END

  If @SallerID != 0 
  Begin
	Set @v_WhereClause = @v_WhereClause + ' And V.SallerID = ' + Cast(@SallerID As varchar(10));
  End	
  
  If @BuyerID != 0 
  Begin
	Set @v_WhereClause = @v_WhereClause + ' And V.BuyerID = ' + Cast(@BuyerID As varchar(10));
  End
  
  If @Status = 1 
  Begin
	Set @v_WhereClause = @v_WhereClause + ' And V.SelfBrokerage = 0 And V.OtherBrokerage = 0 ';
  End
  Else If @Status = 2 
  Begin
	Set @v_WhereClause = @v_WhereClause + ' And (V.SelfBrokerage > 0 Or V.OtherBrokerage > 0) ';
  End
  
  Set @v_Query = @v_Query + @v_WhereClause;
  
  Print ' Query: ' + @v_Query
    
  Execute(@v_Query);
      
END

GO

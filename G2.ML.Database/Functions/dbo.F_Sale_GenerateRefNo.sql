
/****** Object:  UserDefinedFunction [dbo].[F_Sale_GenerateRefNo]    Script Date: 8/19/2017 9:39:13 PM ******/
DROP FUNCTION [dbo].[F_Sale_GenerateRefNo]
GO
/****** Object:  UserDefinedFunction [dbo].[F_Sale_GenerateRefNo]    Script Date: 8/19/2017 9:39:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[F_Sale_GenerateRefNo]
(
	@SaleDate Date
) 
RETURNS varchar(20)
AS
BEGIN
  Declare @RefNo varchar(20);
  Declare @Count int = 0;

  Select @Count = (IsNull(COUNT(1),0) + 1) From SalesDetails SD 
   Where CONVERT(DATE, SD.SaleDate) = CONVERT (DATE, @SaleDate);

  Select @RefNo = Replace(CONVERT(VARCHAR(10), @SaleDate, 111),'/','-') 
				  + '-' + RIGHT('00000'+CAST(@Count AS VARCHAR(5)),5);
  Return @RefNo;
  	
END

GO

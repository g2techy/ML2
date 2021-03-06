
/****** Object:  UserDefinedFunction [dbo].[GetDateBeforeMonths]    Script Date: 8/19/2017 9:39:13 PM ******/
DROP FUNCTION [dbo].[GetDateBeforeMonths]
GO
/****** Object:  UserDefinedFunction [dbo].[GetDateBeforeMonths]    Script Date: 8/19/2017 9:39:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jitendra Chaudhari
-- Create date: 31-03-2017
-- Description:	Get date before/after few months
-- =============================================
CREATE FUNCTION [dbo].[GetDateBeforeMonths] 
(
	@EndDate Date,
	@Interval Int
)
RETURNS Date
AS
BEGIN
	-- Declare the return variable here
	DECLARE @StDate Date;

	Set @StDate = DATEADD(mm,@Interval+1, getdate()); 
	Set @StDate = DATEADD(DAY,-1 * (DATEPART(DAY,@StDate) - 1), @StDate);

	-- Return the result of the function
	RETURN @StDate;

END

GO

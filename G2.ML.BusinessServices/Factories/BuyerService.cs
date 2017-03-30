using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BO = G2.ML.BusinessObjects;

namespace G2.ML.BusinessServices.Factories
{
    public class BuyerService : BaseService, Contracts.IBuyerService
	{

        #region IBuyerRepository Members

        public List<BO.BuyerTypeBO> GetBuyerTypeList()
        {
            List<BO.BuyerTypeBO> _list = new List<BO.BuyerTypeBO>();

			try
			{
				DatabaseAccess.OpenConnection();
				using (DataSet _ds = DatabaseAccess.ExecuteProcedure("P_Buyer_BuyerTypeList", null))
				{
					if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
					{
						foreach (DataRow _dr in _ds.Tables[0].Rows)
						{
							_list.Add(new BO.BuyerTypeBO()
							{
								BuyerTypeID = int.Parse(_dr["BuyerTypeID"].ToString()),
								BuyerTypeName = _dr["BuyerTypeName"].ToString()
							});
						}
					}

				}
			}
			catch
			{
				throw;
			}
			finally
			{
				DatabaseAccess.CloseConnection();
			}

			return _list;
        }

        public int Add(BO.BuyerBO buyer)
        {
			int _ID;
			try
			{
				DatabaseAccess.OpenConnection(true);
				var _ourParams = DatabaseAccess.ExecuteProcedureDML("P_Buyer_AddUpdate", new List<DAL.DatabaseParameter>()
				{
					new DAL.DatabaseParameter("@BuyerID",DAL.ParameterDirection.InOut, DAL.DataType.Int, buyer.BuyerID),
					new DAL.DatabaseParameter("@ClientID",DAL.ParameterDirection.In,DAL.DataType.Int, buyer.ClientID),
					new DAL.DatabaseParameter("@BuyerCode",DAL.ParameterDirection.In,DAL.DataType.String, buyer.BuyerCode, 20),
					new DAL.DatabaseParameter("@FirstName",DAL.ParameterDirection.In,DAL.DataType.String, buyer.FirstName, 100),
					new DAL.DatabaseParameter("@LastName",DAL.ParameterDirection.In,DAL.DataType.String, buyer.LastName, 100),
					new DAL.DatabaseParameter("@PhoneNo",DAL.ParameterDirection.In,DAL.DataType.String, buyer.PhoneNo),
					new DAL.DatabaseParameter("@MobileNo",DAL.ParameterDirection.In,DAL.DataType.String, buyer.MobileNo),
					new DAL.DatabaseParameter("@BuyerTypes",DAL.ParameterDirection.In,DAL.DataType.String, string.Join(",",buyer.SelectedBuyerTypes), 100)
				});
				DatabaseAccess.CommitTransaction();
				_ID = Convert.ToInt32(_ourParams["@BuyerID"]);
				_ourParams = null;
			}
			catch
			{
				DatabaseAccess.RollbackTransaction();
				throw;
			}
			finally
			{
				DatabaseAccess.CloseConnection();
			}
			return _ID;
        }

        public int Delete(int clientID, int buyerID)
        {
			int _ID;
			try
			{
				DatabaseAccess.OpenConnection(true);
				var _ourParams = DatabaseAccess.ExecuteProcedureDML("P_Buyer_Delete", new List<DAL.DatabaseParameter>()
				{
					new DAL.DatabaseParameter("@BuyerID",DAL.ParameterDirection.In, DAL.DataType.Int, buyerID),
					new DAL.DatabaseParameter("@ClientID",DAL.ParameterDirection.In,DAL.DataType.Int, clientID)
				});
				DatabaseAccess.CommitTransaction();
				_ID = buyerID;
				_ourParams = null;
			}
			catch
			{
				DatabaseAccess.RollbackTransaction();
				throw;
			}
			finally
			{
				DatabaseAccess.CloseConnection();
			}
			return _ID;
		}

        public BO.BuyerBO GetBuyerDetails(int clientID, int buyerID)
        {
            BO.BuyerBO _buyer = null;
			try
			{
				DatabaseAccess.OpenConnection();
				using (DataSet _ds = DatabaseAccess.ExecuteProcedure("P_Buyer_GetDetails", new List<DAL.DatabaseParameter>()
				{
					new DAL.DatabaseParameter("@BuyerID",DAL.ParameterDirection.In, DAL.DataType.Int, buyerID),
					new DAL.DatabaseParameter("@ClientID",DAL.ParameterDirection.In,DAL.DataType.Int, clientID)
				}))
				{
					if (_ds != null && _ds.Tables.Count > 0)
					{
						var _row = _ds.Tables[0].Rows[0];
						_buyer = new BO.BuyerBO()
						{
							BuyerID = Convert.ToInt32(_row["BuyerID"]),
							BuyerCode = Convert.ToString(_row["BuyerCode"]),
							FirstName = Convert.ToString(_row["FirstName"]),
							LastName = Convert.ToString(_row["LastName"]),
							PhoneNo = Convert.ToString(_row["PhoneNo"]),
							MobileNo = Convert.ToString(_row["MobileNo"])
						};
						string _buyerTypeList = Convert.ToString(_row["BuyerTypes"]);
						if (!string.IsNullOrEmpty(_buyerTypeList))
						{
							_buyer.SelectedBuyerTypes = _buyerTypeList.Split(',').ToList();
						}
					}
				}
			}
			catch
			{
				throw;
			}
			finally
			{
				DatabaseAccess.CloseConnection();
			}

			return _buyer;
        }

        public BO.BuyerSearchResultBO GetBuyerList(BO.BuyerSearchBO buyerSearch)
        {
			BO.BuyerSearchResultBO _returnVal = null;

			try
			{
				DatabaseAccess.OpenConnection();
				using (DataSet _ds = DatabaseAccess.ExecuteProcedure("P_Buyer_GetBuyerList", new List<DAL.DatabaseParameter>()
				{
					new DAL.DatabaseParameter("@ClientID",DAL.ParameterDirection.In,DAL.DataType.Int, buyerSearch.ClientID),
					new DAL.DatabaseParameter("@StartIndex",DAL.ParameterDirection.In, DAL.DataType.Int, buyerSearch.StartIndex),
					new DAL.DatabaseParameter("@PageSize",DAL.ParameterDirection.In, DAL.DataType.Int, buyerSearch.PageSize),
					new DAL.DatabaseParameter("@BuyerCode",DAL.ParameterDirection.In, DAL.DataType.String, buyerSearch.BuyerCode, 20),
					new DAL.DatabaseParameter("@FirstName",DAL.ParameterDirection.In, DAL.DataType.String, buyerSearch.FirstName, 100),
					new DAL.DatabaseParameter("@LastName",DAL.ParameterDirection.In, DAL.DataType.String, buyerSearch.LastName, 100)
				}))
				{
					if (_ds != null && _ds.Tables.Count > 0)
					{
						int _totalRecords = 0;
						if (_ds.Tables[0].Rows.Count > 0)
						{
							_totalRecords = int.Parse(_ds.Tables[0].Rows[0]["Row_Count"].ToString());
						}

						_returnVal = new BO.BuyerSearchResultBO(_totalRecords);

						foreach (DataRow _row in _ds.Tables[0].Rows)
						{
							var _buyerBM = new BO.BuyerBO()
							{
								BuyerID = Convert.ToInt32(_row["BuyerID"]),
								BuyerCode = Convert.ToString(_row["BuyerCode"]),
								FirstName = Convert.ToString(_row["FirstName"]),
								LastName = Convert.ToString(_row["LastName"]),
								PhoneNo = Convert.ToString(_row["PhoneNo"]),
								MobileNo = Convert.ToString(_row["MobileNo"]),
								SelectedBuyerTypes =  new List<string>() { Convert.ToString(_row["BuyerTypeNames"]) }
							};
							_returnVal.BuyerList.Add(_buyerBM);
						}
						
					}
				}
			}
			catch
			{
				throw;
			}
			finally
			{
				DatabaseAccess.CloseConnection();
			}

			return _returnVal;
        }

        #endregion
    }
}

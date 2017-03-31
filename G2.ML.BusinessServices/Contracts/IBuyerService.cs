using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BO = G2.ML.BusinessObjects;

namespace G2.ML.BusinessServices.Contracts
{
    public interface IBuyerService : IService
	{
        List<BO.BuyerTypeBO> GetBuyerTypeList();
        int Add(BO.BuyerBO buyer);
        int Delete(int clientID, int buyerID);
        BO.BuyerBO GetBuyerDetails(int clientID, int buyerID);
        BO.BuyerSearchResultBO GetBuyerList(BO.BuyerSearchBO buyerSearch);
    }
}

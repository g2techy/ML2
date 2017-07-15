using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BO = G2.ML.BusinessObjects;

namespace G2.ML.BusinessServices.Contracts
{
    public interface ISaleService
    {
        int Add(BO.SaleAddBO saleAdd);
        int Delete(int clientID, int saleID);
        BO.SaleAddBO GetSaleDetails(int clientID, int saleID);
        List<BO.Buyer> GetBuyerList(int clientID, int buyerTypeID);
        BO.SaleSearchResultBO GetSalesList(BO.SaleSearchBO saleSearch);

        List<BO.SaleBrokerageBO> GetBrokerageList(int clientID, int saleID);
        int AddBrokerage(BO.SaleBrokerageBO brokerage);
		int UpdateBrokeragePayment(BO.SaleBrokerageBO brokerage);
		int DeleteBrokerage(int BDID);

        List<BO.SalePaymentBO> GetPaymentList(int clientID, int saleID);
        int AddPayment(BO.SalePaymentBO payment);
        int DeletePayment(int payID);

        int CloseSale(int saleID);
    }
}

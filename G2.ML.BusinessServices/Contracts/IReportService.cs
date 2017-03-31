using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BO = G2.ML.BusinessObjects;
using System.Data;

namespace G2.ML.BusinessServices.Contracts
{
    public interface IReportService : IService
    {
        List<BO.SaleStatusBO> GetSaleStatusList();
        DataTable GetSalesReport(BO.SalesReportBO bo);
        DataTable GetBrokerageReport(BO.BrokerageReportBO bo);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace G2.ML.BusinessObjects
{
    public class BuyerSearchBO : PagerBO
    {
        public int ClientID { get; set; }
        public string BuyerCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class BuyerSearchResultBO
    {
        public int ClientID { get; set; }
        public int RecordCount { get; private set; }

        public List<BuyerBO> BuyerList { get; set; }

        public BuyerSearchResultBO(int recCnt)
        {
            RecordCount = recCnt;
            BuyerList = new List<BuyerBO>();
        }
    }
}

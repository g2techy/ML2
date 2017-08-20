using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO = G2.ML.BusinessObjects;

namespace G2.ML.BusinessServices.Contracts
{
	public interface ILoanService
	{
		int Add(BO.LoanAddBO loanAdd);
		int Delete(int clientID, int loanID);
		BO.LoanAddBO GetLoanDetails(int clientID, int saleID);
		List<BO.Buyer> GetBorrowerList(int clientID, int buyerTypeID);
		BO.LoanSearchResultBO GetLoanList(BO.LoanSearchBO saleSearch);
		int Close(int loanPayID);
		List<BO.LoanPaymentBO> GetPaymentList(int clientID, int loanID);
		int AddPayment(BO.LoanPaymentBO payment);
		int DeletePayment(int loanPayID);
		List<BO.LoanCalcInterestBO> GetCalcInterest(int clientID, int loanID, DateTime intAsOn);
	}
}

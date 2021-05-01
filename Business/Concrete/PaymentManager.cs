using Business.Constants;
using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Concrete
{
    public class PaymentManager : IPaymentService
    {
        // test için
        public IResult MakePayment(Payment payment)
        {
            if (payment.Amount < 1000)
            {
                return new ErrorResult(Messages.PaymentError);
            }
            return new SuccessResult();
        }
    }
}

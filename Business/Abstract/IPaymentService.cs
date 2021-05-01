using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Concrete
{
    public interface IPaymentService
    {
        IResult MakePayment(Payment amount);
    }

}

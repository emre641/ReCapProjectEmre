using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CreditCardManager : ICreditCardService
    {
        ICreditCardDAL _creditCardDAL;

        public CreditCardManager(ICreditCardDAL creditCardDAL)
        {
            _creditCardDAL = creditCardDAL;
        }

        [SecuredOperation("admin,user")]
        [CacheRemoveAspect("ICreditCardService.Get")]
        public IResult Add(CreditCard creditCard)
        {
            IResult result = BusinessRules.Run(IsCardExist(creditCard));

            if (result != null)
            {
                return result;
            }
            _creditCardDAL.Add(creditCard);
            return new SuccessResult();
        }

        [SecuredOperation("admin,user")]
        [CacheRemoveAspect("ICreditCardService.Get")]
        public IResult Delete(CreditCard creditCard)
        {
            _creditCardDAL.Delete(creditCard);
            return new SuccessResult();
        }

        [SecuredOperation("admin,user")]
        [CacheRemoveAspect("ICreditCardService.Get")]
        public IResult Update(CreditCard creditCard)
        {
            _creditCardDAL.Update(creditCard);
            return new SuccessResult();
        }

        [SecuredOperation("admin")]
        [CacheAspect]
        public IDataResult<List<CreditCard>> GetAll()
        {
            return new SuccessDataResult<List<CreditCard>>(_creditCardDAL.GetAll());
        }

        [SecuredOperation("admin,user")]
        [CacheAspect]
        public IDataResult<List<CreditCard>> GetByCustomer(int id)
        {
            return new SuccessDataResult<List<CreditCard>>(_creditCardDAL.GetAll(c => c.CustomerId == id));
        }
        private IResult IsCardExist(CreditCard creditCard)
        {
            var result = _creditCardDAL.Get(c =>
            c.NameOnTheCard == creditCard.NameOnTheCard
            && c.CardNumber == creditCard.CardNumber
            && c.CVV == creditCard.CVV
            && c.ExpirationDate == creditCard.ExpirationDate);

            if (result != null)
            {
                return new ErrorResult(Messages.CardExist);
            }

            return new SuccessResult();
        }


    }
}

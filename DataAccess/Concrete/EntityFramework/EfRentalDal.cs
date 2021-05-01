using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfRentalDal : EfEntityRepositoryBase<Rental, CarDatabaseContext>, IRentalDal
    {
        public RentalDetailDto GetRentalDetails(int id)
        {
            using (CarDatabaseContext context = new CarDatabaseContext())
            {
                var result = from rental in context.Rentals.Where(r => r.RentalId == id)

                             join car in context.Cars
                                 on rental.CarId equals car.Id

                             join brand in context.Brands
                                 on rental.CustomerId equals brand.BrandId

                             join customer in context.Customers
                        on rental.CustomerId equals customer.CustomerId

                             join user in context.Users
                                 on customer.UserId equals user.Id

                             select new RentalDetailDto
                             {
                                 Id = rental.RentalId,
                                 CarID = car.Id,
                                 BrandName = brand.BrandName,
                                 UserName = user.FirstName + " " + user.LastName,
                                 CompanyName = customer.CompanyName,
                                 RentDate = rental.RentDate,
                                 ReturnDate = rental.ReturnDate
                             };

                return result.SingleOrDefault();
            }
        }

        public List<RentalDetailDto> GetRentalsDetails()
        {
            using (CarDatabaseContext context = new CarDatabaseContext())
            {
                var result = from rental in context.Rentals

                             join car in context.Cars
                             on rental.CarId equals car.Id

                             join brand in context.Brands
                             on car.BrandId equals brand.BrandId

                             join customer in context.Customers
                             on rental.CustomerId equals customer.CustomerId

                             join user in context.Users
                             on customer.UserId equals user.Id

                             select new RentalDetailDto
                             {
                                 Id = rental.RentalId,
                                 CarID = car.Id,
                                 BrandName = brand.BrandName,
                                 UserName = user.FirstName + " " + user.LastName,
                                 CompanyName = customer.CompanyName,
                                 RentDate = rental.RentDate,
                                 ReturnDate = rental.ReturnDate
                             };
                Debug.Write("asdfghjk--------" + result.ToList());

                return result.ToList();
            }
        }
    }
}

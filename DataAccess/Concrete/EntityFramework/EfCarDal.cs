using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : EfEntityRepositoryBase<Car, CarDatabaseContext>, ICarDal
    {
        //public List<CarDetailDto> GetCarDetails()
        //{
        //    using (CarDatabaseContext context=new CarDatabaseContext())
        //    {
        //        var result = from c in context.Cars
        //                     join b in context.Brands
        //                     on c.BrandId equals b.BrandId
        //                     join co in context.Colors
        //                     on c.ColorId equals co.ColorId
        //                     select new CarDetailDto
        //                     {
        //                         Id = c.Id,
        //                         BrandName = b.BrandName,
        //                         ColorName = co.ColorName,
        //                         DailyPrice = c.DailyPrice
        //                     };
        //        return result.ToList();


        //    }
        //}

        public CarDetailDto GetCarDetails(int carId)
        {
            using (CarDatabaseContext context = new CarDatabaseContext())
            {
                var result = from car in context.Cars.Where(c => c.Id == carId)

                             join color in context.Colors
                             on car.ColorId equals color.ColorId

                             join brand in context.Brands
                             on car.BrandId equals brand.BrandId

                             select new CarDetailDto()
                             {
                                 BrandId = brand.BrandId,
                                 ColorId = color.ColorId,
                                 BrandName = brand.BrandName,
                                 ColorName = color.ColorName,
                                 DailyPrice = car.DailyPrice,
                                 Description = car.Description,
                                 ModelYear = car.ModelYear,
                                 Id = car.Id,
                                 //MinFindexScore = car.MinFindexScore
                             };

                return result.SingleOrDefault();
            }
        }

        public List<CarDetailDto> GetCarsDetails(Expression<Func<CarDetailDto, bool>> filter = null)
        {
            using (CarDatabaseContext context = new CarDatabaseContext())
            {
                var result = from car in context.Cars

                             join color in context.Colors
                             on car.ColorId equals color.ColorId

                             join brand in context.Brands
                             on car.BrandId equals brand.BrandId

                             select new CarDetailDto()
                             {
                                 Id = car.Id,
                                 Description = car.Description,
                                 BrandId = brand.BrandId,
                                 BrandName = brand.BrandName,
                                 ColorId = color.ColorId,
                                 ColorName = color.ColorName,
                                 DailyPrice = car.DailyPrice,
                                 ModelYear = car.ModelYear,
                                 //MinFindexScore = car.MinFindexScore
                             };

                return filter == null ? result.ToList() : result.Where(filter).ToList();
            }
        }
    }
}

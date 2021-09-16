using carportal.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using carportal.Data;
using Microsoft.EntityFrameworkCore;
using carportal.Models;

namespace carportal.Repositories.CarRepo
{
    public class CarRepo : ICarRepo
    {

        private readonly DataContext _context;

        public CarRepo(DataContext dataContext) {

            _context = dataContext;
     
        }

        public async Task<List<Car>> createCar(Car car)
        {

            await _context.Cars.AddAsync(car);
            await _context.SaveChangesAsync();

            return await _context.Cars.ToListAsync();

        }

        public async Task<List<Car>> deleteCar(Car car)
        {
            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();

            return await _context.Cars.ToListAsync();

        }

        public async Task<Car> getCar(int id)
        {
            return await _context.Cars.FirstOrDefaultAsync(c => c.id == id); 
        }

        public async Task<List<Car>> getCars() {

            return await _context.Cars.ToListAsync();
        }

        public async Task<List<Car>> getCarsByBrand(string brand)
        {
            return await _context.Cars.Where(c => c.brand.Equals(brand)).ToListAsync(); 

        }

        public async Task<Car> updateCar(Car car)
        {
            _context.Cars.Update(car);
            await _context.SaveChangesAsync();

            return car;
        }
    }
}

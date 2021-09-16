using System.Collections.Generic;
using System.Threading.Tasks;
using carportal.Models;

namespace carportal.Repositories.CarRepo
{
    public interface ICarRepo
    {
        Task<List<Car>> getCars();

        Task<Car> getCar(int id);

        Task<List<Car>> createCar(Car car);

        Task<List<Car>> deleteCar(Car car);

        Task<Car> updateCar(Car car);

        Task<List<Car>> getCarsByBrand(string brand);


    }
}

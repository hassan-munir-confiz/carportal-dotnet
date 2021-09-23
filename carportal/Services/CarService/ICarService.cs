using System.Collections.Generic;
using System.Threading.Tasks;
using carportal.Models.Dtos;

namespace carportal.Models
{
    public interface ICarService
    {
        Task<ServiceResponse<List<GetCarDto>>> getCars();

        Task<ServiceResponse<List<GetCarDto>>> getCarsByBrand(string brand);

        Task<ServiceResponse<GetCarDto>> getCar(int id);

        Task<ServiceResponse<List<GetCarDto>>> createCar(CreateCarDto car);

        Task<ServiceResponse<GetCarDto>> updateCar(UpdateCarDto updateCar);

        Task<ServiceResponse<List<GetCarDto>>> deleteCar(int id);

        ServiceResponse<string> getStatus();
        
    }
}
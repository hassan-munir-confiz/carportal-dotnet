using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using carportal.Models;
using carportal.Models.Dtos;
using carportal.Repositories.CarRepo;

namespace carportal.Services.CarService
{
    public class CarService : ICarService
    {


        private readonly IMapper _mapper;

        private readonly ICarRepo _carRepo;
        public CarService(IMapper mapper, ICarRepo carRepo)
        {
            _mapper = mapper;
            _carRepo = carRepo;

        }

        public async Task<ServiceResponse<List<GetCarDto>>> getCars()
        {
            ServiceResponse<List<GetCarDto>> serviceResponse = new ServiceResponse<List<GetCarDto>>();
            List<Car> dbCars = await _carRepo.getCars();

            serviceResponse.Data = (dbCars.Select(c => _mapper.Map<GetCarDto>(c))).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCarDto>> getCar(int id)
        {
            ServiceResponse<GetCarDto> serviceResponse = new ServiceResponse<GetCarDto>();

            Car dbCar = await _carRepo.getCar(id);
            if (dbCar == null)
            {
                serviceResponse.Data = null;
                serviceResponse.message = "No such car exist";
                serviceResponse.isSuccess = false;
            }
            else
            {
                serviceResponse.Data = _mapper.Map<GetCarDto>(dbCar);
            }

            return serviceResponse;

        }



        public async Task<ServiceResponse<List<GetCarDto>>> getCarsByBrand(string brand)
        {
            ServiceResponse<List<GetCarDto>> serviceResponse = new ServiceResponse<List<GetCarDto>>();

            List<Car> dbCars = await _carRepo.getCarsByBrand(brand);

            if (dbCars == null)
            {

                serviceResponse.Data = null;
                serviceResponse.message = "No such car exist";
                serviceResponse.isSuccess = false;

            }

            serviceResponse.Data = (dbCars.Select(c => _mapper.Map<GetCarDto>(c))).ToList();
            return serviceResponse;

        }



        public async Task<ServiceResponse<List<GetCarDto>>> createCar(CreateCarDto car)
        {

            ServiceResponse<List<GetCarDto>> serviceResponse = new ServiceResponse<List<GetCarDto>>();
            Car addCar = _mapper.Map<Car>(car);
            List<Car> dbCars = await _carRepo.createCar(addCar);

            serviceResponse.Data = (dbCars.Select(c => _mapper.Map<GetCarDto>(c))).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCarDto>> updateCar(UpdateCarDto updateCar)
        {

            ServiceResponse<GetCarDto> serviceResponse = new ServiceResponse<GetCarDto>();

            Car updatedCar = await _carRepo.getCar(updateCar.id);

            if (updatedCar != null)
            {
                try
                {
                    updatedCar.name = updateCar.name;
                    updatedCar.description = updateCar.description;
                    updatedCar.brand = updateCar.brand;
                    updatedCar.price = updateCar.price;
                    updatedCar.rating = updateCar.rating;
                    updatedCar.imageUrl = updateCar.imageUrl;

                    await _carRepo.updateCar(updatedCar);
                    serviceResponse.Data = _mapper.Map<GetCarDto>(updatedCar);

                    return serviceResponse;


                }
                catch (Exception e) {
                    serviceResponse.isSuccess = false;
                    serviceResponse.message = e.Message;
                    return serviceResponse;
                }
            }
            else {
                serviceResponse.isSuccess = false;
                serviceResponse.message = "No Such Car Exist";
                return serviceResponse;
            }

        }

        public async Task<ServiceResponse<List<GetCarDto>>> deleteCar(int id)
        {
            ServiceResponse<List<GetCarDto>> serviceResponse = new ServiceResponse<List<GetCarDto>>();

            Car removedCar = await _carRepo.getCar(id);

            if (removedCar != null)
            {

                try
                {
                    List<Car> dbCars = await _carRepo.deleteCar(removedCar);

                    serviceResponse.Data = (dbCars.Select(c => _mapper.Map<GetCarDto>(c))).ToList();

                    return serviceResponse;

                }
                catch (Exception e)
                {
                    serviceResponse.isSuccess = false;
                    serviceResponse.message = e.Message;
                    return serviceResponse;
                }

            }
            else {

                serviceResponse.isSuccess = false;
                serviceResponse.message = "No Such Car Exist";
                return serviceResponse;
            }
        }

        public ServiceResponse<string> getStatus(){

            ServiceResponse<string> serviceResponse = new ServiceResponse<string>();
            serviceResponse.message = "Up and running";
            return serviceResponse;
        }


    }
}
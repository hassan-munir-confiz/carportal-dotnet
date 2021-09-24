using AutoMapper;
using carportal;
using carportal.Models;
using carportal.Models.Dtos;
using carportal.Repositories.CarRepo;
using carportal.Services.CarService;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;
using System.Linq;
using System.Threading.Tasks;

namespace CarportalTest
{
    public class CarServiceTest
    {

        private IMapper _mapper;

        private Mock<ICarRepo> mock = new Mock<ICarRepo>();

        public CarServiceTest()
        {

            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            }).CreateMapper();


        }

        [Fact]
        public async Task getCarsTest()
        {
            List<Car> cars = TestDataProvider.getCars();

            mock.Setup(c => c.getCars()).ReturnsAsync(cars);

            CarService carService = new CarService(_mapper, mock.Object);

            var result = await carService.getCars();

            Assert.Equal(cars.Count,result.Data.Count);

        }


        [Fact]
        public async Task getCarsByBrandTest() {

            List<Car> cars = TestDataProvider.getCars();

            mock.Setup(c => c.getCarsByBrand(It.IsAny<String>())).ReturnsAsync(cars);


            CarService carService = new CarService(_mapper, mock.Object);

            var result = await carService.getCarsByBrand("Honda");

            var count = result.Data.Where(c => c.brand.Equals("Honda")).Count();

            Assert.Equal(2,count);

        }


        [Fact]
        public async Task getCarTest() {

            Car car = TestDataProvider.GetCar();

            mock.Setup(c => c.getCar(1)).ReturnsAsync(car);

            CarService carService = new CarService(_mapper, mock.Object);

            var result = await carService.getCar(1);

            Assert.Equal(result.Data.name, car.name);



        }


        [Fact]
        public async Task updateCarTest() {

            Car car = TestDataProvider.GetCar();

            UpdateCarDto updateCarDto = TestDataProvider.getUpdateCarDto();

            mock.Setup(c => c.getCar(car.id)).ReturnsAsync(car);

            mock.Setup(c => c.updateCar(It.IsAny<Car>())).ReturnsAsync(car);

            CarService carService = new CarService(_mapper, mock.Object);

            var result = await carService.updateCar(updateCarDto);

            Assert.Equal(result.Data.name, car.name);
        }


        [Fact]
        public async Task addCarTest()
        {

            Car updateCar = TestDataProvider.GetCar();

            List<Car> cars = TestDataProvider.getCars();

            CreateCarDto createCarDto = TestDataProvider.getCreateCarDto();

            mock.Setup(c => c.createCar(It.IsAny<Car>())).ReturnsAsync(cars);

            CarService carService = new CarService(_mapper, mock.Object);

            var result = await carService.createCar(createCarDto);

            Assert.Equal(result.Data[0].name, cars[0].name);

        }

        
    }
}

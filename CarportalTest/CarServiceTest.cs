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
        public async void getCarsTest()
        {
            List<Car> cars = new List<Car>()
            {
                new Car{ id=1, name="BR-V", description="Hello" , price=12,rating=2,imageUrl="url",brand="Honda"},
                new Car{ id=2, name="Mehran", description="Hello" , price=12,rating=2,imageUrl="url",brand="Suzuki"},
                new Car{ id=3, name="Civic", description="Hello" , price=12,rating=2,imageUrl="url",brand="Honda"}
            };

            mock.Setup(c => c.getCars()).ReturnsAsync(cars);

            CarService carService = new CarService(_mapper, mock.Object);

            var result = await carService.getCars();

            Assert.Equal(result.Data.Count, cars.Count);

        }


        [Fact]
        public async void getCarsByBrandTest() {

            List<Car> cars = new List<Car>()
            {
                new Car{ id=1, name="BR-V", description="Hello" , price=12,rating=2,imageUrl="url",brand="Honda"},
                new Car{ id=3, name="Civic", description="Hello" , price=12,rating=2,imageUrl="url",brand="Honda"},
                new Car{ id=3, name="Civic", description="Hello" , price=12,rating=2,imageUrl="url",brand="Suzuki"}
            };

            mock.Setup(c => c.getCarsByBrand(It.IsAny<String>())).ReturnsAsync(cars);


            CarService carService = new CarService(_mapper, mock.Object);

            var result = await carService.getCarsByBrand("Honda");

            var count = result.Data.Where(c => c.brand.Equals("Honda")).Count();

            Assert.Equal(2,count);

        }


        [Fact]
        public async void getCarTest() {


            Car car = new Car { id = 1, name = "BR-V", description = "Hello", price = 12, rating = 2, imageUrl = "url", brand = "Honda" };

            mock.Setup(c => c.getCar(1)).ReturnsAsync(car);

            CarService carService = new CarService(_mapper, mock.Object);

            var result = await carService.getCar(1);

            Assert.Equal(result.Data.name, car.name);



        }


        [Fact]
        public async void updateCarTest() { 
        
            Car car = new Car { id = 1, name = "BR-V", description = "Hello", price = 12, rating = 2, imageUrl = "url", brand = "Honda" }; 

            UpdateCarDto updateCarDto = TestDataProvider.GetUpdateCarDto();

            mock.Setup(c => c.getCar(car.id)).ReturnsAsync(car);

            mock.Setup(c => c.updateCar(It.IsAny<Car>())).ReturnsAsync(car);

            CarService carService = new CarService(_mapper, mock.Object);

            var result = await carService.updateCar(updateCarDto);

            Assert.Equal(result.Data.name, car.name);
        }


        [Fact]
        public async void addCarTest()
        {

            Car updateCar = new Car { id = 1, name = "BR-V", description = "Hello", price = 12, rating = 2, imageUrl = "url", brand = "Honda" };

            List<Car> cars = new List<Car>()
            {
                new Car{ id=1, name="BR-V", description="Hello" , price=12,rating=2,imageUrl="url",brand="Honda"},
                new Car{ id=2, name="Mehran", description="Hello" , price=12,rating=2,imageUrl="url",brand="Suzuki"},
                new Car{ id=3, name="Civic", description="Hello" , price=12,rating=2,imageUrl="url",brand="Honda"}
            };

            CreateCarDto createCarDto = TestDataProvider.getCreateCarDto();

            mock.Setup(c => c.createCar(It.IsAny<Car>())).ReturnsAsync(cars);

            CarService carService = new CarService(_mapper, mock.Object);

            var result = await carService.createCar(createCarDto);

            Assert.Equal(result.Data[0].name, cars[0].name);

        }

        
    }
}

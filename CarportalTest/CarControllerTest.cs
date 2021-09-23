using carportal.Controllers;
using carportal.Models;
using carportal.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CarportalTest
{
    public class CarControllerTest
    {

        private Mock<ICarService> mock = new Mock<ICarService>();

        [Fact]
        public async Task getCarsControllerTest() {

            List<GetCarDto> getCarDtos = new List<GetCarDto>();
            getCarDtos.Add(TestDataProvider.getCarDto());
            getCarDtos.Add(TestDataProvider.getCarDto());

            ServiceResponse<List<GetCarDto>> cars = new ServiceResponse<List<GetCarDto>>();

            cars.Data = getCarDtos;
            cars.isSuccess = true;


            mock.Setup(c => c.getCars()).ReturnsAsync(cars);

            CarController carController = new CarController(mock.Object);

            var result = await carController.getCars();


            var resultObject = ((ServiceResponse<List<GetCarDto>>)(result as OkObjectResult).Value);

            Assert.True(resultObject.isSuccess);
            Assert.NotNull(resultObject.Data);

        }

        [Fact]
        public async Task getCarTest() {

            GetCarDto getCarDto = TestDataProvider.getCarDto();
            ServiceResponse<GetCarDto> serviceResponse = new ServiceResponse<GetCarDto>();

            serviceResponse.Data = getCarDto;
            serviceResponse.isSuccess = true;

            mock.Setup(c => c.getCar(It.IsAny<int>())).ReturnsAsync(serviceResponse);

            CarController carController = new CarController(mock.Object);

            var result = await carController.getCar(1);


            var resultObject = ((ServiceResponse<GetCarDto>)(result as OkObjectResult).Value);

            Assert.True(resultObject.isSuccess);
            Assert.NotNull(resultObject.Data);
        }

        [Fact]
        public async Task getCarNoCarTest()
        {

            
            ServiceResponse<GetCarDto> serviceResponse = new ServiceResponse<GetCarDto>();
            serviceResponse.Data = null;
            serviceResponse.message = "No such car exist";
            serviceResponse.isSuccess = false;

            mock.Setup(c => c.getCar(It.IsAny<int>())).ReturnsAsync(serviceResponse);

            CarController carController = new CarController(mock.Object);

            var result = await carController.getCar(1);


            var resultObject = ((ServiceResponse<GetCarDto>)(result as NotFoundObjectResult).Value);

            Assert.False(resultObject.isSuccess);
            Assert.Null(resultObject.Data);
            Assert.Equal("No such car exist", resultObject.message);
        }

        [Fact]
        public async Task getCarsByBrandTest()
        {
            List<GetCarDto> getCarDtos = new List<GetCarDto>();
            getCarDtos.Add(TestDataProvider.getCarDto());
            getCarDtos.Add(TestDataProvider.getCarDto());

            ServiceResponse<List<GetCarDto>> serviceResponse = new ServiceResponse<List<GetCarDto>>();

            serviceResponse.Data = getCarDtos;
            serviceResponse.isSuccess = true;

            mock.Setup(c => c.getCarsByBrand(It.IsAny<string>())).ReturnsAsync(serviceResponse);

            CarController carController = new CarController(mock.Object);

            var result = await carController.getCarByBrand("Honda");


            var resultObject = ((ServiceResponse<List<GetCarDto>>)(result as OkObjectResult).Value);

            Assert.True(resultObject.isSuccess);
            Assert.NotNull(resultObject.Data);
            Assert.Equal("Honda", resultObject.Data[0].brand);
        }

        [Fact]
        public async Task getCarsByBrandNoCarTest()
        {
            
            ServiceResponse<List<GetCarDto>> serviceResponse = new ServiceResponse<List<GetCarDto>>();

            serviceResponse.message = "No such car exist";
            serviceResponse.isSuccess = false;

            mock.Setup(c => c.getCarsByBrand(It.IsAny<string>())).ReturnsAsync(serviceResponse);

            CarController carController = new CarController(mock.Object);

            var result = await carController.getCarByBrand("Honda");


            var resultObject = ((ServiceResponse<List<GetCarDto>>)(result as NotFoundObjectResult).Value);

            Assert.False(resultObject.isSuccess);
            Assert.Null(resultObject.Data);
        }


    }
}

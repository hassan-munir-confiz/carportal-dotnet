using carportal.Models;
using carportal.Models.Dtos;
using System.Collections.Generic;

namespace CarportalTest
{
    class TestDataProvider
    {

        public static readonly string controllerName = "/car";
        public static readonly string getCarsEndPoint = "/getCars";
        public static readonly string getCarEndPoint = "/getCar/";
        public static readonly string getCarsByBrandEndPoint = "/getCarsByBrand/";
        public static readonly string createCarEndPoint = "/createCar";
        public static readonly string updateCarEndPoint = "/updateCar";
        public static readonly string deleteCarEndPoint = "/deleteCar/";

        public static CreateCarDto getCreateCarDto()
        {

            CreateCarDto createCarDto = new CreateCarDto()
            {
                name = "BR-V1",
                description = "Test Desc"
            };

            return createCarDto;
        }

        public static GetCarDto getCarDto()
        {

            GetCarDto getCarDto = new GetCarDto()
            {
                name = "BR-V",
                description = "Test Desc",
                brand = "Honda"
            };

            return getCarDto;
        }


        public static UpdateCarDto getUpdateCarDto()
        {

            UpdateCarDto updateCarDto = new UpdateCarDto()
            {
                name = "BR-V12"
            };

            return updateCarDto;
        }

        public static UpdateCarDto getNoUpdateCarDto()
        {

            UpdateCarDto updateCarDto = new UpdateCarDto()
            {
                name = "BR-V12",
                id = 123
            };

            return updateCarDto;
        }


        public static List<Car> getCars() {

            return new List<Car>()
            {
                new Car{ id=1, name="BR-V", description="Hello" , price=12,rating=2,imageUrl="url",brand="Honda"},
                new Car{ id=2, name="Mehran", description="Hello" , price=12,rating=2,imageUrl="url",brand="Suzuki"},
                new Car{ id=3, name="Civic", description="Hello" , price=12,rating=2,imageUrl="url",brand="Honda"}
            };
        }

        public static Car GetCar() { 
        
            return new Car { id = 1, name = "BR-V", description = "Hello", price = 12, rating = 2, imageUrl = "url", brand = "Honda" }; 

        }
    }
}

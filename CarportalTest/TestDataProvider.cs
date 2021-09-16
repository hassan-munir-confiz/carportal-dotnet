using carportal.Models.Dtos;

namespace CarportalTest
{
    class TestDataProvider
    {

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


        public static UpdateCarDto GetUpdateCarDto()
        {

            UpdateCarDto createCarDto = new UpdateCarDto()
            {
                name = "BR-V"
            };

            return createCarDto;


        }
    }
}

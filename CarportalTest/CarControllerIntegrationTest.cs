using System.Collections.Generic;
using System.Threading.Tasks;
using carportal.Models;
using Xunit;
using carportal.Models.Dtos;
using Newtonsoft.Json;
using System.Net;
using System.Linq;
using System.Net.Http.Json;

namespace CarportalTest
{
    public class CarControllerIntegrationTest : IntegrationBase
    {

        [Fact]
        public async Task getAllCarsTest(){
       
            var serverResponse = await httpClient.GetAsync(requestUri: TestDataProvider.controllerName + TestDataProvider.getCarsEndPoint);
            var responseObject = await serverResponse.Content.ReadAsStringAsync();

            var carsResponse = JsonConvert.DeserializeObject<ServiceResponse<List<GetCarDto>>>(responseObject);

            Assert.Equal(HttpStatusCode.OK, serverResponse.StatusCode);
            Assert.NotEmpty(carsResponse.Data);
        }

        [Fact]
        public async Task getCarTest() {

            var serverResponse = await httpClient.GetAsync(requestUri: TestDataProvider.controllerName + TestDataProvider.getCarEndPoint+"1");
            var responseObject = await serverResponse.Content.ReadAsStringAsync();

            var carResponse = JsonConvert.DeserializeObject<ServiceResponse<GetCarDto>>(responseObject);

            Assert.Equal(HttpStatusCode.OK, serverResponse.StatusCode);
            Assert.NotNull(carResponse.Data);
            Assert.True(carResponse.isSuccess);
        }

        [Fact]
        public async Task getNoCarTest()
        {

            var serverResponse = await httpClient.GetAsync(requestUri: TestDataProvider.controllerName + TestDataProvider.getCarEndPoint + "11");
            var responseObject = await serverResponse.Content.ReadAsStringAsync();

            var carResponse = JsonConvert.DeserializeObject<ServiceResponse<GetCarDto>>(responseObject);

            Assert.Equal(HttpStatusCode.NotFound, serverResponse.StatusCode);
            Assert.Null(carResponse.Data);
            Assert.False(carResponse.isSuccess);
            Assert.Equal("No such car exist", carResponse.message);
        }

        [Fact]
        public async Task getCarsByBrandTest()
        {

            var serverResponse = await httpClient.GetAsync(requestUri: TestDataProvider.controllerName + TestDataProvider.getCarsByBrandEndPoint + "Honda");
            var responseObject = await serverResponse.Content.ReadAsStringAsync();

            var carResponse = JsonConvert.DeserializeObject<ServiceResponse<List<GetCarDto>>>(responseObject);

            Assert.Equal(HttpStatusCode.OK, serverResponse.StatusCode);
            Assert.Equal(2, carResponse.Data.Where(car => car.brand.Equals("Honda")).Count());
            Assert.True(carResponse.isSuccess);
        }

        [Fact]
        public async Task getNoCarsByBrandTest()
        {

            var serverResponse = await httpClient.GetAsync(requestUri: TestDataProvider.controllerName + TestDataProvider.getCarsByBrandEndPoint + "TestBrand");
            var responseObject = await serverResponse.Content.ReadAsStringAsync();

            var carResponse = JsonConvert.DeserializeObject<ServiceResponse<List<GetCarDto>>>(responseObject);

            Assert.Equal(HttpStatusCode.OK, serverResponse.StatusCode);
            Assert.Empty(carResponse.Data);
            Assert.True(carResponse.isSuccess);
        }

        [Fact]
        public async Task createCarTest() {

            var serverResponse = await httpClient.GetAsync(requestUri: TestDataProvider.controllerName + TestDataProvider.getCarsEndPoint);
            var responseObject = await serverResponse.Content.ReadAsStringAsync();

            var carsResponse = JsonConvert.DeserializeObject<ServiceResponse<List<GetCarDto>>>(responseObject);


            var createCarServerResponse = await httpClient.PostAsJsonAsync(requestUri: TestDataProvider.controllerName + TestDataProvider.createCarEndPoint,
              
                                            TestDataProvider.getCreateCarDto());

            var createCarResponseObject = await createCarServerResponse.Content.ReadAsStringAsync();

            var createCarResponse = JsonConvert.DeserializeObject<ServiceResponse<List<GetCarDto>>>(createCarResponseObject);

            Assert.True(createCarResponse.isSuccess);
            Assert.Equal(carsResponse.Data.Count + 1, createCarResponse.Data.Count);

        }

        [Fact]
        public async Task updateCarTest() {

            var serverResponse = await httpClient.PutAsJsonAsync(requestUri: TestDataProvider.controllerName + TestDataProvider.updateCarEndPoint,
                                TestDataProvider.getUpdateCarDto());
            var responseObject = await serverResponse.Content.ReadAsStringAsync();
            var carResponse = JsonConvert.DeserializeObject<ServiceResponse<GetCarDto>>(responseObject);

            Assert.True(carResponse.isSuccess);
            Assert.Equal("BR-V12", carResponse.Data.name);

        }

        [Fact]
        public async Task noUpdateCarTest()
        {

            var serverResponse = await httpClient.PutAsJsonAsync(requestUri: TestDataProvider.controllerName + TestDataProvider.updateCarEndPoint,
                                TestDataProvider.getNoUpdateCarDto());
            var responseObject = await serverResponse.Content.ReadAsStringAsync();
            var carResponse = JsonConvert.DeserializeObject<ServiceResponse<GetCarDto>>(responseObject);

            Assert.False(carResponse.isSuccess);
            Assert.Null(carResponse.Data);
            Assert.Equal("No Such Car Exist", carResponse.message);

        }

        [Fact]
        public async Task deleteCarTest()
        {

            var serverResponse = await httpClient.GetAsync(requestUri: TestDataProvider.controllerName + TestDataProvider.getCarsEndPoint);
            var responseObject = await serverResponse.Content.ReadAsStringAsync();

            var carsResponse = JsonConvert.DeserializeObject<ServiceResponse<List<GetCarDto>>>(responseObject);


            var deleteCarServerResponse = await httpClient.DeleteAsync(requestUri: TestDataProvider.controllerName + TestDataProvider.deleteCarEndPoint+"1");

            var deleteCarResponseObject = await deleteCarServerResponse.Content.ReadAsStringAsync();

            var deleteCarResponse = JsonConvert.DeserializeObject<ServiceResponse<List<GetCarDto>>>(deleteCarResponseObject);

            Assert.True(deleteCarResponse.isSuccess);
            Assert.Equal(carsResponse.Data.Count - 1, deleteCarResponse.Data.Count);

        }

        [Fact]
        public async Task deleteNoCarTest()
        {

            var deleteCarServerResponse = await httpClient.DeleteAsync(requestUri: TestDataProvider.controllerName + TestDataProvider.deleteCarEndPoint + "1211");

            var deleteCarResponseObject = await deleteCarServerResponse.Content.ReadAsStringAsync();

            var deleteCarResponse = JsonConvert.DeserializeObject<ServiceResponse<List<GetCarDto>>>(deleteCarResponseObject);

            Assert.False(deleteCarResponse.isSuccess);
            Assert.Null(deleteCarResponse.Data);
            Assert.Equal("No Such Car Exist", deleteCarResponse.message);

        }
    }
}

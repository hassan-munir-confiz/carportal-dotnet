using Microsoft.AspNetCore.Mvc;
using carportal.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using carportal.Models.Dtos;

namespace carportal.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarController : ControllerBase
    {
        private readonly ICarService _carService;

        public CarController(ICarService carService)
        {
            _carService = carService;

        }

        [HttpGet("getStatus")]
        public  IActionResult getStatus() {

            return Ok(_carService.getStatus());
        }

        [HttpGet("getCars")]
        public async Task<IActionResult> getCars()
        {
            return Ok(await _carService.getCars());

        }

        [HttpGet("getCar/{id}")]
        public async Task<IActionResult> getCar(int id)
        {
            ServiceResponse<GetCarDto> serviceResponse = await _carService.getCar(id);

            if (serviceResponse.Data == null) {

               return NotFound(serviceResponse);
            }

            return Ok(serviceResponse);    
        }

        [HttpGet("getCarsByBrand/{brand}")]
        public async Task<IActionResult> getCarByBrand(string brand)
        {

            ServiceResponse<List<GetCarDto>> serviceResponse = await _carService.getCarsByBrand(brand);

            if (serviceResponse.Data == null)
            {

                return NotFound(serviceResponse);
            }

            return Ok(serviceResponse);   
        }


        [HttpPost("createCar")]
        public async Task<IActionResult> createCar(CreateCarDto car)
        {

            return Ok(await _carService.createCar(car));
        }

        [HttpPut("updateCar")]
        public async Task<IActionResult> updateCar(UpdateCarDto car)
        {

            ServiceResponse<GetCarDto> serviceResponse = await _carService.updateCar(car);
            
            if(serviceResponse.Data ==null){
              return  NotFound(serviceResponse);
            }
            return Ok(serviceResponse);            
        }

        [HttpDelete("deleteCar/{id}")]
        public async Task<IActionResult> deleteCar(int id)
        {

            ServiceResponse<List<GetCarDto>> serviceResponse = await _carService.deleteCar(id);
            
            if(serviceResponse.Data ==null){
              return  NotFound(serviceResponse);
            }
            return Ok(serviceResponse);            
        }
    }
}
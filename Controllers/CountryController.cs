using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorldAPI.Data;
using WorldAPI.DTO.Country;
using WorldAPI.Models;
using WorldAPI.Repository;

namespace WorldAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryRepository _countryRepository;

        private readonly IMapper _mapper;

        private readonly ILogger<CountryController> _logger;

        public CountryController(ICountryRepository countryRepository,IMapper mapper, ILogger<CountryController> logger)
        {
           _countryRepository = countryRepository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<CountryDto>>>GetAll()
        {
            var country = await _countryRepository.GetAll();

            var countryDto = _mapper.Map<List<CountryDto>>(country);
            
            if(country == null)
            {
                return NoContent();
            }
            return Ok(country);
        }


        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CountryDto>> GetById(int id)
        {
            var country = await _countryRepository.Get(id);

            var countrydto = _mapper.Map<CountryDto>(country);

            if(country == null)
            {
                _logger.LogError($"Error while try to get record id:{id}");
                return NoContent();
            }

            return Ok(countrydto);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<CreateCountryDto>> Create([FromBody]CreateCountryDto countrydto)
        {
            var result = _countryRepository.IsRecordExsits(x=>x.Name == countrydto.Name);

            if(result)
            {
                return Conflict("Country Already Exists in the Database");
            }

            //Country country = new Country();
            //country.Name = countrydto.Name;
            //country.ShortName = countrydto.ShortName;
            //country.CountryCode = countrydto.CountryCode;

            var country = _mapper.Map<Country>(countrydto);

            await _countryRepository.Create(country);
            return CreatedAtAction("GetById",new { id = country.Id },country);
        }




        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
    
        public async Task<ActionResult<UpdateCountryDto>> Update(int id,[FromBody]UpdateCountryDto countrydto)
        {
            if(countrydto == null || id != countrydto.Id)
            {
                return BadRequest(); 
            }

            var country = _mapper.Map<Country>(countrydto);



            await _countryRepository.Update(country);
            return Ok();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
       
        public async Task<ActionResult<Country>>DeleteById(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var country = await _countryRepository.Get(id);

            if (country == null)
            {
                return NotFound();  
            }

            await _countryRepository.Delete(country);
            return Ok();
        }
    }
}

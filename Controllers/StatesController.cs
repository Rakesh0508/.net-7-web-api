using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorldAPI.DTO.Country;
using WorldAPI.DTO.States;
using WorldAPI.Models;
using WorldAPI.Repository;

namespace WorldAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatesController : ControllerBase
    {
        private readonly IStatesRepository _statesRepository;

        private readonly IMapper _mapper;

        public StatesController(IStatesRepository statesRepository, IMapper mapper)
        {
            _statesRepository = statesRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<StatesDto>>> GetAll()
        {
            var states = await _statesRepository.GetAll();

            var statesDto = _mapper.Map<List<StatesDto>>(states);

            if (states == null)
            {
                return NoContent();
            }
            return Ok(states);
        }


        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<StatesDto>> GetById(int id)
        {
            var states = await _statesRepository.Get(id);

            var statesDto = _mapper.Map<StatesDto>(states);

            if (states == null)
            {
                return NoContent();
            }

            return Ok(statesDto);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<CreateStatesDto>> Create([FromBody] CreateStatesDto statesdto)
        {
            var result = _statesRepository.IsRecordExsits(x => x.Name == statesdto.Name);

            if (result)
            {
                return Conflict("Country Already Exists in the Database");
            }

     

            var states = _mapper.Map<States>(statesdto);

            await _statesRepository.Create(states);
            return CreatedAtAction("GetById", new { id = states.Id }, states);
        }




        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UpdateStatesDto>> Update(int id, [FromBody] UpdateStatesDto statesdto)
        {
            if (statesdto == null || id != statesdto.Id)
            {
                return BadRequest();
            }

            var states = _mapper.Map<States>(statesdto);



            await _statesRepository.Update(states);
            return Ok();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<States>> DeleteById(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var states = await _statesRepository.Get(id);

            if (states == null)
            {
                return NotFound();
            }

            await _statesRepository.Delete(states);
            return NoContent();
        }
    }
}

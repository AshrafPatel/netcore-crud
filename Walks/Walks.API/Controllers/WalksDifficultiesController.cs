using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Walks.API.Models.Domain;
using Walks.API.Models.DTO;
using Walks.API.Repository;

namespace Walks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalksDifficultiesController : Controller
    {
        private readonly IWalkDifficultyRepository _walkDifficultyRepository;
        private readonly IMapper _mapper;

        public WalksDifficultiesController(IWalkDifficultyRepository walkDifficultyRepository, IMapper mapper)
        {
            _walkDifficultyRepository = walkDifficultyRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ActionName("GetAllWalkDifficulties")]
        public async Task<IActionResult> GetAllWalkDifficulties()
        {
            var walkDifficulties = await _walkDifficultyRepository.GetAllAsync();
            if (walkDifficulties == null) { return null; }
            var walkDifficultiesDto = _mapper.Map<List<WalkDifficultyDto>>(walkDifficulties);
            return Ok(walkDifficultiesDto);
        }

        [HttpGet]
        [ActionName("GetWalkDifficulty")]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetWalkDifficulty([FromRoute] Guid id)
        {
            var walkDifficultyDom = await _walkDifficultyRepository.GetAsync(id);
            if (walkDifficultyDom == null) { return NotFound(); }
            var walkDifficultyDto = _mapper.Map<WalkDifficultyDto>(walkDifficultyDom);
            return Ok(walkDifficultyDto);
        }

        [HttpPost]
        [ActionName("CreateWalkDifficulty")]
        public async Task<IActionResult> CreateWalkDifficulty([FromBody] AddWalkDifficultyDto addWalkDifficultyDto)
        {
            var walkDifficultyDom = new WalkDifficulty()
            {
                Code = addWalkDifficultyDto.Code
            };

            walkDifficultyDom = await _walkDifficultyRepository.AddAsync(walkDifficultyDom);

            var walkDifficultyDto = _mapper.Map<WalkDifficultyDto>(walkDifficultyDom);
            return CreatedAtAction(nameof(GetWalkDifficulty), new { id = walkDifficultyDto.Id }, walkDifficultyDto);
        }

        [HttpPut]
        [ActionName("UpdateWalkDifficulty")]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWalkDifficulty([FromRoute] Guid id, [FromBody] UpdateWalkDifficultyDto updateWalkDifficultyDto)
        {
            var walkDifficultyDom = new WalkDifficulty()
            {
                Code = updateWalkDifficultyDto.Code
            };
            walkDifficultyDom = await _walkDifficultyRepository.UpdateAsync(id, walkDifficultyDom);

            if (walkDifficultyDom == null) { NotFound(); }

            var walkDifficultyDto = _mapper.Map<WalkDifficultyDto>(walkDifficultyDom);
            return Ok(walkDifficultyDto);
        }

        [HttpDelete]
        [ActionName("DeleteWalkDifficulty")]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteWalkDifficulty([FromRoute] Guid id)
        {
            var walkDifficultyDom = await _walkDifficultyRepository.DeleteAsync(id);
            if (walkDifficultyDom == null) { NotFound(); }
            var walkDifficultyDto = _mapper.Map<WalkDifficultyDto>(walkDifficultyDom);
            return Ok(walkDifficultyDto);
        }
    }
}

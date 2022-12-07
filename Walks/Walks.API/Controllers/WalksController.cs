using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Walks.API.Models.Domain;
using Walks.API.Models.DTO;
using Walks.API.Repository;

namespace Walks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalksController : Controller
    {
        private readonly IWalkRepository _walkRepository;
        private readonly IMapper _mapper;

        public WalksController(IWalkRepository walkRepository, IMapper mapper)
        {
            _walkRepository = walkRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ActionName("GetAllWalksAsync")]
        public async Task<IActionResult> GetAllWalksAsync()
        {
            var walks = await _walkRepository.GetAllAsync();
            var walksDto = _mapper.Map<List<Models.DTO.WalkDto>>(walks);
            return Ok(walksDto);
        }

        [HttpGet]
        [ActionName("GetWalkAsync")]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetWalkAsync(Guid id)
        {
            var walk = await _walkRepository.GetByIdAsync(id);
            if (walk == null) { return NotFound(); }

            var walkDto = _mapper.Map<WalkDto>(walk);
            return Ok(walkDto);
        }

        [HttpPost]
        [ActionName("AddWalkAsync")]
        public async Task<IActionResult> AddWalkAsync([FromBody] AddWalkDto addWalkDto) 
        {
            var walk = new Walk()
            {
                Name = addWalkDto.Name,
                Length = addWalkDto.Length,
                RegionId = addWalkDto.RegionId,
                WalkDifficultyId = addWalkDto.WalkDifficultyId
            };
            walk = await _walkRepository.AddAsync(walk);

            var walkDto = _mapper.Map<WalkDto>(walk);
            return CreatedAtAction(nameof(GetWalkAsync), new { id = walkDto.Id }, walkDto);
        }

        [HttpPut]
        [Route("{id:guid}")]
        [ActionName("UpdateWalkAsync")]
        public async Task<IActionResult> UpdateWalkAsync([FromRoute] Guid id, [FromBody] UpdateWalkDto updateWalkDto)
        {
            var updatedWalkDomain = new Walk()
            {
                Name = updateWalkDto.Name,
                Length = updateWalkDto.Length,
                RegionId = updateWalkDto.RegionId,
                WalkDifficultyId = updateWalkDto.WalkDifficultyId
            };

            updatedWalkDomain = await _walkRepository.UpdateAsync(id, updatedWalkDomain);

            if (updatedWalkDomain == null) { return NotFound(); }

            var updatedWalkDto = _mapper.Map<WalkDto>(updatedWalkDomain);
            return Ok(updatedWalkDto);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        [ActionName("DeleteWalkAsync")]
        public async Task<IActionResult> DeleteWalkAsync([FromRoute] Guid id)
        {
            var deletedWalkDom = await _walkRepository.DeleteAsync(id);
            if (deletedWalkDom == null) { return NotFound(); }
            var deleteWalkDto = _mapper.Map<WalkDto>(deletedWalkDom);
            return Ok(deleteWalkDto);   
        }
    }
}

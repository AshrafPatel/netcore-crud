using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Walks.API.Models.Domain;
using Walks.API.Models.DTO;
using Walks.API.Repository;

namespace Walks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegionsController : Controller
    {
        private readonly IRegionRepository _regionRepository;
        private readonly IMapper _mapper;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            _regionRepository = regionRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ActionName("GetAllRegions")]
        public async Task<IActionResult> GetAllRegions()
        {
            var region = await _regionRepository.GetAllAsync();

            //var regionsDto = new List<Models.DTO.Region>();
            //region.ToList().ForEach(region =>
            //{
            //    var regionDto = new Models.DTO.Region()
            //    {
            //        Id = region.Id,
            //        Code= region.Code,
            //        Name= region.Name,
            //        Area= region.Area,
            //        Lat = region.Lat,
            //        Long= region.Long,
            //        Population= region.Population
            //    };

            //    regionsDto.Add(regionDto);
            //});

            var regionsDto = _mapper.Map<List<Models.DTO.RegionDto>>(region);

            return Ok(regionsDto);
        }

        [HttpGet]
        [ActionName("GetRegionAsync")]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetRegionAsync(Guid id)
        {
            var region = await _regionRepository.GetAsync(id);
            if (region == null) { return NotFound();  }
            var regionDto = _mapper.Map<Models.DTO.RegionDto>(region);
            return Ok(regionDto); 
        }

        [HttpPost]
        [ActionName("AddRegionAsync")]
        public async Task<IActionResult> AddRegionAsync(Models.DTO.AddRegionDto addRegionDto)
        {
            var region = new Models.Domain.Region
            {
                Code = addRegionDto.Code,
                Name = addRegionDto.Name,
                Area = addRegionDto.Area,
                Lat = addRegionDto.Lat,
                Long = addRegionDto.Long,
                Population = addRegionDto.Population
            };

            region = await _regionRepository.AddAsync(region);

            var regionDto = _mapper.Map<Models.DTO.RegionDto>(region);

            return CreatedAtAction(nameof(GetRegionAsync), new {id = regionDto.Id}, regionDto);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        [ActionName("DeleteRegionAsync")]
        public async Task<IActionResult> DeleteRegionAsync(Guid id)
        {
            var region = await _regionRepository.DeleteAsync(id);
            if (region == null) { return NotFound(); }
            var regionDto = _mapper.Map<Models.DTO.RegionDto>(region);
            return Ok(regionDto);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateRegionAsync([FromRoute] Guid id, [FromBody] UpdateRegionDto updateRegionDto)
        {
            var region = new Models.Domain.Region
            {
                Code = updateRegionDto.Code,
                Name = updateRegionDto.Name,
                Area = updateRegionDto.Area,
                Lat = updateRegionDto.Lat,
                Long = updateRegionDto.Long,
                Population = updateRegionDto.Population
            };

            region = await _regionRepository.UpdateAsync(id, region);
            if (region == null) { return NotFound();}
            var updatedRegionDto = _mapper.Map<Models.DTO.RegionDto>(region);

            return Ok(updatedRegionDto);
        }
    }
}

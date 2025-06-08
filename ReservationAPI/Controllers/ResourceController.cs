using AutoMapper;
using DAL.Models;
using DAL.Repos.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReservationAPI.DTOs.Read;
using ReservationAPI.DTOs.Write;

namespace ReservationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourceController : ControllerBase
    {
        private readonly IResourceRepository resourceRepository;
        private readonly IMapper _mapper;

        public ResourceController(IResourceRepository resourceRepository, IMapper mapper)
        {
            this.resourceRepository = resourceRepository;
            _mapper = mapper;
        }

        #region Web methods

        [HttpGet]
        public async Task<IActionResult> GetAllResourcesAsync()
        {
            var resources = await resourceRepository.GetAllAsync();
            var resourcesDtos = _mapper.Map<IEnumerable<ResourceDto>>(resources);
            return Ok(resourcesDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetResourceByIdAsync(int id)
        {
            var resource = await resourceRepository.GetByIdAsync(id);
            if (resource == null)
                return NotFound();
            return Ok(resource);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchResourcesAsync([FromQuery] string? name)
        {
            var resources = await resourceRepository.FindAsync(r => string.IsNullOrEmpty(name) || r.Name.Contains(name, StringComparison.OrdinalIgnoreCase));

            if (!resources.Any())
                return NotFound("No resources found matching the criteria.");

            return Ok(resources);
        }

        [HttpPost]
        public async Task<IActionResult> AddResourceAsync([FromBody] ResourceCreateUpdateDto resourceDto)
        {
            var resource = _mapper.Map<Resource>(resourceDto);
            await resourceRepository.AddAsync(resource);
            return CreatedAtAction(nameof(GetResourceByIdAsync).Replace("Async",""), new { id = resource.Id }, resourceDto);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteResourceAsync([FromBody] ResourceDto resource)
        {
            var entity = await resourceRepository.GetByIdAsync(resource.Id);
            if (entity == null)
            {
                return NotFound();
            }
            else
            {
                await resourceRepository.DeleteAsync(entity);
                return NoContent();
            }

        }

        [HttpPut]
        public async Task<IActionResult> UpdateResourceAsync([FromBody] ResourceCreateUpdateDto resourceDto)
        {
            var entity = await resourceRepository.GetByIdAsync(resourceDto.Id);
            if (entity == null)
            {
                return NotFound();
            }
            else
            {
                _mapper.Map(resourceDto, entity);

                await resourceRepository.UpdateAsync(entity);
                return Ok();
            }
        }

        #endregion

    }
}

using AutoMapper;
using DAL.Models;
using DAL.Repos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReservationAPI.DTOs;

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
        public IActionResult GetAllResources()
        {
            var resources = resourceRepository.GetAllAsync();
            var resourcesDtos = _mapper.Map<IEnumerable<ResourceDto>>(resources);
            return Ok(resourcesDtos);
        }

        [HttpGet("{id}")]
        public IActionResult GetResourceById(int id)
        {
            var resource = resourceRepository.GetByIdAsync(id);
            if (resource == null)
                return NotFound();
            return Ok(resource);
        }

        [HttpGet("search")]
        public IActionResult SearchResources([FromQuery] string? name)
        {
            var resources = resourceRepository.FindAsync(r => string.IsNullOrEmpty(name) || r.Name.Contains(name, StringComparison.OrdinalIgnoreCase));

            if (!resources.Any())
                return NotFound("No resources found matching the criteria.");

            return Ok(resources);
        }

        [HttpPost]
        public IActionResult AddResource([FromBody] Resource resource)
        {
            resourceRepository.AddAsync(resource);
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteResource([FromBody] Resource resource)
        {
            var entity = resourceRepository.GetByIdAsync(resource.Id);
            if (entity == null)
            {
                return NotFound();
            }
            else
            {
                resourceRepository.DeleteAsync(entity);
                return NoContent();
            }

        }

        [HttpPut]
        public IActionResult UpdateResource([FromBody] Resource resource)
        {
            var entity = resourceRepository.GetByIdAsync(resource.Id);
            if (entity == null)
            {
                return NotFound();
            }
            else
            {
                entity.Name = resource.Name;
                entity.Description = resource.Description;

                resourceRepository.UpdateAsync(entity);
                return Ok();
            }
        }

        #endregion

    }
}

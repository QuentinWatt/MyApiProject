using Microsoft.AspNetCore.Mvc;

namespace MyApiProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PropertiesController : ControllerBase
    {
        // Fake in-memory list (for demo purposes)
        private static readonly List<Property> _properties = new()
        {
            new Property { Id = 1, Name = "Beach Villa", Location = "Cape Town", Price = 25000 },
            new Property { Id = 2, Name = "Mountain Cabin", Location = "Drakensberg", Price = 18000 },
            new Property { Id = 3, Name = "Studio Apartment", Location = "Johannesburg", Price = 6000 }
        };

        [HttpGet]
        public ActionResult<IEnumerable<Property>> GetAll()
        {
            return Ok(_properties);
        }

        [HttpGet("{id}")]
        public ActionResult<Property> GetById(int id)
        {
            var property = _properties.FirstOrDefault(p => p.Id == id);
            if (property == null) return NotFound();
            return Ok(property);
        }

        [HttpPost]
        public ActionResult<Property> Create(Property property)
        {
            property.Id = _properties.Max(p => p.Id) + 1;
            _properties.Add(property);
            return CreatedAtAction(nameof(GetById), new { id = property.Id }, property);
        }
    }
}

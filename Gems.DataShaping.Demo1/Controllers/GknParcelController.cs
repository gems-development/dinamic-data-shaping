using AutoMapper;
using Gems.DataShaping.Demo1.Dtos;
using Gems.DataShaping.Demo1.Entities;
using Gems.DataShaping.Demo1.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Gems.DataShaping.Demo1.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class GknParcelsController : ControllerBase
	{

		private readonly IMapper _mapper;

		public GknParcelsController(IMapper mapper)
		{
			_mapper = mapper;
		}
		[HttpGet("{id}")]
		public IActionResult Get(long id, [FromQuery] string fields)
		{
			var repo = new GknParcelRepository();

			var parcel = repo.Get(id);

			return Ok(_mapper.Map<GknParcelDto>(parcel).ShapeTo(fields));
		}
	}
}

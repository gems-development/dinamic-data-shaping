using System.Linq;
using Gems.DataShaping.Demo2.Entities;
using Gems.DataShaping.Demo2.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Gems.DataShaping.Demo2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GknParcelsController : ControllerBase
    {
	    [HttpGet]
	    public IActionResult Get([FromQuery] string fields)
	    {
		    var splitedFields = new string[0];
		    if (!string.IsNullOrWhiteSpace(fields))
			    splitedFields = fields.Split(",").Select(x => x.Trim()).ToArray();

		    var repo = new GknParcelRepository();

		    var parcels = repo.GetAll().AsQueryable();

			return Ok(parcels.ShapeTo(splitedFields));

		}

    }
}

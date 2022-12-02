using BlazorBoard_Api.DataAccess;
using LinqToDB;
using Microsoft.AspNetCore.Mvc;

namespace BlazorBoard_Api.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class SectionController : Controller
	{
		private readonly BlazorBoardDB _db;

		public SectionController(BlazorBoardDB db)
		{
			_db = db;
		}

		[HttpGet]
		[Route("Get")]
		public IActionResult Get()
		{
			var sections = _db.Sections;

			if (sections.Count() > 0)
				return Ok(sections);

			return NotFound();
		}
		
		[HttpGet]
		[Route("Get/{id}")]
		public IActionResult Get(int id)
		{
			var section = _db.Sections.FirstOrDefault(x => x.Id == id);

			if (section is not null)
				return Ok(section);

			return NotFound();
		}

        [HttpGet]
        [Route("GetByStatus/{status}")]
        public IActionResult Get(string status)
        {
			var section = _db.Sections.FindBySectionStatus(status);
			if(section is null)
				return NotFound();

            return Ok(section);
        }

        [HttpPost]
		[Route("Delete")]
		public IActionResult Delete(Section section)
		{
			try
			{
				_db.Delete(section);

			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
			return Ok("Success");
		}

		[HttpPost]
		[Route("Create")]
		public IActionResult Create(Section section)
		{
			try
			{
				_db.Insert(section);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}

			return Ok("Success");
		}

		[HttpPost]
		[Route("Update")]
		public IActionResult Update(Section section)
		{
			try
			{
				_db.Update(section);

			}
			catch (Exception ex)
			{
				return BadRequest(ex);
			}

			return Ok("Success");
		}
	}
}

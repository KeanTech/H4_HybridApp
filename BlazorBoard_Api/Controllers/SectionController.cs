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
			var sections = _db.Sections.Where(x => x.Id == id);

			if (sections.Count() > 0)
				return Ok(sections);

			return NotFound();
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
		public IActionResult CreateSection(Section section)
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
		public IActionResult UpdateSection(Section section)
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

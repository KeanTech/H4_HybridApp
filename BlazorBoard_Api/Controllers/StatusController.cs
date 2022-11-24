using BlazorBoard_Api.DataAccess;
using LinqToDB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorBoard_Api.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class StatusController : Controller
	{
		private readonly BlazorBoardDB _db;

		public StatusController(BlazorBoardDB db) 
		{
			_db = db;
		}

		[HttpGet]
		[Route("Get")]
		public IActionResult Get() 
		{
			var statuses = _db.Status;

			if (statuses.Count() > 0)
				return Ok(statuses);

			return NotFound();
		}

		[HttpGet]
		[Route("Get/{id}")]
		public IActionResult Get(int id) 
		{
			var statuses = _db.Status.Where(x => x.Id == id);
			
			if(statuses.Count() > 0)
				return Ok(statuses);

			return NotFound();
		}

		[HttpPost]
		[Route("Delete")]
		public IActionResult Delete(Priority status)
		{
			try
			{
				_db.Delete(status);

			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
			return Ok("Success");
		}

		[HttpPost]
		[Route("Create")]
		public IActionResult Create(Priority status)
		{
			try
			{
				_db.Insert(status);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}

			return Ok("Success");
		}

		[HttpPost]
		[Route("Update")]
		public IActionResult Update(Priority status)
		{
			try
			{
				_db.Update(status);

			}
			catch (Exception ex)
			{
				return BadRequest(ex);
			}

			return Ok("Success");
		}
	}
}

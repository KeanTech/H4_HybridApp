using BlazorBoard_Api.DataAccess;
using LinqToDB;
using Microsoft.AspNetCore.Mvc;

namespace BlazorBoard_Api.Controllers
{
	public class PriorityController : Controller
	{
		private readonly BlazorBoardDB _db;

		public PriorityController(BlazorBoardDB db)
		{
			_db = db;
		}

		[HttpGet]
		[Route("Get")]
		public IActionResult Get()
		{
			var priorities = _db.Priorities;

			if (priorities.Count() > 0)
				return Ok(priorities);

			return NotFound();
		}

		[HttpGet]
		[Route("Get/{id}")]
		public IActionResult Get(int id)
		{
			var priorities = _db.Priorities.Where(x => x.Id == id);

			if (priorities.Count() > 0)
				return Ok(priorities);

			return NotFound();
		}

		[HttpPost]
		[Route("Delete")]
		public IActionResult Delete(Priority priority)
		{
			try
			{
				_db.Delete(priority);

			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
			return Ok("Success");
		}

		[HttpPost]
		[Route("Create")]
		public IActionResult Create(Priority Priority)
		{
			try
			{
				_db.Insert(Priority);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}

			return Ok("Success");
		}

		[HttpPost]
		[Route("Update")]
		public IActionResult Update(Priority Priority)
		{
			try
			{
				_db.Update(Priority);

			}
			catch (Exception ex)
			{
				return BadRequest(ex);
			}

			return Ok("Success");
		}
	}
}

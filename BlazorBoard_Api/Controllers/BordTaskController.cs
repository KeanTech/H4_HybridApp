using BlazorBoard_Api.DataAccess;
using LinqToDB;
using Microsoft.AspNetCore.Mvc;

namespace BlazorBoard_Api.Controllers
{
	public class BordTaskController : Controller
	{
		private readonly BlazorBoardDB _db;

		public BordTaskController(BlazorBoardDB db)
		{
			_db = db;
		}

		[HttpGet]
		[Route("Get")]
		public IActionResult Get()
		{
			var boardTasks = _db.BoardTasks;

			if (boardTasks.Count() > 0)
				return Ok(boardTasks);

			return NotFound();
		}

		[HttpGet]
		[Route("Get/{id}")]
		public IActionResult Get(int id)
		{
			var boardTasks = _db.BoardTasks.Where(x => x.Id == id);

			if (boardTasks.Count() > 0)
				return Ok(boardTasks);

			return NotFound();
		}

		[HttpPost]
		[Route("Delete")]
		public IActionResult Delete(BoardTask boardTasks)
		{
			try
			{
				_db.Delete(boardTasks);

			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
			return Ok("Success");
		}

		[HttpPost]
		[Route("Create")]
		public IActionResult Create(BoardTask boardTasks)
		{
			try
			{
				_db.Insert(boardTasks);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}

			return Ok("Success");
		}

		[HttpPost]
		[Route("Update")]
		public IActionResult Update(BoardTask boardTasks)
		{
			try
			{
				_db.Update(boardTasks);

			}
			catch (Exception ex)
			{
				return BadRequest(ex);
			}

			return Ok("Success");
		}
	}
}

using BlazorBoard_Api.DataAccess;
using LinqToDB;
using Microsoft.AspNetCore.Mvc;

namespace BlazorBoard_Api.Controllers
{
	public class ProjectBoardController : Controller
	{
		private readonly BlazorBoardDB _db;

		public ProjectBoardController(BlazorBoardDB db)
		{
			_db = db;
		}

		[HttpGet]
		[Route("Get")]
		public IActionResult Get()
		{
			var projectBoards = _db.ProjectBoards;

			if (projectBoards.Count() > 0)
				return Ok(projectBoards);

			return NotFound();
		}

		[HttpGet]
		[Route("Get/{id}")]
		public IActionResult Get(int id)
		{
			var projectBoard = _db.ProjectBoards.Where(x => x.Id == id);

			if (projectBoard.Count() > 0)
				return Ok(projectBoard);

			return NotFound();
		}

		[HttpPost]
		[Route("Delete")]
		public IActionResult Delete(BoardTask projectBoard)
		{
			try
			{
				_db.Delete(projectBoard);

			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
			return Ok("Success");
		}

		[HttpPost]
		[Route("Create")]
		public IActionResult Create(ProjectBoard projectBoard)
		{
			try
			{
				_db.Insert(projectBoard);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}

			return Ok("Success");
		}

		[HttpPost]
		[Route("Update")]
		public IActionResult Update(ProjectBoard projectBoard)
		{
			try
			{
				_db.Update(projectBoard);

			}
			catch (Exception ex)
			{
				return BadRequest(ex);
			}

			return Ok("Success");
		}
	}
}

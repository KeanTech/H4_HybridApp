using BlazorBoard_Api.DataAccess;
using BlazorBoard_Api.Services;
using LinqToDB;
using Microsoft.AspNetCore.Mvc;

namespace BlazorBoard_Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SectionTaskController : Controller
	{
		private readonly BlazorBoardDB _db;
		private readonly MailService _mailService;
		public SectionTaskController(BlazorBoardDB db)
		{
			_mailService= new MailService(db);
			_db = db;
		}

		[HttpGet]
		[Route("Get")]
		public IActionResult Get()
		{
			var sectionTasks = _db.SectionTasks;

			if (sectionTasks.Count() > 0)
				return Ok(sectionTasks);

			return NotFound();
		}

		[HttpGet]
		[Route("Get/{id}")]
		public IActionResult Get(int id)
		{
			var sectionTask = _db.SectionTasks.FirstOrDefault(x => x.Id == id);

			if (sectionTask is not null)
				return Ok(sectionTask);

			return NotFound();
		}

        [HttpGet]
        [Route("GetByStatus/{status}")]
        public IActionResult Get(string status)
        {
            var sectionTasks = _db.SectionTasks.FindBySectionStatus(status);

            if (sectionTasks is not null)
                return Ok(sectionTasks);

            return NotFound();
        }

        [HttpPost]
		[Route("Delete")]
		public IActionResult Delete(SectionTask sectionTask)
		{
			try
			{
				_db.Delete(sectionTask);

			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
			return Ok("Success");
		}

		[HttpPost]
		[Route("Create")]
		public IActionResult Create(SectionTask sectionTask)
		{
			try
			{
				_db.Insert(sectionTask);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}

			return Ok("Success");
		}

		[HttpPost]
		[Route("Update")]
		public IActionResult Update(SectionTask sectionTask)
		{
			try
			{
				if (sectionTask.Status == "Done")
					_mailService.SendMail($"{sectionTask.Text} is moved to done");
					
				_db.Update(sectionTask);

			}
			catch (Exception ex)
			{
				return BadRequest(ex);
			}

			return Ok("Success");
		}
	}
}

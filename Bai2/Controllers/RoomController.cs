using Bai2.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Bai2.Controllers
{
	public class RoomController : Controller
	{

		private IRoomRepository _roomRepository;
		public RoomController(IRoomRepository roomRepository)

		{
			_roomRepository = roomRepository;
		}



		public IActionResult Index()

		{

			var rooms = _roomRepository.GetAll();

			return View(rooms);

		}



		public IActionResult Display(int id)

		{

			var room = _roomRepository.GetById(id);

			if (room == null)

			{

				return NotFound();

			}

			return View(room);

		}

		public IActionResult Delete(int id)

		{

			var room = _roomRepository.GetById(id);

			if (room == null)

			{

				return NotFound();

			}

			return View(room);

		}



		[HttpPost, ActionName("DeleteConfirmed")]
		public IActionResult DeleteConfirmed(int id)
		{
			_roomRepository.Delete(id);


			return RedirectToAction(nameof(Index));
		}
	}
}

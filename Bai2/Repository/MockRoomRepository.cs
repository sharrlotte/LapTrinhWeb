using Bai2.Models;

namespace Bai2.Repository
{
	public class MockRoomRepository : IRoomRepository
	{

		private readonly List<Room> _rooms = new List<Room>
				{
					new Room { Id = 1, Name = "Mario", Price = 1000, Arrival= DateTime.Now, Departure= DateTime.Now, Type = "Luxury", Guests = 10 },
					new Room { Id = 2, Name = "Mario", Price = 1000, Arrival= DateTime.Now, Departure= DateTime.Now, Type = "Luxury", Guests = 10 },
				};


		public MockRoomRepository()
		{

		}

		public IEnumerable<Room> GetAll()

		{
			return _rooms;
		}



		public Room GetById(int id)

		{

			return _rooms.FirstOrDefault(p => p.Id == id);

		}



		public void Add(Room room)

		{

			room.Id = _rooms.Max(p => p.Id) + 1;

			_rooms.Add(room);

		}



		public void Update(Room room)

		{

			var index = _rooms.FindIndex(p => p.Id == room.Id);

			if (index != -1)

			{

				_rooms[index] = room;

			}

		}



		public void Delete(int id)

		{
			var room = _rooms.FirstOrDefault(p => p.Id == id);


			if (room != null)
			{
				Console.WriteLine(_rooms.Remove(room));
			}
		}
	}
}

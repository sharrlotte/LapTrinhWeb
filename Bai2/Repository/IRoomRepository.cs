using Bai2.Models;

namespace Bai2.Repository
{
    public interface IRoomRepository
    {
        IEnumerable<Room> GetAll();

        Room GetById(int id);

        void Add(Room room);

        void Update(Room room);

        void Delete(int id);


    }
}

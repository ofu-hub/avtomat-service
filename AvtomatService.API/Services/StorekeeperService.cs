using AvtomatService.Models;

namespace AvtomatService.API.Services;

public class StorekeeperService
{
    private readonly List<Storekeeper> _storekeepers;

    public StorekeeperService()
    {
        _storekeepers = new List<Storekeeper>
        {
            new Storekeeper { Id = new Guid(), Name = "Кладовщик №1" },
            new Storekeeper { Id = new Guid(), Name = "Кладовщик №2" },
            new Storekeeper { Id = new Guid(), Name = "Кладовщик №3" }
        };
    }

    public bool Authenticate(Guid storekeeperId)
    {
        return _storekeepers.Any(s => s.Id == storekeeperId);
    }
}
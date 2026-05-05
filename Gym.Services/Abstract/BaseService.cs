using Gym.Data.Data;

namespace Gym.Services.Abstract
{
    public abstract class BaseService
    {
        protected readonly GymContext _context;
        public BaseService(GymContext context)
        {
            _context = context;
        }
    }
}

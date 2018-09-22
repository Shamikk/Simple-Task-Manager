using SimpleTaskManager.API.DAL;
using SimpleTaskManager.API.Model.BLL;
using SimpleTaskManager.API.Model.BLL.Repository;

namespace SimpleTaskManager.API.Model.DAL.Repository
{
    public class SimpleTaskRepository : Repository<SimpleTask>, ISimpleTaskRepository
    {
        public SimpleTaskRepository(STMContext context) : base(context)
        {

        }
    }
}

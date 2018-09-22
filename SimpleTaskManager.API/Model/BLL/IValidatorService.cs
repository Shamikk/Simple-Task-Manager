using SimpleTaskManager.API.Model.BLL;

namespace SimpleTaskManager.Core.BLL
{
    public interface IValidatorService
    {
        bool Validate(SimpleTask simpleTask);
    }
}

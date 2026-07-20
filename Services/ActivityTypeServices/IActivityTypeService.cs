using Licenses.Dto.ActivityTypeDto;
using Licenses.Models;
using Licenses.Validators.ActivityTypeValidators;
using Licenses.ViewModels;

namespace Licenses.Services.ActivityTypeServices
{
    public interface IActivityTypeService
    {
        public  Task<ResultViewModel<IEnumerable<ActivityTypeReadDto?>>> GetAllAsync();   
        public  Task<ResultViewModel<ActivityTypeReadDto?>> GetByIdAsync(int id);
        public  Task<ResultViewModel<ActivityTypeReadDto>> AddAsync(ActivityTypeAddDto activityTypeAdd);
        public Task<ResultViewModel<ActivityTypeReadDto>> UpdateAsync(ActivityTypeReadDto activityTypeReadDto);
        public Task<ResultViewModel<bool>> SoftDeleteAsync(int id);
    }
}

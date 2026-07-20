using Licenses.Dto.ExcutivePositionDto;
using Licenses.ViewModels;

namespace Licenses.Services.ExcutivePositionServices
{
    public interface IExcutivePositionService
    {
        public Task<ResultViewModel<IEnumerable<ExcutivePositionReadDto?>>> GetAllAsync();
        public Task<ResultViewModel<ExcutivePositionReadDto?>> GetByIdAsync(int id);
        public Task<ResultViewModel<ExcutivePositionReadDto>> AddAsync(ExcutivePositionAddDto excutivePositionAddDto);
        public Task<ResultViewModel<ExcutivePositionReadDto>> UpdateAsync(ExcutivePositionReadDto excutivePositionReadDto);
        public Task<ResultViewModel<bool>> SoftDeleteAsync(int id);

    }
}

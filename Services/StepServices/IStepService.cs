using Licenses.Dto.StepDto;
using Licenses.Dto;
using Licenses.Models;
using Licenses.Validators.StepValidator;
using Licenses.ViewModels;

namespace Licenses.Services.StepServices
{
    public interface IStepService
    {
        public Task<ResultViewModel<PagedResult<StepReadDto?>>> GetAllAsync(int page, int pageSize);

        public Task<ResultViewModel<PagedResult<StepReadDto?>>> GetAllDeletedAsync(int page, int pageSize);

        public Task<ResultViewModel<StepReadDto?>> GetByIdAsync(int id);


        public Task<ResultViewModel<PagedResult<StepReadDto?>>>
            SearchByNameAsync(string name, int page, int pageSize);

        public Task<ResultViewModel<PagedResult<StepReadDto?>>>
            SearchByDeletedNameAsync(string name, int page, int pageSize);

        public Task<ResultViewModel<StepReadDto>> AddAsync(StepAddDto stepAddDto);

        public Task<ResultViewModel<StepReadDto>> UpdateAsync(StepReadDto stepReadDto);

        public Task<ResultViewModel<bool>> SoftDeleteAsync(int id);

        public Task<ResultViewModel<bool>> Revive(int id);
       
        


    }
}

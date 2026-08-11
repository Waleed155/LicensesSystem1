using Licenses.Dto.StageDto;
using Licenses.Dto;
using Licenses.Models;
using Licenses.Validators.StageValidator;
using Licenses.ViewModels;

namespace Licenses.Services.StageServices
{
    public interface IStageService
    {
        public Task<ResultViewModel<PagedResult<StageReadDto?>>>
            GetAllAsync(int page, int pageSize);

        public Task<ResultViewModel<PagedResult<StageReadDto?>>>
            GetAllDeletedAsync(int page, int pageSize);


        public Task<ResultViewModel<StageReadDto?>> GetByIdAsync(int id);

        public Task<ResultViewModel<PagedResult<StageReadDto?>>>
            SearchByNameAsync(string name, int page, int pageSize);

        public  Task<ResultViewModel<PagedResult<StageReadDto?>>>
            SearchByDeletedNameAsync(string name, int page, int pageSize);


        public Task<ResultViewModel<StageReadDto>> AddAsync(StageAddDto stageAddDto);


        public Task<ResultViewModel<StageReadDto>> UpdateAsync(StageReadDto stageReadDto);

        public Task<ResultViewModel<bool>> SoftDeleteAsync(int id);
     
        public  Task<ResultViewModel<bool>> Revive(int id);
       
        
    }
}

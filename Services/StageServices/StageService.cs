using Licenses.Dto.StepDto;
using Licenses.Dto;
using Licenses.Models;
using Licenses.Repositories.StepRepositories;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Licenses.Validators.StepValidator;
using Licenses.ViewModels;
using Licenses.Repositories.StageRepositories;
using Licenses.Dto.StageDto;
using Licenses.Validators.StageValidator;

namespace Licenses.Services.StageServices
{
    public class StageService:IStageService
    {
        IStageRepository _stageRepository;
        public StageService(IStageRepository stageRepository)
        {
            _stageRepository = stageRepository;
        }
        public async Task<ResultViewModel<PagedResult<StageReadDto?>>> GetAllAsync(int page, int pageSize)
        {
            try
            {
                var stages = _stageRepository.GetAll(page, pageSize);
                int allstagesCount = await _stageRepository.CountAsync();
                var stagesReadDto = await stages.ProjectToType<StageReadDto>().ToListAsync();
                var result = PagedResult<StageReadDto?>.
                    PaginationData(stagesReadDto, allstagesCount, page, pageSize);
                if (result.Items.Any())
                {
                    return ResultViewModel<PagedResult<StageReadDto?>>.Success(result);
                }
                else
                {
                    return ResultViewModel<PagedResult<StageReadDto?>>
                        .Success(result, " لا توجد خطوات مسجله حتي الان");
                }
            }
            catch
            {
                return ResultViewModel<PagedResult<StageReadDto?>>.
                    Failure("there is aproblem in service");

            }
        }
        public async Task<ResultViewModel<PagedResult<StageReadDto?>>> GetAllDeletedAsync(int page, int pageSize)
        {
            try
            {
                var deletedStages = _stageRepository.GetAllDeleted(page, pageSize);
                int allDeletedstagesCount = await _stageRepository.CountDeletedAsync();
                var stagesReadDto = await deletedStages.
                       ProjectToType<StageReadDto?>().
                       ToListAsync();
                var result = PagedResult<StageReadDto?>.
                    PaginationData(stagesReadDto, allDeletedstagesCount
                    , page, pageSize);
                if (result.Items.Any())
                {


                    return ResultViewModel<PagedResult<StageReadDto?>>.
                        Success(result);
                }
                else
                {
                    return ResultViewModel<PagedResult<StageReadDto?>>.
                        Success(result, " لا يوجد خطوات تم مسحها م قبل");

                }
            }
            catch
            {
                return ResultViewModel<PagedResult<StageReadDto?>>.
                    Failure("there is aproblem in service");

            }
        }
        public async Task<ResultViewModel<StageReadDto?>> GetByIdAsync(int id)
        {
            try
            {
                var stage = await _stageRepository.GetByIdAsync(id);
                var stageReadDto = stage.
                        Adapt<StageReadDto>();

                if (stageReadDto != null)
                {

                    return ResultViewModel<StageReadDto?>.
                        Success(stageReadDto);
                }
                else
                {
                    return ResultViewModel<StageReadDto?>.
                        Success(stageReadDto, "لايوجد اسم خطوه بهذه الهويه" + id);

                }
            }
            catch
            {
                return ResultViewModel<StageReadDto?>.
                    Failure("problem in service layer ");

            }
        }
        public async Task<ResultViewModel<PagedResult<StageReadDto?>>>
            SearchByNameAsync(string name, int page, int pageSize)
        {
            try
            {
               
                var stages = _stageRepository.
                    SearchByName(name, page, pageSize);
                var stagesReadDto = await stages.
                   ProjectToType<StageReadDto>().
                   ToListAsync();
                int stagesCount = await _stageRepository.
                    CountSearchAsync(name);
                var result = PagedResult<StageReadDto?>.
                    PaginationData(stagesReadDto, stagesCount, page, pageSize);
                if (stagesCount <= 0)
                    return ResultViewModel<PagedResult<StageReadDto?>>.
                        Success(result, "لايوجد خطوات بهذا الاسم");

                return ResultViewModel<PagedResult<StageReadDto?>>.Success(result);
            }
            catch
            {
                return ResultViewModel<PagedResult<StageReadDto?>>.
                    Failure("problem in service");

            }
        }
        public async Task<ResultViewModel<PagedResult<StageReadDto?>>>
            SearchByDeletedNameAsync(string name, int page, int pageSize)
        {
            try
            {

                var deletedStages = _stageRepository.
                    SearchByNameDeleted(name, page, pageSize);
                var stagesReadDto = await deletedStages.
                    ProjectToType<StageReadDto>().
                    ToListAsync();
                int deletedStagesCount = await _stageRepository.
                    CountSearchDeletedAsync(name);
                var result = PagedResult<StageReadDto?>.
                    PaginationData(stagesReadDto, deletedStagesCount, page, pageSize);
                if (deletedStagesCount <= 0)
                    return ResultViewModel<PagedResult<StageReadDto?>>.
                        Success(result, $"لايوجد اسماء خطوات تحتوي علي  هذا الاسم+{name}");
                return ResultViewModel<PagedResult<StageReadDto?>>
                    .Success(result);
            }
            catch
            {
                return ResultViewModel<PagedResult<StageReadDto?>>.Failure("problem in service");

            }
        }
        public async Task<ResultViewModel<StageReadDto>> AddAsync(StageAddDto stageAddDto)
        {
            try
            {
                var validationResult = StageValidators.
                    StageValidator(stageAddDto);
                var stageExistResult = await _stageRepository.
                    GetByNameAsync(stageAddDto.Name);
                if (!validationResult.State)
                {
                    return ResultViewModel<StageReadDto>.
                        Failure(validationResult.Message);

                }
                if (stageExistResult != null)
                {
                    return ResultViewModel<StageReadDto>.
                        Failure(" هذه الخطوه موجوده بالفعل او تم مسحها يمكنك استرجعها أفضل");
                }

                var stage = stageAddDto.Adapt<Stage>();
                var addedStage = await _stageRepository.
                    AddAsync(stage);
                await _stageRepository.
                    SaveChangesAsync();

                var stageReadDto = addedStage.Adapt<StageReadDto>();
                return ResultViewModel<
                    StageReadDto>.
                    Success(stageReadDto);
            }
            catch
            {
                return ResultViewModel<StageReadDto>.
                    Failure("there is problem in service layer  ");

            }
        }
        public async Task<ResultViewModel<StageReadDto>> UpdateAsync(StageReadDto stageReadDto)
        {
            try
            {
                var stageAddDto = stageReadDto.Adapt<StageAddDto>();
                var validationResult = StageValidators.StageValidator(stageAddDto);
                var stageExistResult = await _stageRepository.
                    GetByNameAsync(stageReadDto.Name);
                if (!validationResult.State)
                {
                    return ResultViewModel<StageReadDto>.Failure(validationResult.Message);
                }
                if (stageExistResult != null && stageExistResult.Id != stageReadDto.Id)
                {
                    return ResultViewModel<StageReadDto>.
                        Failure("هذه الخطوه  موجوده بالفعل او تم مسحها من قبل ");
                }

                var stage = stageReadDto.Adapt<Stage>();
                var updatetedStage = _stageRepository.Update(stage);
                await _stageRepository.SaveChangesAsync();
                var updatedStageReadDto = updatetedStage.Adapt<StageReadDto>();
                return ResultViewModel<StageReadDto>.Success(updatedStageReadDto);
            }
            catch
            {
                return ResultViewModel<StageReadDto>.Failure("there is problem in service");
            }
        }
        public async Task<ResultViewModel<bool>> SoftDeleteAsync(int id)
        {
            try
            {
                var stage = await _stageRepository.GetByIdAsync(id);
                if (stage== null) return ResultViewModel<bool>.Failure("لا توجد خطوه بهذا الرقم");
                bool result = _stageRepository.SoftDelete(stage);
                if (!result) return ResultViewModel<bool>.Failure("problem in repo ");
                await _stageRepository.SaveChangesAsync();
                return ResultViewModel<bool>.Success(result);
            }
            catch
            {
                return ResultViewModel<bool>.Failure("there is problem in service");
            }

        }
        public async Task<ResultViewModel<bool>> Revive(int id)
        {
            try
            {
                var stage = await _stageRepository.
                    GetByIdAsync(id);
                if (stage == null)
                    return ResultViewModel<bool>.
                        Failure("لا توجد خطوه تم مسحها بهذا الرقم");
                bool result = _stageRepository.Revive(stage);
                if (!result) return ResultViewModel<bool>.Failure("problem in repo");
                await _stageRepository.
                    SaveChangesAsync();
                return ResultViewModel<bool>.
                    Success(result);
            }
            catch
            {
                return ResultViewModel<bool>.Failure("there is problem in service");
            }

        }

    }
}

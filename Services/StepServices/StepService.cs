using Licenses.Dto.StepDto;
using Licenses.Dto;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Licenses.Models;
using Licenses.Repositories.OrderRepositpories;
using Licenses.Validators.StepValidator;
using Licenses.ViewModels;
using Licenses.Repositories.StepRepositories;
using Licenses.Dto.OrderDto;

namespace Licenses.Services.StepServices
{
    public class StepService:IStepService
    {
        IStepRepository _stepRepository;
        public StepService(IStepRepository stepRepository)
        {
            _stepRepository = stepRepository;
        }
        public async Task<ResultViewModel<PagedResult<StepReadDto?>>> GetAllAsync(int page , int pageSize )
        {
          try
    {
                var steps = _stepRepository.GetAll(page, pageSize);
                int allstepsCount = await _stepRepository.CountAsync();
                var stepsReadDto = await steps.ProjectToType<StepReadDto>().ToListAsync();
                var result = PagedResult<StepReadDto?>.PaginationData(stepsReadDto, allstepsCount, page, pageSize);
                if (result.Items.Any())
                 {
                   return ResultViewModel<PagedResult<StepReadDto?>>.Success(result);
                 }
               else
                {
                   return ResultViewModel<PagedResult<StepReadDto?>>.Success(result," لا توجد خطوات مسجله حتي الان");
                }
    }
     catch
      {
        return ResultViewModel<PagedResult<StepReadDto?>>.Failure("there is aproblem in service");

      }
        }
        public async Task<ResultViewModel<PagedResult<StepReadDto?>>> GetAllDeletedAsync(int page , int pageSize)
        {
            try
            {
                var deletedSteps = _stepRepository.GetAllDeleted(page, pageSize);
                int allDeletedstepsCount = await _stepRepository.CountDeletedAsync();
                var stepsReadDto = await deletedSteps.
                       ProjectToType<StepReadDto?>().
                       ToListAsync();
                var result = PagedResult<StepReadDto?>.
                    PaginationData(stepsReadDto, allDeletedstepsCount
                    , page, pageSize);
                if (result.Items.Any() )
                {
                   

                    return ResultViewModel<PagedResult<StepReadDto?>>.
                        Success(result);
                }
                else
                {
                    return ResultViewModel<PagedResult<StepReadDto?>>.
                        Success(result," لا يوجد خطوات تم مسحها م قبل");

                }
            }
            catch
            {
                return ResultViewModel<PagedResult<StepReadDto?>>.Failure("there is aproblem in service");

            }
        }
        public async Task<ResultViewModel<StepReadDto?>> GetByIdAsync(int id)
        {
            try
            {
                var step = await _stepRepository.GetByIdAsync(id);
                var stepReadDto = step.
                        Adapt<StepReadDto>();

                if (stepReadDto != null)
                {
                   
                    return ResultViewModel<StepReadDto?>.
                        Success(stepReadDto);
                }
                else
                {
                    return ResultViewModel<StepReadDto?>.
                        Success(stepReadDto,"لايوجد اسم خطوه بهذه الهويه" + id);

                }
            }
            catch
            {
                return ResultViewModel<StepReadDto?>.
                    Failure("problem in service layer ");

            }
        }
        public async Task<ResultViewModel<PagedResult<StepReadDto?>>> 
            SearchByNameAsync(string name, int page , int pageSize )
        {
            try
            {
                
                var steps = _stepRepository.
                    SearchByName(name, page, pageSize);
                var stepsReadDto = await steps.
                   ProjectToType<StepReadDto>().
                   ToListAsync();
                int stepsCount = await _stepRepository.
                    CountSearchAsync(name);
                var result = PagedResult<StepReadDto?>.
                    PaginationData(stepsReadDto, stepsCount, page, pageSize);
                if (stepsCount <= 0)
                    return ResultViewModel<PagedResult<StepReadDto?>>.
                        Success(result,"لايوجد خطوات بهذا الاسم");
            
                return ResultViewModel<PagedResult<StepReadDto?>>.Success(result);
            }
            catch
            {
                return ResultViewModel<PagedResult<StepReadDto?>>.
                    Failure("problem in service");

            }
        }
        public async Task<ResultViewModel<PagedResult<StepReadDto?>>>
            SearchByDeletedNameAsync(string name, int page , int pageSize )
        {
            try
            {

                var deletedStepss = _stepRepository.
                    SearchByNameDeleted(name, page, pageSize);
                var stepsReadDto = await deletedStepss.
                    ProjectToType<StepReadDto>().
                    ToListAsync();
                int deletedStepsCount = await _stepRepository.
                    CountSearchDeletedAsync(name);
                var result = PagedResult<StepReadDto?>.
                    PaginationData(stepsReadDto, deletedStepsCount, page, pageSize);
                if (deletedStepsCount <=0)
                return ResultViewModel<PagedResult<StepReadDto?>>.Success(result,$"لايوجد اسماء خطوات تحتوي علي  هذا الاسم+{name}");
                return ResultViewModel<PagedResult<StepReadDto?>>.Success(result);
            }
            catch
            {
                return ResultViewModel<PagedResult<StepReadDto?>>.Failure("problem in service");

            }
        }
        public async Task<ResultViewModel<StepReadDto>> AddAsync(StepAddDto stepAddDto)
        {
            try
            {
                var validationResult = StepValidators.
                    StepValidator(stepAddDto);
                var stepExistResult = await _stepRepository.
                    GetByNameAsync(stepAddDto.Name);
                if (!validationResult.State)
                {
                    return ResultViewModel<StepReadDto>.
                        Failure(validationResult.Message);

                }
                if (stepExistResult != null)
                {
                    return ResultViewModel<StepReadDto>.
                        Failure(" هذه الخطوه موجوده بالفعل او تم مسحها يمكنك استرجعها أفضل");
                }

                var step = stepAddDto.Adapt<Step>();
                var addedStep = await _stepRepository.AddAsync(step);
                await _stepRepository.SaveChangesAsync();

                var stepReadDto = addedStep.Adapt<StepReadDto>();
                return ResultViewModel<
                    StepReadDto>.
                    Success(stepReadDto);
            }
            catch
            {
                return ResultViewModel<StepReadDto>.
                    Failure("there is problem in service layer  ");

            }
        }
        public async Task<ResultViewModel<StepReadDto>> UpdateAsync(StepReadDto stepReadDto)
        {
            try
            {
                var stepAddDto = stepReadDto.Adapt<StepAddDto>();
                var validationResult = StepValidators.StepValidator(stepAddDto);
                var orderExistResult = await _stepRepository.GetByNameAsync(stepReadDto.Name);
                if (!validationResult.State)
                {
                    return ResultViewModel<StepReadDto>.Failure(validationResult.Message);
                }
                if (orderExistResult != null && orderExistResult.Id != stepReadDto.Id)
                {
                    return ResultViewModel<StepReadDto>.
                        Failure("هذه الخطوه  موجوده بالفعل او تم مسحها من قبل ");
                }

                var step = stepReadDto.Adapt<Step>();
                var updatetedStep = _stepRepository.Update(step);
                await _stepRepository.SaveChangesAsync();
                var updatedStepReadDto = updatetedStep.Adapt<StepReadDto>();
                return ResultViewModel<StepReadDto>.Success(updatedStepReadDto);
            }
            catch
            {
                return ResultViewModel<StepReadDto>.Failure("there is problem in service");
            }
        }
        public async Task<ResultViewModel<bool>> SoftDeleteAsync(int id)
        {
            try
            {
                var step = await _stepRepository.GetByIdAsync(id);
                if (step == null) return ResultViewModel<bool>.Failure("لا توجد خطوه بهذا الرقم");
                bool result = _stepRepository.SoftDelete(step);
                if (!result) return ResultViewModel<bool>.Failure("problem in repo ");
                await _stepRepository.SaveChangesAsync();
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
                var step = await _stepRepository.GetByIdAsync(id);
                if (step == null)
                    return ResultViewModel<bool>.Failure("لا توجد خطوه تم مسحها بهذا الرقم");
                bool result = _stepRepository.Revive(step);
                if (!result) return ResultViewModel<bool>.Failure("problem in repo");
                await _stepRepository.SaveChangesAsync();
                return ResultViewModel<bool>.Success(result);
            }
            catch
            {
                return ResultViewModel<bool>.Failure("there is problem in service");
            }

        }


    }
}

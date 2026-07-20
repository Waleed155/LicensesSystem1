using Microsoft.EntityFrameworkCore;
using Licenses.Repositories.ActivityTypeRepositories;
using Licenses.Dto.ClientsDto;
using Licenses.Dto;
using Licenses.ViewModels;
using Licenses.Dto.ActivityTypeDto;
using Licenses.Validators.ActivityTypeValidators;

using Mapster;
using Licenses.Models;
using Licenses.Validators.Client;


namespace Licenses.Services.ActivityTypeServices
{
    public class ActivityTypeService:IActivityTypeService
    {
         IActivityTypeRepository _activityTypeRepository;
        public ActivityTypeService(IActivityTypeRepository activityTypeRepository)
        {
             _activityTypeRepository=activityTypeRepository;
        }
        public async Task<ResultViewModel<IEnumerable<ActivityTypeReadDto?>>> GetAllAsync()
        {
            try
            {
                var activities = _activityTypeRepository.GetAll();
                if (activities.Count() > 0)
                {
                    var activitiesTypeReadDto = await activities.ProjectToType<ActivityTypeReadDto>().ToListAsync();
                    
                    return ResultViewModel<IEnumerable<ActivityTypeReadDto?>>.Success(activitiesTypeReadDto);
                }
                else
                {
                    return ResultViewModel<IEnumerable<ActivityTypeReadDto?>>.Failure("لا توجد أنشطه ");
                }
            }
            catch
            {
                return ResultViewModel<IEnumerable<ActivityTypeReadDto?>>.Failure("there is aproblem in service");

            }
        }
        public async Task<ResultViewModel<ActivityTypeReadDto?>> GetByIdAsync(int id)
        {
            try
            {
                var activityType = await _activityTypeRepository.GetByIdAsync(id);

                if (activityType != null)
                {
                    var activityTypeDto = activityType.Adapt<ActivityTypeReadDto>();
                    return ResultViewModel<ActivityTypeReadDto?>.Success(activityTypeDto);
                }
                else
                {
                    return ResultViewModel<ActivityTypeReadDto?>.Failure("لايوجد عميل بهذه الهويه" + id);

                }
            }
            catch
            {
                return ResultViewModel<ActivityTypeReadDto?>.Failure("problem in service layer ");

            }
        }

        public async Task<ResultViewModel<ActivityTypeReadDto>> AddAsync(ActivityTypeAddDto activityTypeAdd)
        {
            try
            {
                var validationResult=ActivityTypeValidator.ActivityTypeValidate(activityTypeAdd);
               var activityTypeExistResult=await _activityTypeRepository.GetByNameAsync(activityTypeAdd.Name);
                if (!validationResult.State)
                {
                    return ResultViewModel<ActivityTypeReadDto>.Failure(validationResult.Message);

                }
                if (activityTypeExistResult != null)
                {
                    return ResultViewModel<ActivityTypeReadDto>.Failure("هذا النشاط موجود بالفعل او تم مسحه من الافضل ارجاعه بدلا م اضافه جديد");
                }
               
                    var activityType = activityTypeAdd.Adapt<ActivityType>();
                    var addedactivityType = await _activityTypeRepository.AddAsync(activityType);
                    await _activityTypeRepository.SaveChangesAsync();

                    var activityTypeReadDto = addedactivityType.Adapt<ActivityTypeReadDto>();
                    return ResultViewModel<ActivityTypeReadDto>.Success(activityTypeReadDto);
                
               

                
            }
            catch
            {
                  return ResultViewModel<ActivityTypeReadDto>.Failure("there is problem in service layer  ");

            }
        }
        public async Task<ResultViewModel<ActivityTypeReadDto>> UpdateAsync(ActivityTypeReadDto activityTypeReadDto)
        {
            try
            {
                var activityTypeAddDto = activityTypeReadDto.Adapt<ActivityTypeAddDto>();
                var validationResult = ActivityTypeValidator.ActivityTypeValidate(activityTypeAddDto);
                var activityTypeExistResult = await _activityTypeRepository.GetByNameAsync(activityTypeReadDto.Name);
                if (!validationResult.State )
                {
                    return ResultViewModel<ActivityTypeReadDto>.Failure(validationResult.Message);
                }
                if (activityTypeExistResult != null && activityTypeExistResult.Id != activityTypeReadDto.Id)
                {
                    return ResultViewModel<ActivityTypeReadDto>.
                        Failure("هذا النشاط موجود بالفعل او تم مسحه من قبل "); 
                }

                  var activity = activityTypeReadDto.Adapt<ActivityType>();
                  var updatetedActivityType = _activityTypeRepository.Update(activity);
                  await _activityTypeRepository.SaveChangesAsync();
                  var updatedActivityTypeReadDto = updatetedActivityType.Adapt<ActivityTypeReadDto>();
                  return ResultViewModel<ActivityTypeReadDto>.Success(updatedActivityTypeReadDto);
            }
            catch
            {
                return ResultViewModel<ActivityTypeReadDto>.Failure("there is problem in service");
            }
        }
        public async Task<ResultViewModel<bool>> SoftDeleteAsync(int id)
        {
            try
            {
                var activityType = await _activityTypeRepository.GetByIdAsync(id);
                if (activityType == null) return ResultViewModel<bool>.Failure("لا يوجد نشاط بهذا الرقم");
                bool result = _activityTypeRepository.SoftDelete(activityType);
                if (!result) return ResultViewModel<bool>.Failure("problem in repo");
                await _activityTypeRepository.SaveChangesAsync();
                return ResultViewModel<bool>.Success(result);
            }
            catch
            {
                return ResultViewModel<bool>.Failure("there is problem in service");
            }

        }

    }
}

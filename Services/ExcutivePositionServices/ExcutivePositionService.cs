using Licenses.Dto.ActivityTypeDto;
using Licenses.Dto.ExcutivePositionDto;
using Licenses.Models;
using Licenses.Repositories.ExcutivePositionRepositories;
using Licenses.Validators.ActivityTypeValidators;
using Licenses.Validators.ExcutivePositionValidator;
using Licenses.ViewModels;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Licenses.Services.ExcutivePositionServices
{
    public class ExcutivePositionService:IExcutivePositionService
    {
        IExcutivePositionRepository _excutivePositionRepository;
        public ExcutivePositionService(IExcutivePositionRepository excutivePositionRepository) 
        {
            _excutivePositionRepository = excutivePositionRepository;

        }

        public async Task<ResultViewModel<IEnumerable<ExcutivePositionReadDto?>>> GetAllAsync()
        {
            try
            {
                var excutivePositions = _excutivePositionRepository.GetAll();
                if (excutivePositions.Count() > 0)
                {
                    var excutivePositionReadDto = await excutivePositions.
                        ProjectToType<ExcutivePositionReadDto>().ToListAsync();

                    return ResultViewModel<IEnumerable<ExcutivePositionReadDto?>>.Success(excutivePositionReadDto);
                }
                else
                {
                    return ResultViewModel<IEnumerable<ExcutivePositionReadDto?>>.Failure("لا توجد لاتجد مواقف تنفيذيه ");
                }
            }
            catch
            {
                return ResultViewModel<IEnumerable<ExcutivePositionReadDto ?>>.
                    Failure("there is aproblem in service");

            }

        }
        public async Task<ResultViewModel<ExcutivePositionReadDto?>> GetByIdAsync(int id)
        {
            try
            {
                var excutivePosition = await _excutivePositionRepository.GetByIdAsync(id);

                if (excutivePosition != null)
                {
                    var excutivePositionReadDto = excutivePosition.Adapt<ExcutivePositionReadDto>();
                    return ResultViewModel<ExcutivePositionReadDto ?>.Success(excutivePositionReadDto);
                }
                else
                {
                    return ResultViewModel<ExcutivePositionReadDto?>.Failure("لايوجد عميل بهذه الهويه" );

                }
            }
            catch
            {
                return ResultViewModel<ExcutivePositionReadDto?>.Failure("problem in service layer ");

            }
        }

        public async Task<ResultViewModel<ExcutivePositionReadDto>> AddAsync(ExcutivePositionAddDto excutivePositionAddDto)
        {
            try
            {
                var validationResult = ExcutivePositionValidator.ExcutiveValidator(excutivePositionAddDto);
                var excutivePositionExistResult = await _excutivePositionRepository.GetByNameAsync(excutivePositionAddDto.Name);
                if (!validationResult.State)
                {
                    return ResultViewModel<ExcutivePositionReadDto>.Failure(validationResult.Message);
                }
                if (excutivePositionExistResult != null)
                {
                    return ResultViewModel<ExcutivePositionReadDto>.
                        Failure(" هذا الموقف موجود بالفعل او تم مسحه من الافضل ارجاعه بدلا م اضافه جديد\");\r\n                }");
                }
                   var excutivePosition = excutivePositionAddDto.Adapt<ExcutivePosition>();
                    var addedExcutivePositiion = await _excutivePositionRepository.AddAsync(excutivePosition);
                     await _excutivePositionRepository.SaveChangesAsync();
                      var excutivePositionReadDto = addedExcutivePositiion.Adapt<ExcutivePositionReadDto>();
                       return ResultViewModel<ExcutivePositionReadDto>.Success(excutivePositionReadDto);
               
            }
            catch
            {
                return ResultViewModel<ExcutivePositionReadDto>.Failure("there is problem in service layer  ");

            }
        }
        public async Task<ResultViewModel<ExcutivePositionReadDto>> UpdateAsync(ExcutivePositionReadDto excutivePositionReadDto)
        {
            try
            {
                var excutivePositionAddDto = excutivePositionReadDto.Adapt<ExcutivePositionAddDto>();
                var validationResult = ExcutivePositionValidator.ExcutiveValidator(excutivePositionAddDto);
                if (!validationResult.State)
                {
                    return ResultViewModel<ExcutivePositionReadDto>.Failure(validationResult.Message);

                }

                var excutivePositionExistResult = await _excutivePositionRepository.GetByNameAsync(excutivePositionReadDto.Name);

                if (excutivePositionExistResult != null && excutivePositionExistResult.Id != excutivePositionReadDto.Id)
                {
                    return ResultViewModel<ExcutivePositionReadDto>.
                        Failure("هذا الموقف موجود بالفعل او تم مسحه من قبل");
                }

                
                    var excutivePosition = excutivePositionReadDto.Adapt<ExcutivePosition>();
                    var updatetedExcutivePosition =  _excutivePositionRepository.Update(excutivePosition);
                    await _excutivePositionRepository.SaveChangesAsync();
                    var updatedExcutiveDto = updatetedExcutivePosition.Adapt<ExcutivePositionReadDto>();
                    return ResultViewModel<ExcutivePositionReadDto>.Success(updatedExcutiveDto);
                
               
            }
            catch
            {
                return ResultViewModel<ExcutivePositionReadDto>.Failure("there is problem in service");
            }
        }
        public async Task<ResultViewModel<bool>> SoftDeleteAsync(int id)
        {
            try
            {
                var excutivePOsition = await _excutivePositionRepository.GetByIdAsync(id);
                if (excutivePOsition == null) return ResultViewModel<bool>.Failure("لا يوجد موقف تنفيذي بهذا الرقم");
                bool result = _excutivePositionRepository.SoftDelete(excutivePOsition);
                if (!result) return ResultViewModel<bool>.Failure("problem in repo");
                await _excutivePositionRepository.SaveChangesAsync();
                return ResultViewModel<bool>.Success(result);
            }
            catch
            {
                return ResultViewModel<bool>.Failure("there is problem in service");
            }

        }


    }
}

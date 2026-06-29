using Licenses.Dto.ClientsDto;
using Licenses.Dto;

using Licenses.Dto.LotDto;
using Licenses.Models;
using Licenses.Repositories.ClientRepositories;
using Licenses.ViewModels;
using Licenses.Validators.Client;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using Licenses.ViewModels.ClientViewModels;

namespace Licenses.Services.ClientServices
{ 
    public class ClientService:IClientService
    {
        readonly IClientRepository _clientRepository;
        public ClientService(IClientRepository clientRepository)
        {
        _clientRepository = clientRepository;
        }
        
        
        public async Task<ResultViewModel< PagedResult< ClientReadDto?>>>GetAllAsync(int page = 1, int pageSize = 15)
        {
            try
            {
                var Clients = _clientRepository.GetAll(page, pageSize);
                var countClients =await _clientRepository.CountAsync();
                if (countClients>0)
                {
                    var clientsReadDto = await Clients.ProjectToType<ClientReadDto>().ToListAsync();
                    var result = PagedResult<ClientReadDto>.
                        PaginationData(clientsReadDto, page, pageSize, countClients);
                    return ResultViewModel<PagedResult<ClientReadDto?>>.Success(result);
                }
                else
                {
                    return ResultViewModel<PagedResult<ClientReadDto?>>.Failure("لا يوجد عملاء");
                }
            }
            catch
            {
                return ResultViewModel<PagedResult<ClientReadDto?>>.Failure("there is aproblem in service");

            }

        }
       /* public async Task<ResultViewModel<IEnumerable<ClientReadDto?>>> GetByNameAsync( string name,int page = 1, int pageSize = 15)
        {
            try
            {
                var Clients = _clientRepository.GetByName(name,page, pageSize);
                if (Clients.Count() >= 1)
                {
                    var clientsReadDto = await Clients.ProjectToType<ClientReadDto>().ToListAsync();
                    return ResultViewModel<IEnumerable<ClientReadDto?>>.Success(clientsReadDto);
                }
                else
                {
                    return ResultViewModel<IEnumerable<ClientReadDto?>>.Failure("لا يوجد عملاءتحتوي ع هذا الاسم ");
                }
            }
            catch (Exception ex)
            {
                return ResultViewModel<IEnumerable<ClientReadDto?>>.Failure("there is problem in service layer");

            }

        }*/
        public async Task< ResultViewModel< ClientReadDto?>> GetByIdAsync(int id)
        {
            try
            {
                var client = await _clientRepository.GetByIdAsync(id);

                if (client != null)
                {
                    var clientDto = client.Adapt<ClientReadDto>();
                    return ResultViewModel<ClientReadDto?>.Success(clientDto);
                }
                else
                {
                    return ResultViewModel<ClientReadDto?>.Failure("لايوجد عميل بهذه الهويه" + id);

                }
            }catch
            {
                return ResultViewModel<ClientReadDto?>.Failure("problem in service layer ");

            }
        }
        public async Task<ResultViewModel<ClientWithLotDto>> GetByIdWithLotsAsync(int id)
        {
            try
            {
                var clientWithLot = await _clientRepository.GetByIdWithLotsAsync(id);
                TypeAdapterConfig<Client, ClientWithLotDto>.
                   NewConfig().
                   Map(dst => dst.LotReadDtos,
                   src => src.Lots.Adapt<IEnumerable<LotReadDto>>());
                var clientWithLotDto = clientWithLot.Adapt<ClientWithLotDto>();
                if (clientWithLotDto != null)
                {
                    return ResultViewModel<ClientWithLotDto>.Success(clientWithLotDto);
                }
                else
                {
                    return ResultViewModel<ClientWithLotDto>.Failure("لا يوجد عملاء بهذه الهويه ");
                }
            }
            catch
            {
                return ResultViewModel<ClientWithLotDto>.Failure("problem in service layer ");

            }

        }
        public async Task<ResultViewModel<ClientReadDto?>> GetByNationalIdAsync(string nationalid)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(nationalid) && Regex.IsMatch(nationalid, @"^\d{14}$"))
                {
                    var client = await _clientRepository.GetByNationalIdAsync(nationalid);

                    if (client != null)
                    {
                        var clientDto = client.Adapt<ClientReadDto>();
                        return ResultViewModel<ClientReadDto?>.Success(clientDto);
                    }
                    else
                    {
                        return ResultViewModel<ClientReadDto?>.Failure("لايوجد عميل بهذه الهويه" + nationalid);

                    }
                }
                else
                {
                    return ResultViewModel<ClientReadDto?>.Failure("رقم الهويه هذا يحتوي ع مسافات وهذا غير صحيح الرجاء كتابه الرقم القومي بدون مسافات او ان يكون فارغ يجب عليك كتابه 14 رقم المدونين ع بطاقه الرقم القومي الخاصه بالعميل " + nationalid);

                }
            }
            catch
            {
                return ResultViewModel<ClientReadDto?>.Failure("problem in service layer ");

            }
        }
        public async Task<ResultViewModel<PagedResult<ClientReadDto>>> GetByNameOrNationalId(string name, int page = 1, int pageSize = 15)
        {
            try
            {
                   var clients= _clientRepository.GetByNameOrNationalId(name, page, pageSize);
                var countClients=await _clientRepository.CountSearchAsync(name);
                if (countClients > 0)
                {
                    var clientsReadDto = await clients.ProjectToType<ClientReadDto>().ToListAsync();

                    var result = PagedResult<ClientReadDto>.
                        PaginationData(clientsReadDto, page, pageSize, countClients);
                    return ResultViewModel<PagedResult<ClientReadDto>>
                        .Success(result);

                }
                else
                {
                    return ResultViewModel<PagedResult<ClientReadDto>>.Failure("لا يوجد عملاء");

                }
            }
            catch
            {
                return ResultViewModel<PagedResult<ClientReadDto>>.Failure("there is  problem in service");

            }
        }
        public async  Task <ResultViewModel <ClientReadDto>> AddAsync(ClientAddDto clientAddDto)
        {
        

            try
            {
                var validationResult=ClientValidator.ClientValidate(clientAddDto);
                var clientExistResult = await _clientRepository.GetByNationalIdAsync(clientAddDto.NationalId);
                if (clientExistResult != null)
                    return ResultViewModel<ClientReadDto>.Failure("هذا الرقم القومي مسجل ل عميل اخر");
                if (validationResult.State && validationResult.Result)
                {
                    var client = clientAddDto.Adapt<Client>();
                    var addedclient = await _clientRepository.AddAsync(client);
                    await _clientRepository.SaveChangesAsync();

                    var clientReadDto = addedclient.Adapt<ClientReadDto>();
                    return ResultViewModel<ClientReadDto>.Success(clientReadDto);
                }
                else
                {
                    return ResultViewModel<ClientReadDto>.Failure(validationResult.Message);

                }
            }catch
            {
                return ResultViewModel<ClientReadDto>.Failure("there is problem in service layer  ");

            }
        }
        public async Task<ResultViewModel< ClientReadDto>> UpdateAsync(ClientReadDto clientReadDto)
        {
            try
            {
                var clientExistResult = await _clientRepository.GetByNationalIdAsync(clientReadDto.NationalId);
                if (clientExistResult != null &&clientExistResult.Id!=clientReadDto.Id)
                    return ResultViewModel<ClientReadDto>.Failure("هذا الرقم القومي مسجل ل عميل اخر");

                var validationResult = ClientValidator.ClientValidate(clientReadDto.Adapt<ClientAddDto>());

                if (validationResult.State && validationResult.Result)
                {
                    var client = clientReadDto.Adapt<Client>();
                    var updatetedClient = _clientRepository.Update(client);
                    await _clientRepository.SaveChangesAsync();
                    var updatedClientReadDto = updatetedClient.Adapt<ClientReadDto>();

                    return ResultViewModel<ClientReadDto>.Success(updatedClientReadDto);
                }
                else
                {
                   return ResultViewModel<ClientReadDto>.Failure(validationResult.Message);

                }
            }
            catch 
            { 
                return ResultViewModel<ClientReadDto>.Failure("there is problem in service");
            }
        }
        public async Task<ResultViewModel< bool>> SoftDeleteAsync( int id)
        {
            try
            {
                var client = await _clientRepository.GetByIdAsync(id);
                if (client == null) return ResultViewModel<bool>.Failure("there is problem in service");
                bool result = _clientRepository.SoftDelete(client);
                if(!result) return ResultViewModel<bool>.Failure("problem in repo");
                await _clientRepository.SaveChangesAsync();
                 return ResultViewModel<bool>.Success(result);
            }
            catch 
            {
                return ResultViewModel<bool>.Failure("there is problem in service");
            }

        }
    }
}

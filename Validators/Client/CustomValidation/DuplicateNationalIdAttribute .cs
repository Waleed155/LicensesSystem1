using Licenses.Dto.ClientsDto;
using Licenses.Repositories.ClientRepositories;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
namespace Licenses.Validators.Client.CustomValidation
{
    public class DuplicateNationalIdAttribute :ValidationAttribute
    {
        IClientRepository _clientRepository;
        public DuplicateNationalIdAttribute(IClientRepository clientRepository) 
        {
        _clientRepository = clientRepository;
        }
        //protected override async  Task< ValidationResult> IsValid(object? value,ValidationContext validationContext)
        //{
        //    string nationalId = value?.ToString();
        //    ClientReadDto clientEditingReadDto= validationContext.ObjectInstance as ClientReadDto;
        //    var client = await _clientRepository.GetByNationalIdAsync(nationalId);
        //    if (client == null || client.Id==clientEditingReadDto.Id) 
        //    {
        //        return  ValidationResult.Success;
        //    }
        //    else
        //    {
        //        return new ValidationResult("هذا الرقم القومي مسج بالفعل ل عميل اخر ");
        //    }
        //}
    }
}

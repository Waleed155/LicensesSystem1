using Licenses.Dto.ClientsDto;
using Licenses.ViewModels;
using System.Text.RegularExpressions;

namespace Licenses.Validators.Client
{
    public static class ClientValidator
    {
        public static ResultViewModel< bool> NationalIdValidator(string nationalId)
        {
            #region Check
            if (string.IsNullOrWhiteSpace(nationalId )) return ResultViewModel<bool>.
                    Failure("لا يوجد رقم قومي تم ادخاله ") ;
            if(!Regex.IsMatch(nationalId, @"^[0-9]{14}$")) return ResultViewModel<bool>.
                    Failure("  الرجاء ادخال رقم قومي مكون م 14 رقم باللغه الانجليزيه فقط ");
            #endregion
            #region CheckCentury
            int century = nationalId[0] - '0';
            string preYearNumber = "";
            if(century !=2&&century!=3) return ResultViewModel<bool>.
                    Failure("يجب ان يكون الرقم القومي  يبدا ب رقم 2 او 3");
            if (century == 2) preYearNumber = "19";
            if (century == 3) preYearNumber = "20";

            #endregion
            #region Check birthdate
            bool yearSucces = int.TryParse( preYearNumber+nationalId.Substring(1, 2),out int yearNumber);
            bool monthSucces = int.TryParse(nationalId.Substring(3, 2), out int monthNumber);
            bool daySucces = int.TryParse(nationalId.Substring(5, 2), out int dayNumber);
            if(!yearSucces||!monthSucces||!daySucces) return ResultViewModel<bool>.Failure("ther is problem in validation");
            bool dateSuccess = DateTime.TryParse($"{yearNumber}-{monthNumber:D2}-{dayNumber:D2}",out DateTime birthDateTime);
            if(!dateSuccess) return ResultViewModel<bool>.Failure("تاريخ الميلاد المسجل بالرقم القومي خاطئ");
            if (birthDateTime > DateTime.Today.AddYears(-15) ||
               birthDateTime < DateTime.Today.AddYears(-90))
                return  ResultViewModel<bool>.Failure("يجب الا يكون عمر العميل اقل م 15 سنه او اكبر 90 سنه");
            #endregion
            #region Check GovernrateCode
            bool governrateSuccess =int.TryParse(nationalId.Substring(7,2),out int governrateNumber);
            if (!governrateSuccess) return ResultViewModel<bool>.Failure("ther is problem in validation");
             int[] governratesCode = { 1, 2, 3, 4, 
                11, 12, 13, 14, 15, 16, 17, 18, 19, 
                21, 22, 23, 24, 25, 26, 27, 28, 29, 
                31, 32, 33, 34, 35, 88 };
            if(!governratesCode.Contains(governrateNumber))
                return ResultViewModel<bool>.Failure("كود محافظه خاطئ");
            #endregion

            return ResultViewModel<bool>.Success(true);

        }
       
        public static ResultViewModel< bool> NameValidator(string name)
        {
            if(string.IsNullOrWhiteSpace(name)) return ResultViewModel<bool>.Failure("لا يوجد اسم ");
            name = name.Trim();
            name = Regex.Replace(name, @"\s+", " ");
            if (!Regex.IsMatch(name, @"^[\u0600-\u06FF\s]+$")) return ResultViewModel<bool>.Failure("ارجاء ادخال الاسم باللغه العربيه");
            var parts = name.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                foreach( var part in parts)
            {
                if(part.Length < 2) return ResultViewModel<bool>.Failure("اسم العميل يجب ان يكون ع الاقل اسم ثلاثي وان يكون كل اسم مكون م حرفين او اكثر ");
            }
                 return ResultViewModel<bool>.Success(true);
        }
        public static ResultViewModel<bool> PhoneValidator(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone)) return ResultViewModel<bool>.Failure("لا يوجد رقم هاتف ");

            phone = phone.Trim();
            phone = Regex.Replace(phone, @"\s+", "");
            if(phone.Length!=11||!phone.All(char.IsDigit)) return ResultViewModel<bool>.Failure("يجب ان يكون رقم الهاتف مكون 1 رقم ولا يحتوي ع حروف ");
            string prefix = phone.Substring(0, 3);
            string[] validPrefixes = { "010", "011", "012", "015" };
            if(!validPrefixes.Contains(prefix)) return ResultViewModel<bool>.Failure("يجب بداء ارقم ب  الارقام المعتمده م شركات الاتصالات المصريه مثل 010");
            return ResultViewModel<bool>.Success(true);
        }
        public static ResultViewModel<bool> ClientValidate(ClientAddDto clientAddDto)
        {
            ResultViewModel<bool> nameValidatorResult = NameValidator(clientAddDto.Name);
            if (!nameValidatorResult.State)
                return ResultViewModel<bool>.Failure(nameValidatorResult.Message);
            ResultViewModel<bool> nationalIdValidatorResult=NationalIdValidator(clientAddDto.NationalId);
            if (!nationalIdValidatorResult.State)
                return ResultViewModel<bool>.Failure(nationalIdValidatorResult.Message);
            ResultViewModel<bool> phoneValidatorResult = PhoneValidator(clientAddDto.FirstPhoneNumber);
            if (!phoneValidatorResult.State)
                return ResultViewModel<bool>.Failure(phoneValidatorResult.Message);
            return ResultViewModel<bool>.Success(true);
        }
    }
}

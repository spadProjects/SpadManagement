using FluentValidation;
using SpadManagement.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpadManagement.DataAccess.Validation
{
    public class ContractPlanValidator : AbstractValidator<ContractPlan>
    {
        private static ContractPlanValidator _instance;
        public static ContractPlanValidator GetInstance()
        {
            if (_instance == null)
            {
                _instance = new ContractPlanValidator();
            }

            return _instance;
        }

        public ContractPlanValidator()
        {
            RuleFor(p => p.Price).NotNull().WithMessage("قیمت را وارد کنید")
                .GreaterThanOrEqualTo(0).WithMessage("قیمت نمیتواند منفی باشد");

            //RuleFor(p => p.PersianFullName).NotNull().WithMessage("نام و نام خانوادگی را وارد کنید");
            //RuleFor(p => p.BirthDate).NotNull().WithMessage("تاریخ تولد را وارد کنید");
            //RuleFor(p => p.Duration).NotNull().WithMessage("تعداد شب را وارد کنید");
            //RuleFor(p => p.HotelName).NotNull().WithMessage("نام هتل را وارد کنید");
            //RuleFor(p => p.AirlineTitle).NotNull().WithMessage("نام ایرلاین وارد کنید");
            //RuleFor(p => p.DestinationCountry).NotNull().WithMessage("کشور مقصد وارد کنید");
        }
    }
}

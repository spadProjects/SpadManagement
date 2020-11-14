using FluentValidation;
using SpadManagement.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpadManagement.DataAccess.Validation
{
    public class WebsiteContractItemValidator : AbstractValidator<WebsiteContractItem>
    {
        private static WebsiteContractItemValidator _instance;
        public static WebsiteContractItemValidator GetInstance()
        {
            if (_instance == null)
            {
                _instance = new WebsiteContractItemValidator();
            }

            return _instance;
        }

        public WebsiteContractItemValidator()
        {
            RuleFor(p => p.Amount).NotNull().WithMessage("قیمت را وارد کنید")
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

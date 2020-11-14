﻿using FluentValidation;
using SpadManagement.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpadManagement.DataAccess.Validation
{
    public class WebsiteContractValidator : AbstractValidator<WebsiteContract>
    {
        private static WebsiteContractValidator _instance;
        public static WebsiteContractValidator GetInstance()
        {
            if (_instance == null)
            {
                _instance = new WebsiteContractValidator();
            }

            return _instance;
        }

        public WebsiteContractValidator()
        {
            //RuleFor(p => p.PassengerInsurancePrice).NotNull().WithMessage("حق بیمه مسافرتی را وارد کنید")
            //    .GreaterThanOrEqualTo(0).WithMessage("حق بیمه مسافرتی نمیتواند منفی باشد");

            //RuleFor(p => p.PassengerInsurancePrice).NotNull().WithMessage("نوع خدمات را وارد کنید");
            RuleFor(p => p.ContractDate).NotNull().WithMessage("تاریخ قرارداد را وارد کنید");
            //RuleFor(p => p.FromDate).NotNull().WithMessage("تاریخ رفت را وارد کنید");
            //RuleFor(p => p.ToDate).NotNull().WithMessage("تاریخ برگشت را وارد کنید");
            //RuleFor(p => p.PassengerMobile).NotNull().WithMessage("تلفن همراه مسافر وارد کنید");
            RuleFor(p => p.ContractContext).NotNull().WithMessage("متن قرارداد را وارد کنید");
        }
    }
}
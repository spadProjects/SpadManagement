﻿using FluentValidation;
using SpadManagement.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpadManagement.DataAccess.Validation
{
    public class AccountValidator : AbstractValidator<Account>
    {
        private static AccountValidator _instance;
        public static AccountValidator GetInstance()
        {
            if (_instance == null)
            {
                _instance = new AccountValidator();
            }

            return _instance;
        }

        public AccountValidator()
        {
            
        }
    }
}

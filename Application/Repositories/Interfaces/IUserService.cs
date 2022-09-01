﻿
using Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repository.Interfaces
{
    public interface IUserService
    {
        List<UserFullNameViewModel> GetUserForSearchInAutoComplete(string trem);
    }
}

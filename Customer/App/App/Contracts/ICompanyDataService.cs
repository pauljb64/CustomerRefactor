﻿using App.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Contracts
{
    public  interface ICompanyDataService
    {
        Company GetById(int id);
    }
}

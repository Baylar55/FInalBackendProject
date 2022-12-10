﻿using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Abstract
{
    public interface IWhyChooseRepository:IRepository<WhyChoose>
    {
        Task<WhyChoose> GetAsync();
    }
}

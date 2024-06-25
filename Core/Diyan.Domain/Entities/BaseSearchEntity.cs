﻿using Diyan.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diyan.Persistence.Repositories
{
    public class BaseSearchEntity : BasePaninationEntity
    {
        [DefaultValue("")]
        public string SearchText { get; set; }

        public bool? IsActive { get; set; }
    }
}

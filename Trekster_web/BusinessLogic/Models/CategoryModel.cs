﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Models
{
    public class CategoryModel : BaseModel
    {
        public string Name { get; set; }

        public int Type { get; set; }
    }
}

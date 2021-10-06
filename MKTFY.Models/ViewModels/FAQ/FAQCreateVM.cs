﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKTFY.Models.ViewModels.FAQ
{
    public class FAQCreateVM
    {
        [Required]
        public string Question { get; set; }

        [Required]
        public string Answer { get; set; }

    }
}

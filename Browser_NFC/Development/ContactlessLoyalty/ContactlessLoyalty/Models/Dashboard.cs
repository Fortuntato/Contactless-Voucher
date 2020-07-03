
﻿using ContactlessLoyalty.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ContactlessLoyalty.Data
{
    public class Dashboard
    {
        public int Id { get; set; }
        public string Title { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime LastStampDateTime { get; set; }
        public string NumberOfStamps { get; set; }
        public decimal Price { get; set; }

    }
}

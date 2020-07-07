
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

        [MaxLength(5)]
        public int NumberOfVouchers { get; set; }

        [MaxLength(30)]
        [DataType(DataType.DateTime)]
        public DateTime LastStampDateTime { get; set; }

        [MaxLength(5)]
        public int NumberOfStamps { get; set; }

        [MaxLength(50)]
        public string StoreName { get; set; }


        [MaxLength(10)]
        public string StoreSchemeCode { get; set; }

        public AccountContactlessLoyaltyUser User { get; set; }
    }
}

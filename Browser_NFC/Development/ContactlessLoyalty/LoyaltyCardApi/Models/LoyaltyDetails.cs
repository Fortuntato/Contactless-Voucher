using ContactlessLoyalty.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LoyaltyCardApi.Models
{
    public class LoyaltyDetails
    {
        public int Id { get; set; }
        public string NumberOfVouchers { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime LastStampDateTime { get; set; }
        public int NumberOfStamps { get; set; }
        public string StoreName { get; set; }

        public AccountContactlessLoyaltyUser User { get; set; }
    }
}

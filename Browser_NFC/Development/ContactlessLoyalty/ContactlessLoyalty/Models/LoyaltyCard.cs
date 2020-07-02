using ContactlessLoyalty.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ContactlessLoyalty.Data
{
    public class LoyaltyCard
    {
        public int CardID { get; set; }

        public int UserID { get; set; }

        public int TotalNumVoucherCollected { get; set; }

        public int NumOfStamps { get; set; }
        
        [DataType(DataType.DateTime)]
        public DateTime LastStampDateTime { get; set; }
    }
}

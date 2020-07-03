using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ContactlessLoyaltyWebApp.Models
{
    public class LoyaltyCardModel
    {
        [Key]
        public int ID { get; set; }

        [MaxLength(50)]
        [DataType(DataType.DateTime)]
        public DateTime LastStampReceived { get; set; }
        
        [MaxLength(10)]
        public int VoucherReceived{ get; set; }

        [MaxLength(10)]
        public int StampsCollected { get; set; }

        public UserModel User { get; set; }
    }
}

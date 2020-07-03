using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactlessLoyaltyWebApp.Models
{
    public class LoyaltyCardModel
    {
        public int ID { get; set; }

        public DateTime LastStampReceived { get; set; }
        public int VoucherReceived{ get; set; }

        public int StampsCollected { get; set; }

        public List<UserModel> User { get; set; }
    }
}

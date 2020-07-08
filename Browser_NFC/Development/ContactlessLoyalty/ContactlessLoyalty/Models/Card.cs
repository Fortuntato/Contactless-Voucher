// <copyright company="University Of Westminster">
//     Contactless Loyalty All rights reserved.
// </copyright>
// <author>Shouyi Cui</author>

using System;
using System.ComponentModel.DataAnnotations;

namespace ContactlessLoyalty.Data
{
    public class Card
    {
        public Card()
        {
        }

        public Card(int numberOfVouchers, DateTime lastStampDateTime, int numberOfStamps, string storeName, string storeSchemeCode, AccountContactlessLoyaltyUser user)
        {
            NumberOfVouchers = numberOfVouchers;
            LastStampDateTime = lastStampDateTime;
            NumberOfStamps = numberOfStamps;
            StoreName = storeName;
            StoreSchemeCode = storeSchemeCode;
            User = user;
        }

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

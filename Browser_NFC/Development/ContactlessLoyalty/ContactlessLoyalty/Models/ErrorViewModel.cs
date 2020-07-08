// <copyright company="University Of Westminster">
//     Contactless Loyalty All rights reserved.
// </copyright>
// <author>Shouyi Cui</author>

namespace ContactlessLoyalty.Data
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
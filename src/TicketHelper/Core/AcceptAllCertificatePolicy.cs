using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace TicketHelper
{
    public class AcceptAllCertificatePolicy 
        : ICertificatePolicy
    {
        public bool CheckValidationResult(ServicePoint srvPoint, System.Security.Cryptography.X509Certificates.X509Certificate certificate, WebRequest request, int certificateProblem)
        {
            return true;
        }
    }
}

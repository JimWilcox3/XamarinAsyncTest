using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Text;

namespace Xamarin_Async_Test
{
    class DataModel
    {
        public static BasicHttpBinding CreateBasicHttp()
        {

            BasicHttpBinding binding = new BasicHttpBinding()
            {
                Name = "basicHttpBinding",
                MaxBufferSize = 2048000,
                MaxReceivedMessageSize = 2048000
            };
            TimeSpan timeout = new TimeSpan(0, 0, 30);
            binding.SendTimeout = timeout;
            binding.OpenTimeout = timeout;
            binding.ReceiveTimeout = timeout;
            //binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Windows;

            return binding;
        }


    }
}

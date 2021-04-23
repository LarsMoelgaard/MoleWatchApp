using System;
using System.Collections.Generic;
using System.Text;
using APIWebServiesConnector;

namespace MoleWatchApp.Models
{
    public static class APIFactory
    {
        private static IAPIService _api;

        private static IAPIService api
        {
            get
            {
                if (_api == null)
                {
                    // _api = new ApiService(APIWebServiesConnector.APIStringFabrics.APIStringFabric.GetDeveloperAPIString(), 'm');
                    // Logintype == mobil
                    // For at skifte API ændre getDevString til getProductionString

                    _api = new StubApiService(); //Skal slettes til produktion
                }

                return _api;
            }
        }

        public static IAPIService GetAPI()
        {
            return api;
        }



    }
}

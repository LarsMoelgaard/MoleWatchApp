using System;
using System.Collections.Generic;
using System.Text;
using APIWebServiesConnector;

namespace MoleWatchApp.Models
{
    public static class APISingleton
    {
        private static IAPIService _api;

        private static IAPIService api
        {
            get
            {
                if (_api == null)
                {
                    _api = new ApiService("https://moletrackerapiv2.azurewebsites.net/",'W');
                    //_api = new ApiService(APIWebServiesConnector.APIStringFabrics.APIStringFabric.GetDeveloperAPIString(),'W');
                    // Logintype == mobil
                    // For at skifte API ændre getDevString til getProductionString

                    // _api = new StubApiService(); //Skal slettes til produktion


                    //

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

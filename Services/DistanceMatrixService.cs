using PawMates.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PawMates.Services
{
    public class DistanceMatrixService
    {
        public DistanceMatrixService()
        {

        }
        public string GetDistanceMatrixURL(Owner originOwner, Owner destinationOwner)
        {
            return $"https://maps.googleapis.com/maps/api/distancematrix/json?units=imperial&origins={originOwner.OwnerLatitude},{originOwner.OwnerLongitude}&destinations=40.6905615%2C-73.9976592%7C40.6905615%2C-73.9976592%7C40.6905615%2C-73.9976592%7C40.6905615%2C-73.9976592%7C40.6905615%2C-73.9976592%7C40.6905615%2C-73.9976592%7C40.659569%2C-73.933783%7C40.729029%2C-73.851524%7C40.6860072%2C-73.6334271%7C40.598566%2C-73.7527626%7C40.659569%2C-73.933783%7C40.729029%2C-73.851524%7C40.6860072%2C-73.6334271%7C40.598566%2C-73.7527626&key=" + APIKeys.GOOGLE_API_KEY;

        }

    }
}

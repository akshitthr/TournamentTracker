using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Models
{
    public class PrizeModel
    {
        public int Id { get; set; }
        public int PlaceNumber { get; set; }
        public string PlaceName { get; set; }
        public decimal PrizeAmount { get; set; }
        public decimal PrizePercentage { get; set; }
        public PrizeModel(string placeNumber, string placeName, string prizeAmount, string prizePercentage)
        {
            PlaceNumber = int.Parse(placeNumber);
            PlaceName = placeName;
            PrizeAmount = decimal.Parse(prizeAmount);
            PrizePercentage = decimal.Parse(prizePercentage);
        }

        public PrizeModel()
        {

        }
    }
}

using Microsoft.AspNetCore.Mvc.Rendering;

namespace VehicleFinder.Models
{
    public class HomeModels
    {

        public class IndexModels
        {
            public List <SelectListItem> CarMakesList { get; set; }
            public string manufacture_year { get; set; }

        }
        // Get All Makes: returns json with this structure 
        public class Make
        {
            public int MakeId { get; set; }
            public string MakeName { get; set; }
        }
        //Get Vehicle Types for Make by Id:
        public class VehicleType
        {
            public int VehicleTypeId { get; set; }
            public string VehicleTypeName { get; set; }
        }
        //Get Vehicle Types for Make by Id:
        public class VechicleModel
        {
            public int Make_ID { get; set; }
            public string Make_Name { get; set; }
            public int Model_ID { get; set; }
            public string Model_Name { get; set; }
        }

    }
}

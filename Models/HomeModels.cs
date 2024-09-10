using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace VehicleFinder.Models
{
    public class HomeModels
    {

        public class IndexModels
        {
            public List<SelectListItem> CarMakesList { get; set; }
            public string manufacture_year { get; set; }

        }
        public class MakeResult
        {
            public int Count { get; set; }
            public string Message { get; set; }
            public string SearchCriteria { get; set; }
             public List<Make> Results { get; set; }
        }
            // Get All Makes: returns json with this structure 
            public class Make
            {
                public int Make_ID { get; set; }
                public string Make_Name { get; set; }
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
 

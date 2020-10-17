using System.Collections.Generic;

namespace WebAPI.Models.DTO._Treatment
{
    public class TreatmentReq
    {
        public string GroupName { get; set; }
        public List<string> List { get; set; }
    }
}
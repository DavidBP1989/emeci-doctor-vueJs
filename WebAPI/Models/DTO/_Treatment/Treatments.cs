using System.Collections.Generic;

namespace WebAPI.Models.DTO._Treatment
{
    public class Treatments
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
        /// <summary>
        /// list of treatments
        /// </summary>
        public List<string> List { get; set; }
    }
}
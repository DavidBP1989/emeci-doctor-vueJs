using System.Collections.Generic;

namespace WebAPI.Models.DTO._Studies
{
    public class CategoryStudies
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public List<Studies> StudiesList { get; set; }

        public class Studies
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}
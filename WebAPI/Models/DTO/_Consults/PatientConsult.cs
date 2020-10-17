namespace WebAPI.Models.DTO._Consults
{
    public class PatientConsult
    {
        public int? PatientId { get; set; }
        public string Allergies { get; set; }
        public string Reserved { get; set; }
        public string RelevantPathologies { get; set; }
    }
}
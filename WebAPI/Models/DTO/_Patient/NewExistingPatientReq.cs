namespace WebAPI.Models.DTO._Patient
{
    /// <summary>
    /// request to add existing patient 
    /// to another doctor
    /// </summary>
    public class NewExistingPatientReq
    {
        public string Emeci { get; set; }
        public string Coordinate { get; set; }
        public string Value { get; set; }
    }
}
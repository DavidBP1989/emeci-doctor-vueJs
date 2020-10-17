using System.Collections.Generic;

namespace WebAPI.Models.DTO._Diagnostic
{
    public class DiagnosticsReq
    {
        public string GroupName { get; set; }
        public List<string> List { get; set; }
    }
}
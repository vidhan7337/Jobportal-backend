using System.Collections.Generic;

namespace Employer.WEBAPI.Models
{
    public class ResponseModel
    {
        public List<VacancyDetails> VacancyDetails { get; set; }
        public int pageIndex { get; set; }
        public int totalPage { get; set; }
        public int totalItems { get; set; }
    }
}

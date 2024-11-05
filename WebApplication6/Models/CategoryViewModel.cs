using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MesWeb.Models.ViewModel
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        public int? Stt { get; set; }
        public string CategoryCode { get; set; }
        public string CategoryName { get; set; }
        public int? ParentId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public int Level { get; set; }
        public string TotalWidth { get; set; }
        public int Col { get; set; }
        public string Link { get; set; }
        public string Class { get; set; }
    }
}

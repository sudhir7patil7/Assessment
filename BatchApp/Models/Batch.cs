using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BatchAPI.Models
{
    public class Batch    {
       [Key]
       [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid? batchID { get; set; }
        //[Required]
        public string businessUnit { get; set; }
        public string status { get; set; }
        [Required, DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy HH:mm:ss}")]
        public DateTime batchPublishDate { get; set; }
        [Required, DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy HH:mm:ss}")]
        public DateTime requiredDate { get; set; }
        [Required]
        [StringLength(10, ErrorMessage = "Can contain only 10 Characters")]
        public string fileName { get; set; }

    }
}

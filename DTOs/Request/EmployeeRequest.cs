using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Test_Dev.DTOs.Request
{
    public class EmployeeRequest
    {
        public int EmployeeId { get; set; }

        [Required]
        [MaxLength(150, ErrorMessage = "สามารถระบุได้สูงสุด 150 ตัวอักษร")]
        public string Fullname { get; set; } = null!;

        [MaxLength(10, ErrorMessage = "ระบุได้สูงสุด 10 ตัว")]
        public string TelNo { get; set; } = null!;

        public int PositionId { get; set; }
    }
}
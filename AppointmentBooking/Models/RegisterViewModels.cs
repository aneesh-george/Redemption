using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppointmentBooking.Models
{
    public class RegisterViewModels
    {
    }

    public class Patient
    {
        [Required]
        public int phoneNumber { get; set; }
    }
}
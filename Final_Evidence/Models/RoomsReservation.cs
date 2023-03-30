using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Final_Evidence.Models
{
    public class RoomsReservation
    {
        [Required(ErrorMessage = "Please enter Room NO")]
        public string RoomNo { get; set; }
        [Required(ErrorMessage = "Please enter Floor NO")]
        public string FloorNo { get; set; }
        public string Description { get; set; }
        public double Rate { get; set; }
        public bool Active { get; set; }
        public string Image { get; set; }
        [Required(ErrorMessage = "Please enter ReserveId")]
        public string ReserveId { get; set; }
        public string GuestName { get; set; }
        public int Days { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
       
       
    }
}
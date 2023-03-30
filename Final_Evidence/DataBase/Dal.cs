using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Final_Evidence.DataBase
{
    public class Rooms
    {
        [Key]
        public string RoomNo { get; set; }
        public string FloorNo { get; set; }
        public string Description { get; set; }
        public double Rate { get; set; }
        public bool Active { get; set; }
        public string Image { get; set; }
        public virtual ICollection<Reservation> Roomlink { get; set; }

    }
    public class Reservation
    {
        [Key]
        public string ReserveId { get; set; }
        [ForeignKey("Roomlink")]
        public string RoomNo { get; set; }
        public string GuestName { get; set; }
        public int Days { get; set; }
        public DateTime Date { get; set; }
        public virtual Rooms Roomlink { get; set; }
    }
   
}
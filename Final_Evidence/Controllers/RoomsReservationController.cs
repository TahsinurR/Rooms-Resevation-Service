using CrystalDecisions.CrystalReports.Engine;
using Final_Evidence.Data;
using Final_Evidence.DataBase;
using Final_Evidence.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Final_Evidence.Controllers
{
    public class RoomsReservationController : Controller
    {
        private DBContext db = new DBContext();
        List<Rooms> mst = new List<Rooms>();
        List<Reservation> sdt = new List<Reservation>();
        public ActionResult Index(string mode, string RoomNo = "")
        {

            if (RoomNo == "")
            {
                //create work
                Session["sdt"] = TempData["sdt"];
                ViewBag.records = Session["sdt"];
                TempData["sdt"] = Session["sdt"];
                return View();
            }
            else
            {
                //edit work
                if (mode == null)
                {
                    //first time
                    Session["sdt"] = (from d in db.Reservation where d.RoomNo == RoomNo select d).ToList();
                    TempData["sdt"] = Session["sdt"];
                }
                else//after click add
                    Session["sdt"] = TempData["sdt"];
                ViewBag.records = Session["sdt"];
                TempData["sdt"] = Session["sdt"];
                Rooms m2 = db.Rooms.Find(RoomNo);
                RoomsReservation md = new RoomsReservation() { RoomNo = m2.RoomNo, FloorNo = m2.FloorNo, Description = m2.Description, Rate = m2.Rate, Active = m2.Active, Image = m2.Image };
                return View(md);
            }
        }
        public void AddLine(RoomsReservation d)
        {

            sdt = TempData["sdt"] as List<Reservation>;
            if (sdt == null)
                sdt = new List<Reservation>();
            sdt.Add(new Reservation() { ReserveId = d.ReserveId, GuestName = d.GuestName, Days = d.Days, Date = d.Date });
            TempData["sdt"] = sdt;

            Session["sdt"] = TempData["sdt"];
            ViewBag.records = Session["sdt"];
            TempData["sdt"] = Session["sdt"];
        }
        public void SaveMe(RoomsReservation m2)
        {
            DeleteMe(m2.RoomNo);
            Rooms md = new Rooms() { RoomNo = m2.RoomNo, FloorNo = m2.FloorNo, Description = m2.Description, Rate = m2.Rate, Active = m2.Active, Image = m2.Image };
            db.Rooms.Add(md);
            db.SaveChanges();
            sdt = TempData["sdt"] as List<Reservation>;
            foreach (Reservation d in sdt)
            {
                Reservation r = new Reservation() { ReserveId = d.ReserveId, RoomNo = m2.RoomNo, GuestName = d.GuestName, Days = d.Days, Date = d.Date };
                db.Reservation.Add(r);
                db.SaveChanges();
            }

            TempData["sdt"] = "";
            Session["sdt"] = "";

        }
        public void DeleteMe(string RoomNo)
        {
            db.Database.ExecuteSqlCommand($"delete Reservation where RoomNo='{RoomNo}'");
            db.Database.ExecuteSqlCommand($"delete Rooms where RoomNo='{RoomNo}'");
            db.SaveChanges();
        }
        [HttpPost]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]//to clear the cache
        public ActionResult index(RoomsReservation m2, string ButtonType)
        {
            if (ButtonType == "Add")
            {
                AddLine(m2);
                ModelState.Clear();
                return PartialView("_PartialPage1");
            }
            if (ButtonType == "Save")
            {
                SaveMe(m2);
                return Json(new
                {
                    url = Url.Action("list")
                });
            }
            if (ButtonType == "Delete")
            {
                DeleteMe(m2.RoomNo);
                return Json(new
                {
                    url = Url.Action("list")
                });
            }
            return View();
        }
        public ActionResult List()
        {
            TempData["sdt"] = "";
            List<RoomsReservation> c = new List<RoomsReservation>();
            var a = db.Rooms.ToList();
            foreach (var m2 in a)
            {
                c.Add(new RoomsReservation { RoomNo = m2.RoomNo, FloorNo = m2.FloorNo, Description = m2.Description, Rate = m2.Rate, Active = m2.Active, Image = m2.Image });

            }
            return View(c);
        }

        public ActionResult Download_PDF()
        {
            List<Reservation> allCustomer = new List<Reservation>();
            allCustomer = db.Reservation.ToList();


            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Report"), "CrystalReport1.rpt"));

            rd.SetDataSource(allCustomer);

            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();


            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "CrystalReport.pdf");
        }

    }
   

}

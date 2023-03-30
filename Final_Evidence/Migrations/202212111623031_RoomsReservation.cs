namespace Final_Evidence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RoomsReservation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reservation",
                c => new
                    {
                        ReserveId = c.String(nullable: false, maxLength: 128),
                        RoomNo = c.String(maxLength: 128),
                        GuestName = c.String(),
                        Days = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ReserveId)
                .ForeignKey("dbo.Rooms", t => t.RoomNo)
                .Index(t => t.RoomNo);
            
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        RoomNo = c.String(nullable: false, maxLength: 128),
                        FloorNo = c.String(),
                        Description = c.String(),
                        Rate = c.Double(nullable: false),
                        Active = c.Boolean(nullable: false),
                        Image = c.String(),
                    })
                .PrimaryKey(t => t.RoomNo);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservation", "RoomNo", "dbo.Rooms");
            DropIndex("dbo.Reservation", new[] { "RoomNo" });
            DropTable("dbo.Rooms");
            DropTable("dbo.Reservation");
        }
    }
}

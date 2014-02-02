using System.Data;

namespace PracticeTime.Web.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class C_Badge : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BadgeAwards",
                c => new
                    {
                        BadgeAwardId = c.Int(nullable: false, identity: true),
                        AwardDate = c.DateTime(nullable: false),
                        Badge_C_BadgeId = c.Int(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.BadgeAwardId)
                .ForeignKey("dbo.C_Badge", t => t.Badge_C_BadgeId)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.Badge_C_BadgeId)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.C_Badge",
                c => new
                    {
                        C_BadgeId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable:false,maxLength:255),
                        Description = c.String(),
                        ImageUrl = c.String(maxLength:255),
                    })
                .PrimaryKey(t => t.C_BadgeId)
                .Index(t => t.Name,unique:true);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UserName = c.String(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        User_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.LoginProvider, t.ProviderKey })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sessions",
                c => new
                    {
                        SessionId = c.Int(nullable: false, identity: true),
                        Time = c.Int(nullable: false),
                        Title = c.String(),
                        SessionDateTimeUtc = c.DateTime(nullable: false),
                        TimeZoneOffset = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.SessionId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);

            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sessions", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.BadgeAwards", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.BadgeAwards", "Badge_C_BadgeId", "dbo.C_Badge");
            DropIndex("dbo.Sessions", new[] { "UserId" });
            DropIndex("dbo.BadgeAwards", new[] { "User_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "User_Id" });
            DropIndex("dbo.BadgeAwards", new[] { "Badge_C_BadgeId" });
            DropTable("dbo.Sessions");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.C_Badge");
            DropTable("dbo.BadgeAwards");
        }
    }
}

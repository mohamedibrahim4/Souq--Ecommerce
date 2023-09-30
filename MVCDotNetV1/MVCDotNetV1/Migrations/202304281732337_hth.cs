namespace MVCDotNetV1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hth : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CartAdmins",
                c => new
                    {
                        CartAdminID = c.Int(nullable: false, identity: true),
                        BuyerID = c.String(),
                        BuyerName = c.String(),
                        CartContent = c.String(),
                        AddTime = c.DateTime(),
                        IsConfirmed = c.Boolean(nullable: false),
                        IsActionTaken = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CartAdminID);
            
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        CartID = c.String(nullable: false, maxLength: 128),
                        CartContant = c.String(),
                    })
                .PrimaryKey(t => t.CartID)
                .ForeignKey("dbo.AspNetUsers", t => t.CartID)
                .Index(t => t.CartID);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Gender = c.Byte(),
                        BirthDate = c.DateTime(),
                        Address_Country = c.String(nullable: false),
                        Address_City = c.String(nullable: false),
                        Address_Street = c.String(nullable: false),
                        Address_BuildingNumber = c.String(nullable: false),
                        CustomerVISA = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.CustomerWishLists",
                c => new
                    {
                        id = c.String(nullable: false, maxLength: 128),
                        ProductID = c.Int(nullable: false),
                        DateOfAdding = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.id, t.ProductID })
                .ForeignKey("dbo.AspNetUsers", t => t.id, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.id)
                .Index(t => t.ProductID);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductID = c.Int(nullable: false, identity: true),
                        ProductName = c.String(),
                        ImagePath = c.String(),
                        Description = c.String(),
                        Price = c.Double(nullable: false),
                        Discount = c.Double(nullable: false),
                        MadeIn = c.String(),
                        SupplierName = c.String(),
                        Weight = c.Double(nullable: false),
                        AvailableQuantity = c.Int(nullable: false),
                        subcatId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProductID)
                .ForeignKey("dbo.subcats", t => t.subcatId, cascadeDelete: true)
                .Index(t => t.subcatId);
            
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        OrderDetailID = c.Int(nullable: false),
                        OrderQuantity = c.Int(nullable: false),
                        ShippingType = c.String(),
                        order_OrderID = c.Int(),
                    })
                .PrimaryKey(t => t.OrderDetailID)
                .ForeignKey("dbo.Orders", t => t.order_OrderID)
                .ForeignKey("dbo.Products", t => t.OrderDetailID)
                .Index(t => t.OrderDetailID)
                .Index(t => t.order_OrderID);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderID = c.Int(nullable: false, identity: true),
                        OrderDate = c.DateTime(nullable: false),
                        OrderStatus = c.Byte(nullable: false),
                        id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.OrderID)
                .ForeignKey("dbo.AspNetUsers", t => t.id, cascadeDelete: true)
                .Index(t => t.id);
            
            CreateTable(
                "dbo.subcats",
                c => new
                    {
                        subcatId = c.Int(nullable: false, identity: true),
                        subcatName = c.String(),
                        maincatID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.subcatId)
                .ForeignKey("dbo.maincats", t => t.maincatID, cascadeDelete: true)
                .Index(t => t.maincatID);
            
            CreateTable(
                "dbo.maincats",
                c => new
                    {
                        maincatID = c.Int(nullable: false, identity: true),
                        maincatName = c.String(),
                    })
                .PrimaryKey(t => t.maincatID);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.CustomerPhones",
                c => new
                    {
                        PhoneNumber = c.String(nullable: false, maxLength: 128),
                        id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.PhoneNumber, t.id })
                .ForeignKey("dbo.AspNetUsers", t => t.id, cascadeDelete: true)
                .Index(t => t.id);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.offers",
                c => new
                    {
                        ProductID = c.Int(nullable: false, identity: true),
                        ProductName = c.String(),
                        ImagePath = c.String(),
                        Description = c.String(),
                        Price = c.Double(nullable: false),
                        Discount = c.Int(nullable: false),
                        MadeIn = c.String(),
                        SupplierName = c.String(),
                        Weight = c.Double(nullable: false),
                        AvailableQuantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProductID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Carts", "CartID", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.CustomerPhones", "id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Products", "subcatId", "dbo.subcats");
            DropForeignKey("dbo.subcats", "maincatID", "dbo.maincats");
            DropForeignKey("dbo.OrderDetails", "OrderDetailID", "dbo.Products");
            DropForeignKey("dbo.OrderDetails", "order_OrderID", "dbo.Orders");
            DropForeignKey("dbo.Orders", "id", "dbo.AspNetUsers");
            DropForeignKey("dbo.CustomerWishLists", "ProductID", "dbo.Products");
            DropForeignKey("dbo.CustomerWishLists", "id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.CustomerPhones", new[] { "id" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.subcats", new[] { "maincatID" });
            DropIndex("dbo.Orders", new[] { "id" });
            DropIndex("dbo.OrderDetails", new[] { "order_OrderID" });
            DropIndex("dbo.OrderDetails", new[] { "OrderDetailID" });
            DropIndex("dbo.Products", new[] { "subcatId" });
            DropIndex("dbo.CustomerWishLists", new[] { "ProductID" });
            DropIndex("dbo.CustomerWishLists", new[] { "id" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Carts", new[] { "CartID" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.offers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.CustomerPhones");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.maincats");
            DropTable("dbo.subcats");
            DropTable("dbo.Orders");
            DropTable("dbo.OrderDetails");
            DropTable("dbo.Products");
            DropTable("dbo.CustomerWishLists");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Carts");
            DropTable("dbo.CartAdmins");
        }
    }
}

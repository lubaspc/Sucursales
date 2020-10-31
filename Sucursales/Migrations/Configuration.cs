namespace Sucursales.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Sucursales.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Sucursales.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Sucursales.Models.ApplicationDbContext";
        }

        protected override void Seed(Sucursales.Models.ApplicationDbContext context)
        {
            //CreateUser(context);
            //var branches = CreateSucursales(context);
            //CreateProduct(context,branches);
        }

        private void CreateProduct(ApplicationDbContext db, List<Branch> branches)
        {
            var cafe = new Product { Name = "Café legal", CodeBar = "100010", Price = 7.0 };
            db.Product.Add(cafe);

            var cholocate = new Product { Name = "Chocolate Abuelita", CodeBar = "100011", Price = 15.0 };
            db.Product.Add(cholocate);

            var bonafina= new Product { Name = "Bonafina", CodeBar = "100012", Price = 12.0 };
            db.Product.Add(bonafina);


            var StockACafe = new Stock { Branch = branches[0], BranchId = branches[0].Id, Product = cafe, ProductId = cafe.Id, Amount = 5 };
            db.Stock.AddOrUpdate(StockACafe);

            var StockACafeH = new StockHistory { Stock = StockACafe, StockId = StockACafe.Id, Amounth = StockACafe.Amount, Entity = true, Created = DateTime.Now };
            db.StockHistory.AddOrUpdate(StockACafeH);


            var StockAChocolate = new Stock { Branch = branches[0], BranchId = branches[0].Id, Product = cholocate, ProductId = cholocate.Id, Amount = 6 };
            db.Stock.AddOrUpdate(StockACafe);

            var StockAChocolateH = new StockHistory { Stock = StockAChocolate, StockId = StockAChocolate.Id, Amounth = StockAChocolate.Amount, Entity = true, Created = DateTime.Now };
            db.StockHistory.AddOrUpdate(StockAChocolateH);


            var StockABonafina = new Stock { Branch = branches[0], BranchId = branches[0].Id, Product = bonafina, ProductId = bonafina.Id, Amount = 1 };
            db.Stock.AddOrUpdate(StockACafe);

            var StockABonafinaH = new StockHistory { Stock = StockABonafina, StockId = StockABonafina.Id, Amounth = StockABonafina.Amount, Entity = true, Created = DateTime.Now };
            db.StockHistory.AddOrUpdate(StockABonafinaH);

            var StockBCafe = new Stock { Branch = branches[1], BranchId = branches[1].Id, Product = cafe, ProductId = cafe.Id, Amount = 8 };
            db.Stock.AddOrUpdate(StockACafe);

            var StockBCafeH = new StockHistory { Stock = StockACafe, StockId = StockACafe.Id, Amounth = StockACafe.Amount, Entity = true, Created = DateTime.Now };
            db.StockHistory.AddOrUpdate(StockACafeH);


            var StockBChocolate = new Stock { Branch = branches[1], BranchId = branches[1].Id, Product = cholocate, ProductId = cholocate.Id, Amount = 4 };
            db.Stock.AddOrUpdate(StockACafe);

            var StockBChocolateH = new StockHistory { Stock = StockAChocolate, StockId = StockAChocolate.Id, Amounth = StockAChocolate.Amount, Entity = true, Created = DateTime.Now };
            db.StockHistory.AddOrUpdate(StockAChocolateH);


            var StockBBonafina = new Stock { Branch = branches[1], BranchId = branches[1].Id, Product = bonafina, ProductId = bonafina.Id, Amount = 2 };
            db.Stock.AddOrUpdate(StockACafe);

            var StockBBonafinaH = new StockHistory { Stock = StockABonafina, StockId = StockABonafina.Id, Amounth = StockABonafina.Amount, Entity = true, Created = DateTime.Now };
            db.StockHistory.AddOrUpdate(StockABonafinaH);
        }

        private void CreateUser(ApplicationDbContext db)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));


            roleManager.Create(new IdentityRole("Administrador"));

            var Admin = new ApplicationUser { UserName = "Administrador", Email = "administrador@test.com" };
            userManager.Create(Admin, "prueba");
            userManager.AddToRole(Admin.Id, "Administrador");
        }
        private List<Branch> CreateSucursales(ApplicationDbContext db)
        {
         
            Address Address = new Address { City = "Toluca", Colony = "Toluca", ZipCode = "52149", State = "Mexico", ExtNum = "53", Street = "Av 9" };
            db.Address.AddOrUpdate(Address);

            Branch BranchA = new Branch { Name = "Sucursal A", AddressId = Address.Id ,Address = Address};
            db.Branch.AddOrUpdate(BranchA);


            Branch BranchB = new Branch { Name = "Sucursal B", AddressId = Address.Id, Address = Address };
            db.Branch.AddOrUpdate(BranchB);

            var Branches = new List<Branch>();
            Branches.Add(BranchA);
            Branches.Add(BranchB);
            return Branches;

        }
    }
}

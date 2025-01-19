using FribergCarRental.Models.Entities;

namespace FribergCarRental.data.Seeding
{
    public class ApplicationDbContextSeeder
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            if (!context.Users.Any())
            {
                await SeedUsers(context);
            }

            if (!context.Cars.Any())
            {
                await SeedCars(context);
            }
        }

        private static async Task SeedCars(ApplicationDbContext context)
        {
            context.Set<Car>().Add(new Car
            {
                Model = "Toyota Camry",
                PricePerDay = 499,
                ImageUrl= "https://i.ytimg.com/vi/8e7OnPcb2jo/hqdefault.jpg"
            });

            context.Set<Car>().Add(new Car
            {
                Model = "Opel Tigra",
                PricePerDay = 899,
                ImageUrl = "https://cartuning.ws/uploads/posts/2022-11/thumbs/opel-tigra-rat-1.jpg"
            });
            context.Set<Car>().Add(new Car
            {
                Model = "VW Beetle",
                PricePerDay = 599,
                ImageUrl = "https://c8.alamy.com/comp/A25MDE/rusty-old-vw-beetle-car-A25MDE.jpg"
            });
            context.Set<Car>().Add(new Car
            {
                Model = "VW Squareback",
                PricePerDay = 999,
                ImageUrl = "https://thumbs.dreamstime.com/z/old-rusty-volkswagen-squareback-car-high-resolution-photos-old-rusty-classic-volkswagen-squareback-car-empty-warehouse-168484866.jpg"
            });

            await context.SaveChangesAsync();
        }

        private static async Task SeedUsers(ApplicationDbContext context)
        {
            context.Set<User>().Add(new User
            {
                Username = "admin",
                Email = "Admin@admin.com",
                Password = "admin",
                Role = "Admin",
                Contact = new Contact
                {
                    FirstName = "Admin",
                    LastName = "Admin",
                    Address = "Admin",
                    City = "Admin",
                    PostalCode = "127001",
                    Phone = "127001"
                }
            });

            context.Set<User>().Add(new User
            {
                Username = "customer",
                Email = "customer@customer.com",
                Password = "customer",
                Contact = new Contact
                {
                    FirstName = "customer",
                    LastName = "customer",
                    Address = "customer",
                    City = "customer",
                    PostalCode = "127001",
                    Phone = "127001"
                }
            });

            await context.SaveChangesAsync();
        }
    }
}

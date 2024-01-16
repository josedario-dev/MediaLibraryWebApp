using MediaLibrary.WebApp.Core.Entities;
using MediaLibrary.WebApp.Core.Enums;
using MediaLibrary.WebApp.Server.Helpers.Contracts;

namespace MediaLibrary.WebApp.Server.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public SeedDb(DataContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckRolesAsync();
            await CheckCountriesAsync();

            await CheckUserAsync("Dario", "dario@gmail.com", "https://medioteca.blob.core.windows.net/users/2a88b2d8-1dbc-4b83-96da-017231a60ba6.jpg", UserType.Admin);
        }

        private async Task CheckCountriesAsync()
        {
            if (!_context.Country.Any())
            {
                _context.Country.Add(new Country { Name = "España" });
                _context.Country.Add(new Country { Name = "Colombia" });
                _context.Country.Add(new Country { Name = "Puerto Rico" });
                _context.Country.Add(new Country { Name = "Estados Unidos" });
                _context.Country.Add(new Country { Name = "Suiza" });
                _context.Country.Add(new Country { Name = "Francia" });
                _context.Country.Add(new Country { Name = "Noruega" });
            }

            await _context.SaveChangesAsync();
        }

        private async Task<User> CheckUserAsync(string firstName, string email, string photo, UserType userType)
        {
            var user = await _userHelper.GetUserAsync(email);
            if (user == null)
            {
                user = new User
                {
                    FirstName = firstName,
                    Email = email,
                    UserName = email,
                    Country = _context.Country.FirstOrDefault(),
                    Photo = photo,
                    UserType = userType,
                };

                var result = await _userHelper.AddUserAsync(user, "123456");
                await _userHelper.AddUserToRoleAsync(user, userType.ToString());

                //var contributor = new Contributor 
                //{ 
                //    AccountId = user.Id,
                //    NickName = user.FirstName,
                //    RegisteredAt = DateTime.Now,
                //};
                //_context.Contributor.Add(contributor);

                //await _context.SaveChangesAsync();
            }

            return user;
        }

        private async Task CheckRolesAsync()
        {
            await _userHelper.CheckRoleAsync(UserType.Admin.ToString());
            await _userHelper.CheckRoleAsync(UserType.User.ToString());
        }

    }


}

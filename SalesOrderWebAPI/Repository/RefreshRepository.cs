
using Microsoft.EntityFrameworkCore;
using SalesOrderWebAPI.Data.DataContext;
using SalesOrderWebAPI.IRepository;
using SalesOrderWebAPI.Model;
using System.Security.Cryptography;

namespace SalesOrderWebAPI.Repository
{
    public class RefreshRepository : IRefreshRepository
    {
        private readonly SalesOrderDbContext _context;

        public RefreshRepository(SalesOrderDbContext context)
        {
            _context = context;
        }
        public async Task<string> GenerateToken(string username)
        {
            var randomnumber = new byte[32];
            using (var randomnumbergenerator = RandomNumberGenerator.Create())
            {
                randomnumbergenerator.GetBytes(randomnumber);
                string refreshtoken = Convert.ToBase64String(randomnumber);
                var Existtoken = _context.TblRefreshtokens.FirstOrDefaultAsync(item => item.UserId == username).Result;
                if (Existtoken != null)
                {
                    Existtoken.RefreshToken = refreshtoken;
                }
                else
                {
                    await _context.TblRefreshtokens.AddAsync(new TblRefreshtoken
                    {
                        UserId = username,
                        TokenId = new Random().Next().ToString(),
                        RefreshToken = refreshtoken
                    });
                }
                await _context.SaveChangesAsync();

                return refreshtoken;

            }
        }
    }
}
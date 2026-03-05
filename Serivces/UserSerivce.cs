// UserService.cs
using System.Threading.Tasks;
using blogapi.Models;
using blogapi.Models.DTO;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using blogapi.Serivces.Context;
using Microsoft.Identity.Client;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace blogapi.Serivces
{
    public class UserService : ControllerBase
    {
        private readonly DataContext _context;
        private bool newHashPassword;
        private string? password;
        private SecurityToken tokenOptions;

        public string StoredSalt { get; private set; }

        public UserService(DataContext dataContext)
        {
            _context = dataContext;
        }

        public bool DoesUserExist(string username)
        {
            return _context.UserInfo.SingleOrDefault(user => user.Username == username) != null;
        }

        public bool AddUser(CreateAccountDTO userToAdd)
        {
            bool result = false;

            if (userToAdd.Username != null && !DoesUserExist(userToAdd.Username))
            {
                UserModel newUser = new UserModel();

                var hashPassword = HashPassword(userToAdd.Password);

                newUser.Id = userToAdd.Id;
                newUser.Username = userToAdd.Username;

                newUser.Salt = hashPassword.Salt;
                newUser.Hash = hashPassword.Hash;

                _context.Add(newUser);

                result = _context.SaveChanges() != 0;
            }
            return result;
        }

        public PasswordDTO HashPassword(string? password)
        {
            PasswordDTO newhashPassword = new PasswordDTO();

            byte[] SaltBytes = new byte[64];

            var provider = RandomNumberGenerator.Create();
            provider.GetNonZeroBytes(SaltBytes);

            var Salt = Convert.ToBase64String(SaltBytes);

            var rfc298DeriveBytes = new Rfc2898DeriveBytes(password ?? "", SaltBytes, 10000, HashAlgorithmName.SHA256);

            var hash = Convert.ToBase64String(rfc298DeriveBytes.GetBytes(256));

            newhashPassword.Salt = Salt;
            newhashPassword.Hash = hash;
            return newhashPassword;
        }

        public bool verifyUserPassword(string? Password, string? StoreHash, string? StoreSalt)
        {
            if (StoreSalt == null)
            {
                return false;
            }

            var SaltBytes = Convert.FromBase64String(StoreSalt);

            var rfc2898DriveBytes = new Rfc2898DeriveBytes(Password ?? "", SaltBytes, 10000, HashAlgorithmName.SHA256);

            var newHash = Convert.ToBase64String(rfc2898DriveBytes.GetBytes(256));

            return StoreHash == newHash;
        }

        [Authorize]
        public IEnumerable<UserModel> GetAllUser()
        {
            return _context.UserInfo;
        }

        public IActionResult Login(LoginDTO user)
        {
            IActionResult result = Unauthorized();

            if (DoesUserExist(user.Username))
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("supersupersuperduppersecurekey@34456789"));

                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var tokenoptions = new JwtSecurityToken(
                    issuer: "http://localhost:5249/",
                    audience: "http://localhost:5249/",
                    claims: new List<Claim>(),
                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials: signinCredentials
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenoptions);

                result = Ok(new { Token = tokenString });
            }

            return result;
        }

        internal UserIdDTO GetUserDTOUserName(string username)
        {
            throw new NotImplementedException();
        }
    }
}


    internal class SemtricSecurityKey
    {
        private object value;

        public SemtricSecurityKey(object value)
        {
            this.value = value;
        }
        
    }


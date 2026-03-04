// UserService.cs
using System.Threading.Tasks;
using blogapi.Models;
using blogapi.Models.DTO;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using blogapi.Serivces.Context;
using Microsoft.Identity.Client;
using System.Security.Cryptography;

namespace blogapi.Serivces
{
    public class UserService
    {
        private readonly DataContext _context;
        private bool newHashPassword;
        private string? password;

        public string StoredSalt { get; private set; }

        public UserService(DataContext dataContext)
        {
            _context = dataContext;
        }
       // we need help method to check if user exist or not 
      public bool DoesUserExist(string username)
        {
            // Check if user exists in the database
            return _context.UserInfo.SingleOrDefault(user => user.Username == username) != null;
        }
    public bool AddUser(CreateAccountDTO userToAdd)
        {
         bool result = false;

         if(userToAdd.Username != null && !DoesUserExist(userToAdd.Username))
            {
                UserModel newUser = new UserModel();

                var hashPassword = HashPassword(userToAdd.Password);

                newUser.Id = userToAdd.Id;
                newUser. Username = userToAdd.Username;

                newUser.Salt = hashPassword.Salt;
                newUser.Hash = hashPassword.Hash;

                _context.Add(newUser);

               result = _context.SaveChanges() != 0;
            }
            return result;
        }

            //We are going to need hash and salt the password before we save it to the database
            // We will use the built in method to create a hash and salt for the password
            //UserName and password are required fields so we need to check if they are null or empty before we try to add the user to the database
            //salt
            //hash

            //then we add it to ur Database
            //Save our changes
            //Return true if the user was added successfully, false otherwise

            //Fuction tht will hash the password and return the hash and salt as a tuple
            public PasswordDTO HashPassword(string? password)
            {
                PasswordDTO newhashPassword = new PasswordDTO();

                byte[] SaltBytes = new byte[64];

                var provider =  RandomNumberGenerator.Create();
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
                if (StoredSalt == null)
                {
                    return false;
                }

                var SaltBytes = Convert.FromBase64String(StoredSalt);
                
                var rfc2898DriveBytes = new Rfc2898DeriveBytes(password ?? "", SaltBytes, 1000, HashAlgorithmName.SHA256);

                var newHash = Convert.ToBase64String(rfc2898DriveBytes.GetBytes(256));

               return StoreHash == newHash;
            }
        }

    

   
}

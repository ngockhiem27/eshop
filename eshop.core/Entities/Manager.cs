using eshop.core.RequestModels;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Text;

namespace eshop.core.Entities
{
    public class Manager
    {
        public int Id { get; set; }

        public int Role_Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password_Hash { get; set; }

        public DateTime Created_At { get; set; }

        public Manager(int id, int roleId, string firstName, string lastName, string email, string password)
        {
            Id = id;
            Role_Id = roleId;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password_Hash = HashPassword(password);
        }

        public Manager(ManagerRequest managerRequest)
        {
            Id = managerRequest.Id;
            Role_Id = managerRequest.RoleId;
            FirstName = managerRequest.FirstName;
            LastName = managerRequest.LastName;
            Email = managerRequest.Email;
            Password_Hash = HashPassword(managerRequest.Password);
        }

        public string HashPassword(string password)
        {
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: Encoding.ASCII.GetBytes("eshop-secret"),
            prf: KeyDerivationPrf.HMACSHA1,
            iterationCount: 10000,
            numBytesRequested: 256 / 8));
            return hashed;
        }
    }
}

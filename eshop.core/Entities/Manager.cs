using eshop.core.DTO.Request;
using eshop.core.Helper;
using System;

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
            Password_Hash = AuthenticateHelper.HashPassword(password);
        }

        public Manager(ManagerInfoRequest managerRequest)
        {
            Id = managerRequest.Id;
            Role_Id = managerRequest.RoleId;
            FirstName = managerRequest.FirstName;
            LastName = managerRequest.LastName;
            Email = managerRequest.Email;
            Password_Hash = AuthenticateHelper.HashPassword(managerRequest.Password);
        }
    }
}

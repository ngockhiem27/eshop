using System;

namespace eshop.core.Entities
{
    public class Customer
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password_Hash { get; set; }

        public DateTime Created_At { get; set; }
    }
}
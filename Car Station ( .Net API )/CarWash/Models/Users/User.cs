using System;
using Microsoft.AspNetCore.Identity;

namespace CarWash.Models.Users
{
	public class User : IdentityUser<Guid>
    {
            public override Guid Id
            {
                get => base.Id;
                set => base.Id = value;
            }

            public override string UserName
            {
                get => base.Email;
                set => base.Email = value;
            }

            public override string PhoneNumber
            {
                get => base.PhoneNumber;
                set => base.PhoneNumber = value;
            }

            public string Name { get; set; }
            public string MiddleName { get; set; }
            public string FamilyName { get; set; }
            public UserStatus Status { get; set; }
            public DateTimeOffset CreatedDate { get; set; }
            public DateTimeOffset UpdatedDate { get; set; }

        }
    }

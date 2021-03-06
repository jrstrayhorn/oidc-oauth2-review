using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Marvin.IDP.Entities
{
    public class UserLogin : IConcurrencyAware
    {
        [Key]
        public Guid Id { get; set; }

        // name of provider, facebook, google, okta, etc
        [MaxLength(200)]
        [Required]
        public string Provider { get; set; }

        // link from our user to provider
        [MaxLength(200)]
        [Required]
        public string ProviderIdentityKey { get; set; }

        // foreign key
        [Required]
        public Guid UserId { get; set; }

        public User User { get; set; }
        public string ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();
    }
}
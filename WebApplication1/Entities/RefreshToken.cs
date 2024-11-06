using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Registration.Entities
{
    [Owned]
    public class RefreshToken
    {
        [Key]
        public string Token { get; set; }
        public DateTime Expires { get; set; }
        public bool IsExpired => DateTime.UtcNow >= Expires;
        public DateTime Created { get; set; }
        public DateTime? Revoked { get; set; }
        public bool IsActive => Revoked == null && !IsExpired;
    }
}

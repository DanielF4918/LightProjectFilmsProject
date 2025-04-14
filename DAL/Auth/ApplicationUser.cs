using Microsoft.AspNetCore.Identity;
using System;

namespace DAL.Auth
{
    public class ApplicationUser : IdentityUser
    {
        // Información personal
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }

        // Información para el negocio
        public DateTime RegistrationDate { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = true;
        public string ProfilePictureUrl { get; set; }
        public DateTime? LastLoginDate { get; set; }

        // Preferencias del usuario
        public string PreferredLanguage { get; set; }
        public bool ReceiveNotifications { get; set; } = true;

        // Para tu negocio específico (alquiler)
        public int? CustomerRating { get; set; }
        public bool HasVerifiedID { get; set; } = false;
        public int CompletedRentals { get; set; } = 0;
    }
}
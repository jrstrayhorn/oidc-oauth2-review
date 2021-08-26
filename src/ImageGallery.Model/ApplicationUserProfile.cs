using System;

namespace ImageGallery.Model
{
    public class ApplicationUserProfile
    {
        public Guid Id { get; set; }         
        public string Subject { get; set; } 
        public string SubscriptionLevel { get; set; }
    }
}
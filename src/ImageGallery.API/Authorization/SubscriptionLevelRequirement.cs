using Microsoft.AspNetCore.Authorization;

namespace ImageGallery.API.Authorization
{
    public class SubscriptionLevelRequirement : IAuthorizationRequirement
    {
        public string RequiredSubscriptionLevel { get; }
        public SubscriptionLevelRequirement(string requiredSubscriptionLevel)
        {
            RequiredSubscriptionLevel = requiredSubscriptionLevel;

        }
    }
}
using System.Linq;
using System.Threading.Tasks;
using ImageGallery.API.Services;
using Microsoft.AspNetCore.Authorization;

namespace ImageGallery.API.Authorization
{
    public class SubscriptionLevelHandler : AuthorizationHandler<SubscriptionLevelRequirement>
    {
        private readonly IGalleryRepository _galleryRepository;
        public SubscriptionLevelHandler(IGalleryRepository galleryRepository)
        {
            _galleryRepository = galleryRepository;

        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, SubscriptionLevelRequirement requirement)
        {
            var subject = context.User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;

            var subscriptionLevel = _galleryRepository
                .GetApplicationUserProfile(subject)?.SubscriptionLevel;

            if (subscriptionLevel != requirement.RequiredSubscriptionLevel)
            {
                context.Fail();
                return Task.CompletedTask;
            }

            // all checks out
            context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}
using Facade.Notification;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DangMedicalSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationFacade _facade;

        public NotificationController(INotificationFacade facade)
        {
            _facade = facade;
        }

    }
}

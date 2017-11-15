using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AppointmentBooking.Startup))]
namespace AppointmentBooking
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

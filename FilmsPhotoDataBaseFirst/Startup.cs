using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FilmsPhotoDataBaseFirst.Startup))]
namespace FilmsPhotoDataBaseFirst
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

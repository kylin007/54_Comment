using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Comment.Startup))]
namespace Comment
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

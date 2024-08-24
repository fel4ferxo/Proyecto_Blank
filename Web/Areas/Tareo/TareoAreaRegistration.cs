using System.Web.Mvc;

namespace Web.Areas.Tareo
{
    public class TareoAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Tareo";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Tareo_default",
                "Tareo/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "WebMain.Controllers" }
            );
        }
    }


}
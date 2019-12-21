using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Umbraco.Core.Composing;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;
using Umbraco.Web.Mvc;
using Umbraco.Web.PublishedModels;

namespace DttInfo.App_Code
{

    public class SetDefaultRenderMvcControllerComposer : IUserComposer
    {
        public void Compose(Composition composition)
        {
            // Custom route to MyProductController which will use a node with a specific ID as the
            // IPublishedContent for the current rendering page
            RouteTable.Routes.MapUmbracoRoute(
                "ProductCustomRoute",
                "Litteratur/{category}",
                new
                {
                    controller = "LiteratureOverview",
                    action = "index",
                    category = UrlParameter.Optional
                },
                new UmbracoVirtualNodeByIdRouteHandler(1212));


            RouteTable.Routes.MapUmbracoRoute(
                "EventCustomRoute",
                "Arrangementer/{category}/{eventname}",
                new
                {
                    controller = "Events",
                    action = "index",
                    category = UrlParameter.Optional,
                    eventname = UrlParameter.Optional
        },
                new UmbracoVirtualNodeByIdRouteHandler(2665));
        }
    }

    // TODO: Can probably be deleted
    //UmbracoVirtualNodeByIdRouteHandler(New FindJobPageRouteHandler));
    //public class FindJobPageRouteHandler : UmbracoVirtualNodeRouteHandler
    //{
    //    protected override IPublishedContent FindContent(RequestContext requestContext, UmbracoContext umbracoContext)
    //    {
    //        // change this to whatever you need, but make sure it is fast!
    //        var jobspage = umbracoContext.Content.GetAtRoot().First().FirstChild<LiteratureOverview>();
    //        return jobspage;
    //    }
    //}

}
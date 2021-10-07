using DttInfo.ViewModels;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Core.Models;
using Umbraco.Web;
using Umbraco.Web.Models;
using Umbraco.Web.Mvc;
using Umbraco.Web.PublishedModels;

namespace DttInfo.Controllers
{
    public class LiteratureOverviewController : RenderMvcController
    {
        
        // GET: LitteratureOverview
        public override ActionResult Index(ContentModel model)
        {
            if (RouteData.Values["category"] != null)
            {
                // url segment "category"
                string category = RouteData.Values["category"].ToString();
                // the model
                LiteratureListViewModel viewModel = new LiteratureListViewModel(model.Content);

                switch (category)
                {
                    case "livets-bog":
                        viewModel.Catagory = "Livets Bog";
                        viewModel.Books = Umbraco.ContentAtRoot().FirstOrDefault().Descendants().Where(x => x.IsDocumentType("book")).Where(x => x.GetProperty("sortNo").Value<int>() > 0 && x.GetProperty("sortNo").Value<int>() < 10).Select(x => (Book)x).ToList();
                        break;
                    case "det-evige-verdensbillede":
                        viewModel.Catagory = "Det Evige Verdensbillede";
                        viewModel.Books = Umbraco.ContentAtRoot().FirstOrDefault().Descendants().Where(x => x.IsDocumentType("book")).Where(x => x.GetProperty("sortNo").Value<int>() > 10 && x.GetProperty("sortNo").Value<int>() < 20).Select(x => (Book)x).ToList();
                        break;
                    case "andre-store-boeger":
                        viewModel.Catagory = "De øvrige store bøger";
                        viewModel.Books = Umbraco.ContentAtRoot().FirstOrDefault().Descendants().Where(x => x.IsDocumentType("book")).Where(x => x.GetProperty("sortNo").Value<int>() > 20 && x.GetProperty("sortNo").Value<int>() < 50).Select(x => (Book)x).ToList();
                        break;
                    case "smaaboeger":
                        viewModel.Catagory = "Småbøger";
                        viewModel.Books = Umbraco.ContentAtRoot().FirstOrDefault().Descendants().Where(x => x.IsDocumentType("book")).Where(x => x.GetProperty("sortNo").Value<int>() > 100 && x.GetProperty("sortNo").Value<int>() < 150).Select(x => (Book)x).ToList();
                        break;
                    default:
                        {
                            throw new HttpException(404, "Not Found");
                        }
                }
                return View("LiteratureList", viewModel);
            }
            return View("LiteratureOverview", model);
        }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core.Models;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web.Models;
using Umbraco.Web.PublishedModels;

namespace DttInfo.ViewModels
{
    public class LiteratureListViewModel: ContentModel
    {
        public LiteratureListViewModel(IPublishedContent content) : base(content)
        {
            IEnumerable<Book> Books = new List<Book>();
        }
        public string Catagory { get; set; }
        public IEnumerable<Umbraco.Web.PublishedModels.Book> Books { get; set; }
    }
}
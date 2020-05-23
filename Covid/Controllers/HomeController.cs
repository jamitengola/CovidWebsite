using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using NewsAPI;
using NewsAPI.Constants;
using NewsAPI.Models;

namespace Covid.Controllers
{
    public class HomeController : Controller
    {
        List<NoticiaCopia> noticias = new List<NoticiaCopia>();

        public async Task<ActionResult> Index()
        {
            var noticia = await getNoticia();
            foreach (var i in noticia)
            {
                noticias.Add(new NoticiaCopia { Id = i.Url, Fonte = i.Source.Name, Imagem = i.UrlToImage, Data = (DateTime)i.PublishedAt, Descricao = i.Description, Tema = i.Title });
            }
            ViewBag.noticias = noticias;
            return View(noticias);
        }
        public async Task<List<Article>> getNoticia()
        {

            var newsApiClient = new NewsApiClient("3f608f568658410c81829fa44de7f860");
            var articlesResponse = await newsApiClient.GetEverythingAsync(new EverythingRequest
            {
                Q = "Angola Coronavirus",
                SortBy = SortBys.Popularity,
                Language = Languages.PT,
                From = DateTime.Now.AddDays(-29)
            });
            if (articlesResponse.Status == Statuses.Ok)
            {
            }

            return articlesResponse.Articles.ToList();
        }
        public async Task<JsonResult> Noticias()
        {

            var newsApiClient = new NewsApiClient("3f608f568658410c81829fa44de7f860");
            var articlesResponse = await newsApiClient.GetEverythingAsync(new EverythingRequest
            {
                Q = "Covid 19",
                SortBy = SortBys.Popularity,
                Language = Languages.PT,
                From = DateTime.Now.AddDays(-29)
            });
            if (articlesResponse.Status == Statuses.Ok)
            {
            }
            foreach (var i in articlesResponse.Articles.ToList())
            {
                noticias.Add(new NoticiaCopia { Id = i.Url, Fonte = i.Source.Name, Imagem = i.UrlToImage, Data = (DateTime)i.PublishedAt, Descricao = i.Description, Tema = i.Title });
            }
            return Json(noticias, JsonRequestBehavior.AllowGet);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
  
}
public class NoticiaCopia
{
    public string Id { get; set; }
    public string Tema { get; set; }
    public string Imagem { get; set; }
    public string CurtaDescricao { get; set; }
    public string Descricao { get; set; }
    public string Fonte { get; set; }
    public DateTime Data { get; set; }

}
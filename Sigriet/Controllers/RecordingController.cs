using System.IO;
using System.Web;
using System.Web.Http;

namespace Sigriet.Controllers
{
    public class RecordingController : ApiController
    {
        public void Add()
        {
            var file = HttpContext.Current.Request.Files.Count > 0 ?
                HttpContext.Current.Request.Files[0] : null;

            if (file != null && file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);

                var path = Path.Combine(
                    HttpContext.Current.Server.MapPath("~/"),
                    fileName
                );

                file.SaveAs(path);
            }
        }
    }
}

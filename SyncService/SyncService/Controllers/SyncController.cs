using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using SyncService.Providers;

namespace SyncService.Controllers
{
    public class SyncController : Controller
    {
        [Inject]
        public ISyncProvider SyncProvider { get; set; }

        public ActionResult Index()
        {
            return View(SyncProvider.GetCursor());
        }


        public ActionResult NotifyPlaying()
        {
            return PartialView(new PlaylistPosition());
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult NotifyPlaying(PlaylistPosition position)
        {
            SyncProvider.RecordPlay(new PlaylistPosition() {VideoIndex = position.VideoIndex, VideoTime = position.VideoTime}, DateTime.UtcNow );
            return Json(new {reponse = "OK"});
        }

        public ActionResult GetCursor()
        {
            var currentPosition = SyncProvider.GetCursor();
            return Json(currentPosition, JsonRequestBehavior.AllowGet);
        }

    }
}

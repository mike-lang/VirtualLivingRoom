﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using SyncService.Misc;
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

        [JsonpFilter]
        public ActionResult NotifyPlaying(PlaylistPosition position)
        {
            SyncProvider.RecordPlay(new PlaylistPosition() {VideoIndex = position.VideoIndex, VideoTime = position.VideoTime}, DateTime.UtcNow );
            return Json(new {reponse = "OK"});
        }

        [JsonpFilter]
        public ActionResult GetCursor()
        {
            var currentPosition = SyncProvider.GetCursor();
            return Json(currentPosition, JsonRequestBehavior.AllowGet);
        }

    }
}

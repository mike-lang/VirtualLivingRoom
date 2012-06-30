using System;
using System.Collections.Generic;
using System.Linq;

namespace SyncService.Providers
{
    public class InMemSyncProvider : ISyncProvider
    {

        public InMemSyncProvider()
        {
            PlayerReports = new List<PlayerStatus>();
        }

        private List<PlayerStatus> PlayerReports { get; set; } 

        public void RecordPlay(PlaylistPosition playerPosition, DateTime timeNoted)
        {
            PlayerReports.Add(new PlayerStatus() {PlayerPosition = playerPosition, TimeNoted = timeNoted});
        }

        public PlaylistPosition GetCursor()
        {
            var mostRecentReport = PlayerReports.OrderByDescending(report => report.TimeNoted).FirstOrDefault();
            if (mostRecentReport == null)
            {
                return new PlaylistPosition() {VideoIndex = 0, VideoTime = 0};
            }

            var timeSinceLastReport = DateTime.UtcNow - mostRecentReport.TimeNoted;

            return new PlaylistPosition()
                       {
                           VideoIndex = mostRecentReport.PlayerPosition.VideoIndex,
                           VideoTime = mostRecentReport.PlayerPosition.VideoTime + timeSinceLastReport.TotalSeconds
                       };

        }
    }
}
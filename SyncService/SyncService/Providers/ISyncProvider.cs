using System;

namespace SyncService.Providers
{
    public interface ISyncProvider
    {
        void RecordPlay(PlaylistPosition playerPosition, DateTime timeNoted);
        PlaylistPosition GetCursor();
    }
}
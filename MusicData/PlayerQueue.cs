using System;
using System.Collections.Generic;

namespace MusicData
{
    [Serializable()]
    public class PlayerQueue
    {
        private List<QueueItem> queueItems = new List<QueueItem>();
        public List<QueueItem> QueueItems
        {
            get => queueItems;
            set => queueItems = value;
        }
    }
}

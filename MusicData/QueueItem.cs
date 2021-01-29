using System;
using System.Collections.Generic;

namespace MusicData
{
    [Serializable()]
    public class QueueItem
    {
        private List<KeyValuePair<string, string>> item = new List<KeyValuePair<string, string>>();
        public List<KeyValuePair<string, string>> Item
        {
            get => item;
            set => item = value;
        }

        private int count;
        public int Count
        {
            get => count;
            set => count = value;
        }
    }
}

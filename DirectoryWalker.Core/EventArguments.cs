using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryWalker.Core
{
    public class DirectoryWalkerProgressChanged : EventArgs
    {
        public DirectoryEntry CurrentEntry { get; internal set; }
        public int TotalEntriesCount { get; internal set; }
        public int CurrentEntryIndex { get; internal set; }

        internal DirectoryWalkerProgressChanged() { }
    }

    public class DirectoryWalkerCompleted : EventArgs
    {
        public bool IsCancelled { get; internal set; }
    }
}

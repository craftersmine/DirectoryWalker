using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryWalker.Core
{
    public class DirectoryMimeTypeInfo
    {
        public string MimeType { get; private set; }
        public long AverageFileSize => AmountOfFiles != 0 ? TotalSize / AmountOfFiles : 0;
        public int AmountOfFiles { get; internal set; }
        public long TotalSize { get; internal set; }

        internal DirectoryMimeTypeInfo(string mimeType, int amountOfFiles, long totalSize)
        {
            MimeType = mimeType;
            TotalSize = totalSize;
            AmountOfFiles = amountOfFiles;
        }
    }
}

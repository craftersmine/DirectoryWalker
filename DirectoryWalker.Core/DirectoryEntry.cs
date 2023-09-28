using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MimeDetective;
using MimeDetective.Storage;

namespace DirectoryWalker.Core
{
    public class DirectoryEntry
    {
        public string Name { get; private set; }
        public string FullName { get; private set; }
        public string MimeType { get; private set; } = "Unknown";
        public long Size { get; internal set; }
        public DirectoryEntryType Type { get; private set; }
        public DirectoryEntry[] Children { get; internal set; }

        internal DirectoryEntry(string fullName)
        {
            FullName = fullName;
            Name = Path.GetFileName(FullName);
            Type = File.GetAttributes(fullName).HasFlag(FileAttributes.Directory) ? DirectoryEntryType.Directory : DirectoryEntryType.File;
            if (Type == DirectoryEntryType.File)
            {
                var mimeResults = DirectoryWalker.ContentInspector.Inspect(FullName);
                if (mimeResults.Any())
                    MimeType = mimeResults[0].Definition.File.MimeType;
            }
        }

        public DirectoryMimeTypeInfo[] GetMimeTypeInfos()
        {
            return MergeSameTypes(GetMimeTypeInfosInternal(this));
        }

        public int GetFileCount(bool includeDirectories = false)
        {
            return GetFileCountInternal(this, includeDirectories);
        }

        private int GetFileCountInternal(DirectoryEntry entry, bool includeDirectories)
        {
            int count = 0;

            if (!(entry.Children is null) && entry.Children.Any())
                foreach (DirectoryEntry child in entry.Children)
                {
                    if (child.Type == DirectoryEntryType.Directory)
                    {
                        count += GetFileCountInternal(child, includeDirectories);
                        if (includeDirectories)
                            count++;
                    }
                    else 
                        count++;
                }

            return count;
        }

        private DirectoryMimeTypeInfo[] GetMimeTypeInfosInternal(DirectoryEntry entry)
        {
            List<DirectoryMimeTypeInfo> mimeInfos = new List<DirectoryMimeTypeInfo>();

            if (!(entry.Children is null) && entry.Children.Any())
            {
                foreach (DirectoryEntry child in entry.Children)
                {
                    if (!(child is null))
                        switch (child.Type)
                        {
                            case DirectoryEntryType.File:
                                mimeInfos.Add(new DirectoryMimeTypeInfo(child.MimeType, 1, child.Size));
                                break;
                            case DirectoryEntryType.Directory:
                                //if (!(child.Children is null) && child.Children.Any()) 
                                mimeInfos.AddRange(GetMimeTypeInfosInternal(child));
                                break;
                        }
                }
            }

            return mimeInfos.ToArray();
        }

        private DirectoryMimeTypeInfo[] MergeSameTypes(DirectoryMimeTypeInfo[] types)
        {
            List<DirectoryMimeTypeInfo> uniqueTypes = new List<DirectoryMimeTypeInfo>();
            foreach (DirectoryMimeTypeInfo type in types)
            {
                if (!uniqueTypes.Exists(t => t.MimeType == type.MimeType))
                {
                    uniqueTypes.Add(new DirectoryMimeTypeInfo(type.MimeType, 1, type.TotalSize));
                }
                else
                {
                    DirectoryMimeTypeInfo t = uniqueTypes.FirstOrDefault(tt => tt.MimeType == type.MimeType);
                    t.TotalSize += type.TotalSize;
                    t.AmountOfFiles++;
                }
            }

            return uniqueTypes.ToArray();
        }
    }

    public enum DirectoryEntryType
    {
        Directory,
        File,
        Unknown
    }
}

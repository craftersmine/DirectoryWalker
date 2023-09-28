using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DirectoryWalker.Core.Properties;
using MimeDetective;
using MimeDetective.Definitions;
using MimeDetective.Definitions.Licensing;
using MimeDetective.Storage;

namespace DirectoryWalker.Core
{
    public class DirectoryWalker
    {
        private DirectoryWalkerProgressChanged _progressChanged;
        private CancellationToken _cancellationToken;

        internal static ContentInspector ContentInspector = new ContentInspectorBuilder()
        {
            Definitions = Default.All().TrimCategories().TrimDescription().TrimMeta().ToImmutableArray()
        }.Build();

        public event EventHandler<DirectoryWalkerProgressChanged> ProgressChanged;
        public event EventHandler<DirectoryWalkerCompleted> Completed;

        public string DirectoryPath { get; }
        public int TotalEntriesCount { get; private set; }
        public int CurrentEntryIndex { get; private set; }

        public bool IsCancelled
        {
            get
            {
                if (_cancellationToken != CancellationToken.None)
                    return _cancellationToken.IsCancellationRequested;

                return false;
            }
        }

        public DirectoryWalker(string directoryPath)
        {
            DirectoryPath = directoryPath;
            if (!File.GetAttributes(DirectoryPath).HasFlag(FileAttributes.Directory))
                throw new IOException(string.Format(
                    Resources.FilePathIsNotDirectoryExceptionMessageFormat, DirectoryPath));

            DirectoryInfo dir = new DirectoryInfo(DirectoryPath);

            TotalEntriesCount = CountTotalEntries(dir);
            _progressChanged = new DirectoryWalkerProgressChanged();
            _progressChanged.TotalEntriesCount = TotalEntriesCount;
        }

        public async Task<DirectoryEntry> WalkThroughAsync()
        {
            return await WalkThroughAsync(CancellationToken.None);
        }

        public async Task<DirectoryEntry> WalkThroughAsync(CancellationToken cancellationToken)
        {
            _cancellationToken = cancellationToken;
            CurrentEntryIndex = 0;
            DirectoryEntry entry = new DirectoryEntry(DirectoryPath);
            _progressChanged.CurrentEntry = entry;

            if (File.GetAttributes(DirectoryPath).HasFlag(FileAttributes.Directory))
            {
                return await Task.Run(() =>
                {
                    List<DirectoryEntry> children = new List<DirectoryEntry>();
                    DirectoryInfo dir = new DirectoryInfo(DirectoryPath);
                    long size = 0;
                    
                    foreach (FileInfo file in dir.EnumerateFiles())
                    {
                        if (_cancellationToken.IsCancellationRequested)
                            break;
                        DirectoryEntry ent = new DirectoryEntry(file.FullName);
                        ent.Size = file.Length;
                        size += ent.Size;
                        CurrentEntryIndex++;
                        _progressChanged.CurrentEntryIndex = CurrentEntryIndex;
                        _progressChanged.CurrentEntry = ent;
                        ProgressChanged?.Raise(this, _progressChanged);
                        children.Add(ent);
                    }

                    foreach (DirectoryInfo directory in dir.EnumerateDirectories())
                    {
                        if (_cancellationToken.IsCancellationRequested)
                            break;
                        DirectoryEntry child = WalkThroughInternal(entry, directory.Name, _cancellationToken);
                        if (!(child is null))
                        {
                            children.Add(child);
                            size += child.Size;
                        }
                    }

                    entry.Children = children.ToArray();
                    entry.Size = size;
                    
                    Completed?.Raise(this, new DirectoryWalkerCompleted() { IsCancelled = _cancellationToken.IsCancellationRequested });
                    return entry;
                }, _cancellationToken);
            }
            
            throw new IOException(string.Format(
                Resources.FilePathIsNotDirectoryExceptionMessageFormat, DirectoryPath));
        }

        internal DirectoryEntry WalkThroughInternal(DirectoryEntry parentEntry, string entryName, CancellationToken token)
        {

            string fsEntryPath = Path.Combine(parentEntry.FullName, entryName);
            if (File.GetAttributes(fsEntryPath).HasFlag(FileAttributes.Directory))
            {
                long size = 0;
                List<DirectoryEntry> children = new List<DirectoryEntry>();
                DirectoryInfo dir = new DirectoryInfo(fsEntryPath);
                DirectoryEntry currentEntry = new DirectoryEntry(dir.FullName);
                
                if (token.IsCancellationRequested)
                    return null;
                
                foreach (FileInfo file in dir.EnumerateFiles())
                {
                    DirectoryEntry ent = new DirectoryEntry(file.FullName);
                    CurrentEntryIndex++;
                    ent.Size = file.Length;
                    size += ent.Size;
                    _progressChanged.CurrentEntryIndex = CurrentEntryIndex;
                    _progressChanged.CurrentEntry = ent;
                    ProgressChanged?.Raise(this, _progressChanged);
                    children.Add(ent);
                }

                foreach (DirectoryInfo directory in dir.EnumerateDirectories())
                {
                    DirectoryEntry child = WalkThroughInternal(currentEntry, directory.Name, token);
                    if (!(child is null))
                        size += child.Size;
                    CurrentEntryIndex++;
                    _progressChanged.CurrentEntryIndex = CurrentEntryIndex;
                    _progressChanged.CurrentEntry = currentEntry;
                    ProgressChanged?.Raise(this, _progressChanged);
                    children.Add(child);
                }

                currentEntry.Children = children.ToArray();
                currentEntry.Size = size;

                return currentEntry;
            }
            return new DirectoryEntry(fsEntryPath);
        }

        private int CountTotalEntries(DirectoryInfo dir)
        {
            int count = 0;

            foreach (DirectoryInfo directory in dir.EnumerateDirectories())
            {
                count += CountTotalEntries(directory);
                count++;
            }

            count += dir.GetFiles().Length;

            return count;
        }
    }
}

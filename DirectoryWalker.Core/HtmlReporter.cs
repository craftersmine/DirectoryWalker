using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using DirectoryWalker.Core.Properties;

namespace DirectoryWalker.Core
{
    public class HtmlReporter
    {
        public string OutputDirectory { get; }
        public string OutputFile { get; }

        public HtmlReporter(string outputDirectory, string name)
        {
            OutputDirectory = outputDirectory;
            OutputFile = Path.Combine(outputDirectory, name + ".html");
        }

        public void GenerateReport(DirectoryEntry directory, DirectoryMimeTypeInfo[] mimeTypeInfos)
        {
            string tpl = Resources.template;

            string mimeTypeInfo = GetHtmlForMimeTypes(mimeTypeInfos);
            string fsInfo = GetHtmlForDirectory(directory);

            tpl = tpl.Replace("{$reportTitle}", string.Format(Resources.ReportTitleFormat, directory.Name));
            tpl = tpl.Replace("{$generationDateTime}", DateTime.Now.ToString("F"));
            tpl = tpl.Replace("{$directoryPath}", directory.FullName);
            tpl = tpl.Replace("{$totalFilesCount}", directory.GetFileCount().ToString());
            tpl = tpl.Replace("{$totalFilesCountIncludingDirs}", directory.GetFileCount(true).ToString());
            tpl = tpl.Replace("{$totalSize}", GetSizeString(directory.Size));
            tpl = tpl.Replace("{$uniqueMimeTypes}", mimeTypeInfos.Length.ToString());

            tpl = tpl.Replace("{$mimeTypesList}", mimeTypeInfo);
            tpl = tpl.Replace("{$entries}", fsInfo);

            File.WriteAllText(OutputFile, tpl);
        }

        private string GetHtmlForMimeTypes(DirectoryMimeTypeInfo[] mimeTypes)
        {
            if (!(mimeTypes is null) && mimeTypes.Any())
            {
                List<string> mimeHtmls = new List<string>();
                foreach (var mimeType in mimeTypes)
                {
                    mimeHtmls.Add(GetHtmlForMimeType(mimeType));
                }
                return string.Join(Environment.NewLine, mimeHtmls);
            }

            return Resources.NoMimeTypesFound;
        }

        private string GetHtmlForMimeType(DirectoryMimeTypeInfo mimeTypeInfo)
        {
            string tpl = Resources.mime_type_template;
            tpl = tpl.Replace("{$mimeType}", mimeTypeInfo.MimeType);
            tpl = tpl.Replace("{$mimeTotalSize}", GetSizeString(mimeTypeInfo.TotalSize));
            tpl = tpl.Replace("{$mimeAverageFileSize}", GetSizeString(mimeTypeInfo.AverageFileSize));
            tpl = tpl.Replace("{$mimeAmountOfFiles}", mimeTypeInfo.AmountOfFiles.ToString());

            return tpl;
        }

        private string GetHtmlForDirectory(DirectoryEntry directory)
        {
            List<string> fileHtmls = new List<string>();
            List<string> dirHtmls = new List<string>();

            string tpl = Resources.dir_entry_template;

            tpl = tpl.Replace("{$dirName}", directory.Name);

            if (!(directory.Children is null) && directory.Children.Any())
            {
                foreach (DirectoryEntry child in directory.Children)
                {
                    switch (child.Type)
                    {
                        case DirectoryEntryType.File:
                            fileHtmls.Add(GetHtmlForFile(child));
                            break;
                        case DirectoryEntryType.Directory:
                            dirHtmls.Add(GetHtmlForDirectory(child));
                            break;
                    }
                }
            }

            string dirHtml = string.Join(Environment.NewLine, dirHtmls);
            string fileHtml = string.Join(Environment.NewLine, fileHtmls);

            tpl = tpl.Replace("{$directories}", dirHtml);
            tpl = tpl.Replace("{$files}", fileHtml);

            return tpl;
        }

        private string GetHtmlForFile(DirectoryEntry file)
        {
            string tpl = Resources.file_entry_template;
            if (file.Type == DirectoryEntryType.File)
            {
                tpl = tpl.Replace("{$fileName}", file.Name);
                tpl = tpl.Replace("{$mimeType}", file.MimeType);
                tpl = tpl.Replace("{$fileSize}", GetSizeString(file.Size));
            }

            return tpl;
        }

        private string GetSizeString(long size)
        {
            string suffix = Resources.Bytes;
            double sz = size;
            if (sz > 1024)
            {
                sz /= 1024;
                suffix = Resources.Kilobytes;
                if (sz > 1024)
                {
                    sz /= 1024;
                    suffix = Resources.Megabytes;
                    if (sz > 1024)
                    {
                        sz /= 1024;
                        suffix = Resources.Gigabytes;
                    }
                }
            }

            return string.Format("{0:F2} {1}", sz, suffix);
        }
    }
}

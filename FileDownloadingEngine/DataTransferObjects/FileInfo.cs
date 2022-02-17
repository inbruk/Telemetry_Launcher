using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FileDownloadingEngine.DataTransferObjects
{
    public class FileInfo
    {
        public String FilesToDownloadPath { set; get; }

        public String FileName { set; get; }

        public Int64 FileSize { set; get; }

        public Int64 ChunkSize  { set; get; }

        public Int64 ChunkCount { set; get; }
        
        public FileInfo() { }

    }
}

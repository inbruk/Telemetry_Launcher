using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FileDownloadingEngine.DataTransferObjects
{
    public class ChunkInfo
    {
        public Int64 CurrChunkNumber { set; get; }
        public Int64 CurrChunkSize { set; get; }
        public Byte[] ChunkData { set; get; }
    }
}

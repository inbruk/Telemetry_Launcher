using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using DTO = FileDownloadingEngine.DataTransferObjects;
using CommonTools.CRC32Calculator;

namespace FileDownloadingEngine
{
    public static class FileDownloader
    {
        const Int64 ChunkSize = 65000;
        // при отладке текущая директория та, в которой IISExpress
        const String FilesToDownloadPath = @"e:/temp/FilesToDownload/";

        private static String GetFilePathAndNameAndCheckExistence(String fileName)
        {
            // for current directory checking string currPath = Directory.GetCurrentDirectory();
            String filePathAndName = FilesToDownloadPath + fileName;
            if (File.Exists(filePathAndName) == false)
            {
                throw new Exception("Can't get file info. File " + fileName + " not exists in " + FilesToDownloadPath + ".");
            }
            return filePathAndName;
        }
        private static Int64 GetFileSize(String filePathAndName)
        {
            System.IO.FileInfo currFI = new FileInfo(filePathAndName);
            Int64 fileSize = currFI.Length;
            return fileSize;
        }

        public static DTO.FileInfo GetFileInfo(String fileName)
        {
            String filePathAndName = GetFilePathAndNameAndCheckExistence(fileName);
            Int64 fileSize = GetFileSize(filePathAndName);

            Int64 chunkCount = fileSize / ChunkSize;

            // now calculate: is neccessary additional chunk for tail of data ?
            Int64 tailOfDataSize = fileSize%ChunkSize;
            if( tailOfDataSize!=0)
            {
                chunkCount++;
            }

            DTO.FileInfo fInfo = new DTO.FileInfo();

            fInfo.FilesToDownloadPath = FilesToDownloadPath;
            fInfo.FileName = fileName;
            fInfo.FileSize = fileSize;
            fInfo.ChunkSize = ChunkSize;
            fInfo.ChunkCount = chunkCount;

            return fInfo;
        }
        public static DTO.ChunkInfo GetFileChunk(String fileName, Int64 chunkNumber)
        {           
            String filePathAndName = GetFilePathAndNameAndCheckExistence(fileName);
            Int64 fileSize = GetFileSize(filePathAndName);

            // size to read calculation
            Int64 offset = chunkNumber * ChunkSize;
            Int64 readSize = ((offset+ChunkSize)>fileSize) ? (fileSize - offset) : ChunkSize;

            // chunk reading
            FileStream currFS = new FileStream(filePathAndName, FileMode.Open);
            Byte[] returnStorage = new Byte[readSize];
            currFS.Seek(offset, SeekOrigin.Begin);
            Int32 numBytesRead = currFS.Read(returnStorage, 0, (Int32)readSize);
            currFS.Close(); // it is necessary to close file before next chunk in other http request will read

            // readed size checking
            if( numBytesRead != (Int32)readSize) 
            {
                throw new Exception("Can't properly read data chunk from file" + fileName + " in " + FilesToDownloadPath + ". Chunk Number is " + chunkNumber + ".");
            }

            DTO.ChunkInfo result = new DTO.ChunkInfo();

            result.CurrChunkNumber = chunkNumber;
            result.CurrChunkSize = readSize;
            result.ChunkData = returnStorage;

            return result;
        }
        public static UInt32 GetRemoteFileCRC32Hash(String fileName)
        {
            String filePathAndName = GetFilePathAndNameAndCheckExistence(fileName);
            FileStream currFS = new FileStream(filePathAndName, FileMode.Open);
            String strRes = CRC32HashAlgorithm.Compute(currFS);
            UInt32 result = UInt32.Parse(strRes);
            return result;
        }
    }
}

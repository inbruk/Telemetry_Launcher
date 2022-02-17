using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using WS = M2BCenter.LauncherWebService;
using CommonTools.CRC32Calculator;

namespace M2BCenter.Engine
{  
    /// <summary>
    /// универсальный однозадачный движок для закачки любых файлов через сервис
    /// только закачивает файл, каталог для файла уже должен быть подготовлен
    /// </summary>
    public class FileDownloadingTool
    {
        delegate void ProgressTick(Byte currProgressInPercents);

        private String _targetDirectory;
        private String _nameOfFileToDownload;

        private WS.FileInfo _currFileInfo;
        public  WS.FileInfo CurrFileInfo
        {
            get
            {
                return _currFileInfo;
            }
        }

        private Int64 _currChunkNumber;

        public Boolean IsDownloadingComplete
        {
            get
            {
                if (_currChunkNumber >= _currFileInfo.ChunkCount)
                {
                    _currFileStream.Close();
                    _currFileStream = null;
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private WS.tmtLauncherWebService _currServProxy;

        private WS.Token _currSecurityToken;

        private FileStream _currFileStream; 

        public FileDownloadingTool(WS.Token currSecurityToken, String targetDirectory, String fileToDownload)
        {
            _currSecurityToken = currSecurityToken;
            _targetDirectory = targetDirectory;
            _nameOfFileToDownload = fileToDownload;

            // получим данные о загружаемом файле от сервера
            _currServProxy =  WebServiceProxyHandler.Get();
            _currFileInfo = _currServProxy.GetDownloadedFileInfo(_currSecurityToken, _nameOfFileToDownload);
            _currChunkNumber = 0;

            // откроем создаваемый файл, предполагается, что в целевом каталоге его нет
            _currFileStream = new FileStream(_targetDirectory + _nameOfFileToDownload, FileMode.Create);
        }

        public void DownloadNextFileChunk(out Byte progressInPercents, out Int64 progressInBytes)
        {
            // проверим не вышли ли мы за границы файла
            if( IsDownloadingComplete )
            {
                throw new Exception("Try to transfer file chunk form the beyond of the file's borders.");
            }

            // загрузим кусок данных
            WS.ChunkInfo currChunkInfo = _currServProxy.GetDownloadedFileChunk(_currSecurityToken, _nameOfFileToDownload, _currChunkNumber);
            if(_currChunkNumber != currChunkInfo.CurrChunkNumber)
            {
                throw new Exception("Error when file's chunk loading. Wrong chunk number.");
            }

            // сместимся куда нужно в выходном файле
            Int64 offset = _currChunkNumber * _currFileInfo.ChunkSize;
            _currFileStream.Seek(offset, SeekOrigin.Begin);

            // запишем его в выходной файл
            _currFileStream.Write(currChunkInfo.ChunkData, 0, (Int32)currChunkInfo.CurrChunkSize);

            // сдвинемся на следующий кусок
            _currChunkNumber++;

            // рассчитаем выходные параметры
            progressInBytes = offset + currChunkInfo.CurrChunkSize;
            progressInPercents = (Byte)(100 * progressInBytes / _currFileInfo.FileSize);
        }

        /// <summary>
        /// внимание ! в случае не совпаденя Хэшей для следующей закачки нужно полностью пересоздавать объект класса FileDownloadingTool
        /// </summary>
        /// <returns></returns>
        public Boolean CheckDownloadedFileCRC32Hash()
        {
            // закроем поток, если он еще открыт
            if (_currFileStream!=null )
            {
                _currFileStream.Close();
                _currFileStream = null;
            }
            
            // рассчитываем CRC32 по записанному файлу
            _currFileStream = new FileStream(_targetDirectory + _nameOfFileToDownload, FileMode.Open);
            String strRes = CRC32HashAlgorithm.Compute(_currFileStream);
            UInt32 localCRC32Hash = UInt32.Parse(strRes);

            // закроем поток, он уже не нужен
            _currFileStream = null;

            UInt32 WsCRC32Hash = _currServProxy.GetDownloadedFileCRC32Hash(_currSecurityToken, _nameOfFileToDownload);

            Boolean result = (localCRC32Hash == WsCRC32Hash);
            return result;
        }
    }
}

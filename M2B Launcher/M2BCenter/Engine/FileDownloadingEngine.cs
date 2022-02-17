using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using WS = M2BCenter.LauncherWebService;

namespace M2BCenter.Engine
{
    public delegate void partOfWorkComplete(object sender, EventArgs e);
    public delegate void allWorkComplete(object sender, EventArgs e);

    /// <summary>
    /// движок закачки файла из сервиса на локальный винт, внутри использует отдельную задачу, но снаружи интерфейс однозадачный
    /// поэтому удобен для использования из других задач, например из ViewModel, также имеет события для оповещения классов использующих его
    /// если обработчики не установленя, то события не вызываются
    /// </summary>
    public class FileDownloadingEngine
    {
        const String PathForSavingApplications = "../../DownloadedApplications/";

        /// ОБРАБОТЧИКИ РЕАЛИЗОВАНЫ, НО РАБОТУ ПРИ МНОГОЗАДАЧНОСТИ НЕ ПРОВЕРЯЛ, ТАК КАК ОНИ ДЛЯ МОЕЙ ЗАДАЧИ НЕ НУЖНЫ БЫЛИ
        public event partOfWorkComplete partOfWorkCompleteHandler;
        public event allWorkComplete allWorkCompleteHandler;

        public enum FDEState
        {
            // закоментареное пока не используется
            //DirectoryExistenceChecking,
            //DirectoryCreation,
            //DirectoryContentChecking,
            //DirectoryCleaning,
            NotStarted,
            AquireFileInfo,
            FileDataTransmission,
            FileDataCRC32Checking,
            CompleteAll
        };

        private FDEState _currentState;
        public FDEState CurrentState
        {
            get 
            { 
                return _currentState; 
            }
        }

        private WS.AppsDataModel _currAppsDataModel;
        public WS.AppsDataModel CurrAppsDataModel
        {
            get 
            { 
                return _currAppsDataModel; 
            }
        }

        private Byte  _progressInPercents;
        public Byte  ProgressInPercents 
        {
            get
            {
                return _progressInPercents;
            }
        }

        private Int64 _progressInBytes;
        public Int64 ProgressInBytes
        {
            get
            {
                return _progressInBytes;
            }
        }

        private Int64 _fileSize;
        public Int64 FileSize
        {
            get
            {
                return _fileSize;
            }
        }

        private String _fileName;
        public String FileName
        {
            get
            {
                return _fileName;
            }
        }

        private String _targetDirectory;
        public String TargetDirectory
        {
            get
            {
                return _targetDirectory;
            }
        }

        private WS.Token _currSecurityToken;
        private FileDownloadingTool _fileDownloadingTool = null;

        private Boolean _downloadedFileCRC32HashIsEqual;
        public  Boolean DownloadedFileCRC32HashIsEqual
        {
            get
            {
                return _downloadedFileCRC32HashIsEqual;
            }
        }

        private Thread _makeItAllThread;

        public FileDownloadingEngine(WS.Token currSecurityToken, WS.AppsDataModel currMdl)
        {
            _currSecurityToken = currSecurityToken;
            _currAppsDataModel = currMdl;
            _fileName = _currAppsDataModel.ActualFileName;
            _targetDirectory = PathForSavingApplications; // пока поддиректории не поддерживаются

            _currentState = FDEState.NotStarted;
            _progressInPercents = 0;
            _progressInBytes = 0;

            _makeItAllThread = new Thread(MakeItAllThreadMethod);
        }

        /// <summary>
        /// оповещение внешнего обработчика о том, что часть работы выполнена
        /// </summary>
        private void PartOfWorkCompleteNotify()
        {
            if (partOfWorkCompleteHandler != null)
            {
                partOfWorkCompleteHandler(this, null);
            }
        }

        private void MakeItAllThreadMethod()
        {
            try
            {
                // проверить и создать каталог НЕ РЕАЛИЗОВАНО
                // проверить и очистить каталог НЕ РЕАЛИЗОВАНО

                // получить информацию о файле из сервиса
                _currentState = FDEState.AquireFileInfo;
                _fileDownloadingTool = new FileDownloadingTool(_currSecurityToken, PathForSavingApplications, _fileName);
                _fileSize = _fileDownloadingTool.CurrFileInfo.FileSize;
                PartOfWorkCompleteNotify();

                // перекачать данные по кускам
                _currentState = FDEState.FileDataTransmission;
                while (!_fileDownloadingTool.IsDownloadingComplete)
                {
                    _fileDownloadingTool.DownloadNextFileChunk(out _progressInPercents, out _progressInBytes);
                    PartOfWorkCompleteNotify();
                }

                // проверить CRC32 сохраненного файла
                _currentState = FDEState.FileDataCRC32Checking;
                _downloadedFileCRC32HashIsEqual = _fileDownloadingTool.CheckDownloadedFileCRC32Hash();
                if (_downloadedFileCRC32HashIsEqual == false)
                {
                    throw new Exception("Downloaded file has invalid CRC32Hash/");
                }
                PartOfWorkCompleteNotify();
            }
            catch (ThreadAbortException abortException)
            {
                // prevent exceprions only
            }

            // оповестим внешних пользователей, о том что все действия закончены
            _currentState = FDEState.CompleteAll;

            // оповестим обработчик завершения, если он навешен
            if (allWorkCompleteHandler != null)
            {
                allWorkCompleteHandler(this, null);
            }
        }

        // ПОКА ДОКАЧКА НЕ РЕАЛИЗОВАНА ( МОЖНО ПРОСТО СКАЧАТЬ ЗАНОВО ) 
        // докачка имеет смысл после cancelLoad или ошибок
        //public FileDownloadingEngine(WS.AppsDataModel currMdl, Int64 resumeChunkNumber)‏
        //{
        //}

        public void Start()
        {
            _makeItAllThread.Start();
        }
        // ПОКА НЕ ПОДДЕРЖИВАЮТСЯ, ТАК КАК НОРМАЛЬНЫЙ SUSPEND, RESUME ОНИ ЗАБРАКОВАЛИ, НАДО ГОРОДИТЬ ЧТО-ТО С СЕМАФОРАМИ (ДОЛГО)
        //public void PauseLoad()
        //{

        //}

        // ПОКА НЕ ПОДДЕРЖИВАЮТСЯ, ТАК КАК НОРМАЛЬНЫЙ SUSPEND, RESUME ОНИ ЗАБРАКОВАЛИ, НАДО ГОРОДИТЬ ЧТО-ТО С СЕМАФОРАМИ (ДОЛГО)
        //public void ResumeLoad()
        //{
        //}

        public void Cancel()
        {
            _makeItAllThread.Abort();
        }
    }
}
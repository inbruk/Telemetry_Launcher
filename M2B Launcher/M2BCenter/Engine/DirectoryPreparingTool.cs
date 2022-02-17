using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace M2BCenter.Engine
{
    /// <summary>
    /// ПОКА НЕ ГОТОВА И НЕ ИСПОЛЬЗУЕТСЯ, ТАК КАК ДЛЯ ФАЙЛОВ ИЗ ОДНОГО ЭКЗЕШНИКА НЕ НУЖНО СОЗДАВАТЬ И СТИРАТЬ КАТАЛОГИ
    /// 
    /// универсальная однозадачная подсистема для проверки и очистки каталогов 
    /// обычно возле программы для помещения туда чего-либо, но можно и другие
    /// внимание ! в результате работы содержимое каталога будет потеряно
    /// </summary>
    public class DirectoryPreparingTool
    {
        private String _targetDirectory;

        public DirectoryPreparingTool(String targetDirectory)
        {
            _targetDirectory = targetDirectory;
        }

        public Boolean IsTargetDirectoryExists()
        {
            Boolean result = true;

            ///

            return result;
        }

        public Boolean IsTargetDirectoryEmpty()
        {
            Boolean result = true;

            ///

            return result;
        }

        private void CreateTargetDirectory()
        {
            ///
        }

        private void LowLevelCleaning()
        {
            ///
        }

        /// <summary>
        /// если каталог не существует, то он будет создан
        /// если каталогне пуст, то он будет очищен
        /// каталог может быть не очищен, например, если его нет (кто-то его удалил), если в нем есть запущенная программа, захваченные файлы, залили файлы во время удаления и т.п.
        /// точно также каталог возможно не удастся создать -  это штатные ситуации, поэтому возвращаем флаг
        /// </summary>
        public Boolean PrepareDirectory()
        {
            if (IsTargetDirectoryExists() == false)
            {
                CreateTargetDirectory();
            }

            if (IsTargetDirectoryExists() == false) return false;

            LowLevelCleaning();

            if (IsTargetDirectoryEmpty() == false) return false;

            return true;
        }
    }
}

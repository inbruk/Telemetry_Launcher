using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecurityCommunicationEngine
{
    /// <summary>
    /// токен безопасности, пока хранит Guid, но может хранить другое
    /// </summary>
    public class Token
    {
        /// <summary>
        /// иначе не пропихнешь сквозь севрис
        /// </summary>
        public Guid internalData { set;  get; }

        public Guid internalDataU { get; set; }

        /// <summary>
        /// создает новый токен, заполняя данные внутри
        /// </summary>
        public Token()
        {
            internalData = Guid.NewGuid();
        }

        /// <summary>
        /// это понадобится для точного сравнения по значению
        /// </summary>
        /// <param name="obj"> объект класса SecurityToken </param>
        public override bool Equals(Object objToCmp)
        {
            Token objectToCompare = (Token)objToCmp;
            Guid compareObjectInternalData = objectToCompare.internalData;
            return internalData.Equals(compareObjectInternalData);
        }

        public override int GetHashCode()
        {
            return internalData.GetHashCode();
        }
    }
}

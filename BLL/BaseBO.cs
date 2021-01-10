using System;
using Enterprise.Library;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    [Serializable()]
    abstract public class BaseBO : Enterprise.Library.ICache
    {
        #region Properties

        public bool IsEditable = true;

        public object Tag = null;

        public BOStatusEnum BOStatus { get; set; }

        /// <summary>
        /// This returns the text that should appear in a list box or drop down list for this object.
        /// The property is used when binding to a control.
        /// </summary>
        public string DisplayText
        {
            get { return GetDisplayText(); }
        }

        public string GetDisplayText(int maxLength)
        {
            return (DisplayText.Length <= maxLength) ? DisplayText : string.Format("{0}...", DisplayText.Substring(0, maxLength - 3));
        }

        #endregion Properties

        #region Constructor

        /// <summary>
        /// Default constructor.
        /// </summary>
        public BaseBO()
        {
            //Default the status to UnKnown.
            BOStatus = BOStatusEnum.UnKnown;

            IsCached = false;
        }

        #endregion Constructor

        #region Abstract Methods

        protected abstract string GetDisplayText();

        #endregion Abstratct Methods

        #region ICache Members

        public string CacheKey { get; set; }
        public bool IsCached { get; set; }

        #endregion
    }
}

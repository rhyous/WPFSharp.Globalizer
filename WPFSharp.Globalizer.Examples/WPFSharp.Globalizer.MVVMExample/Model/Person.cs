using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WPFSharp.Globalizer.MVVMExample.Model
{
    class Person
    {
        #region Member Variables
        private String _Text = "Hello World";
        #endregion

        #region Constructors
        /// <summary>
        /// The default constructor
        /// </summary>
        public Person()
        {
        }
        #endregion

        #region Properties
        public String Text
        {
            get { return _Text; }
            set { _Text = value; }
        }
        #endregion

        #region Functions
        #endregion

        #region Enums
        #endregion
    }
}

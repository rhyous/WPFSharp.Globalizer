using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WPFSharp.Globalizer.MVVMExample.Model
{
    class Person
    {
        #region Constructors
        /// <summary>
        /// The default constructor
        /// </summary>
        public Person()
        {
        }
        #endregion

        #region Properties
        public string FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        } private string _FirstName = "Jared";

        public string LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        } private string _LastName = "Barneck";

        public int Age
        {
            get { return _Age; }
            set { _Age = value; }
        } private int _Age = 38;
        #endregion

        #region Functions
        #endregion

        #region Enums
        #endregion
    }
}

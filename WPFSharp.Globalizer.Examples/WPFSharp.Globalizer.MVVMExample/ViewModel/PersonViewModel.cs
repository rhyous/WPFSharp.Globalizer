using System;
using MVVM;
using WPFSharp.Globalizer.MVVMExample.Model;

namespace WPFSharp.Globalizer.MVVMExample.ViewModel
{
    class PersonViewModel : ViewModelBase
    {
        #region Member Variables
        private Person _HelloWorld;
        #endregion

        #region Constructors
        /// <summary>
        /// The default constructor
        /// </summary>
        public PersonViewModel()
        {
        }
        #endregion

        #region Properties
        public string FirstNameLabel
        {
            get { return "Form_FirstName"; }
        }

        public string LastNameLabel
        {
            get { return "Form_LastName"; }
        }

        public string AgeLabel
        {
            get { return "Form_Age"; }
        }

        #endregion
    }
}

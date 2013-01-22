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
        public String Text
        {
            get
            {
                if (_HelloWorld == null)
                    _HelloWorld = new Person();
                return _HelloWorld.Text;
            }
            set
            {
                _HelloWorld.Text = value;
                NotifyPropertyChanged("HelloWorld");
            }
        }
        #endregion

        #region Functions
        #endregion

        #region Enums
        #endregion
    }
}

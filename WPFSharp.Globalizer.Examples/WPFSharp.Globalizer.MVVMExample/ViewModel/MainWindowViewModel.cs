using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MVVM;

namespace WPFSharp.Globalizer.MVVMExample.ViewModel
{
    class MainWindowViewModel : ViewModelBase
    {
        #region Member Variables
        private List<ViewModelBase> _ViewModels;
        #endregion

        #region Constructors
        /// <summary>
        /// The default constructor
        /// </summary>
        public MainWindowViewModel()
        {
            ViewModels.Add(new PersonViewModel());
        }
        #endregion

        #region Properties
        public List<ViewModelBase> ViewModels
        {
            get
            {
                if (_ViewModels == null)
                    _ViewModels = new List<ViewModelBase>();
                return _ViewModels;
            }
        }
        #endregion

        #region Functions
        #endregion

        #region Enums
        #endregion
    }
}

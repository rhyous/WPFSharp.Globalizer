using MVVM;

namespace WPFSharp.Globalizer.MVVMExample.ViewModel
{
    class PersonViewModel : ViewModelBase
    {
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

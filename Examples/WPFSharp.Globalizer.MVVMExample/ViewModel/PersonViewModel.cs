using MVVM;
using WPFSharp.Globalizer.MVVMExample.Model;

namespace WPFSharp.Globalizer.MVVMExample.ViewModel
{
    class PersonViewModel : ViewModelBase
    {
        #region Properties
        public string FirstNameLabel { get { return "Form_FirstName"; } }

        public string LastNameLabel { get { return "Form_LastName"; } }

        public string AgeLabel { get { return "Form_Age"; } }

        private Person Person { get { return _Person ?? (_Person = new Person()); } }
        private Person _Person;

        public string FirstNameValue
        {
            get { return Person.FirstName; }
            set
            {
                Person.FirstName = value;
                NotifyPropertyChanged("FirstNameValue");
            }
        }

        public string LastNameValue
        {
            get { return Person.LastName; }
            set
            {
                Person.LastName = value;
                NotifyPropertyChanged("LastNameValue");
            }
        }

        public int AgeValue
        {
            get { return Person.Age; }
            set
            {
                Person.Age = value;
                NotifyPropertyChanged("AgeValue");
            }
        }
        #endregion
    }
}

using MVVM;
using WPFSharp.Globalizer.MVVMExample.Model;

namespace WPFSharp.Globalizer.MVVMExample.ViewModel
{
    class PersonViewModel : ViewModelBase
    {
        #region Properties
        public string FirstNameLabel => "Form_FirstName";

        public string LastNameLabel => "Form_LastName";

        public string AgeLabel => "Form_Age";

        private Person Person { get; } = new Person();

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

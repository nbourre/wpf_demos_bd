using System;
using System.Collections.Generic;
using System.Text;

namespace wpf_demo_phonebook.ViewModels
{
    class MainViewModel : BaseViewModel
    {
        private ContactModel selectedContact;

        public ContactModel SelectedContact
        {
            get => selectedContact;
            set { 
                selectedContact = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            SelectedContact = PhoneBookBusiness.GetContactByID(1);
        }
    }
}

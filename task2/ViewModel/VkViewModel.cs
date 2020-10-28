using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using task2.VK;
namespace task2.ViewModel
{
    class VkViewModel : INotifyPropertyChanged
    {

        private VK_User __user;
        public VK_User User
        {
            get { return __user; }
            set
            {
                __user = value;
                OnPropertyChanged("User");
            }
        }
        public string Find_User(string id)
        {
            __user = new VK_User(id);
            return __user.Status;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}

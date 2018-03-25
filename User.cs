using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace DataBaseClient
{
   public class User: INotifyPropertyChanged
    {
        string name;
        string secondName;
        string lastName;
        int id;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Name
        {
            get { return this.name; }
            set
            {
                if (this.name != value)
                {
                    this.name = value;
                    this.NotifyPropertyChanged("Name");
                }
            }
        }
        public string SecondName
        {
            get => secondName;
            set
            {
                if (this.secondName != value)
                {
                    this.secondName = value;
                    this.NotifyPropertyChanged("SecondName");
                }
            }
        }
        public string LastName
        {
            get => lastName;
            set
            {
                if (this.lastName != value)
                {
                    this.lastName = value;
                    this.NotifyPropertyChanged("LastName");
                }
            }
        }
        public int ID { get => id; set => id = value; }

        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public User()
        {

        }
        public User(string name, string secondname, string lastname)
        {
            Name = name;
            SecondName = secondname;
            LastName = lastname;
            //ID = id;
        }

    }
}

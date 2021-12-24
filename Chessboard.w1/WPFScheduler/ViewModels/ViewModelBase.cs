using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Data;
using System.Windows.Controls.Primitives;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using System.Diagnostics;

namespace WPFScheduler.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        private TimeSpan elapsed;

        public TimeSpan Elapsed
        {
            get { return elapsed; }
            set { elapsed = value; OnPropertyChanged("Elapsed"); }
        }
        
        #region Singleton
        private static readonly ViewModelBase instance = new ViewModelBase();

        public static ViewModelBase Instance
        {
            get { return instance; }
        }

        protected ViewModelBase() { this.ThrowOnInvalidPropertyName = true; }

        #endregion
        #region INotifyProperty
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            this.VerifyPropertyName(propertyName);
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        public void VerifyPropertyName(string propertyName)
        {
            // Verify that the property name matches a real,  
            // public, instance property on this object.
            if (TypeDescriptor.GetProperties(this)[propertyName] == null)
            {
                string msg = "Invalid property name: " + propertyName;

                if (this.ThrowOnInvalidPropertyName)
                    throw new Exception(msg);
                else
                    Debug.Fail(msg);
            }
        }
        #endregion

        public bool ThrowOnInvalidPropertyName { get; set; }
    }

   
}

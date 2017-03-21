using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using MvvmRar.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace MvvmRar.Helpers
{
    class DialogHelper : DependencyObject
    {
        public MainViewModel ViewModel
        {
            get { return (MainViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register("ViewModel", typeof(MainViewModel), typeof(DialogHelper),
            new UIPropertyMetadata(new PropertyChangedCallback(ViewModelProperty_Changed)));

        private static void ViewModelProperty_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (ViewModelProperty != null)
            {
                Binding myBinding = new Binding("FileName");
                myBinding.Source = e.NewValue;
                myBinding.Mode = BindingMode.OneWayToSource;
                BindingOperations.SetBinding(d, FileNameProperty, myBinding);
            }
        }

        private string FileName
        {
            get { return (string)GetValue(FileNameProperty); }
            set { SetValue(FileNameProperty, value); }
        }

        private static readonly DependencyProperty FileNameProperty =
            DependencyProperty.Register("FileName", typeof(string), typeof(DialogHelper),
            new UIPropertyMetadata(new PropertyChangedCallback(FileNameProperty_Changed)));

        private static void FileNameProperty_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Debug.WriteLine("DialogHelper.FileName = {0}", e.NewValue);
        }

        //public ICommand OpenFile { get; private set; }
        public RelayCommand<object> OpenFile { get; private set; }

        public DialogHelper()
        {
            //OpenFile = new RelayCommand<object>((s)=>OpenFileAction(s));
        }


        private void OpenFileAction(object obj)
        {
            OpenFileDialog dlg = new OpenFileDialog();

            //if (dlg.ShowDialog() == true)
            //{
            //    FileName = dlg.FileName;
            //}
        }
    }
}

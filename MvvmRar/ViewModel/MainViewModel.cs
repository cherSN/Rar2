using GalaSoft.MvvmLight;
using MvvmRar.Model;
using System.Collections.ObjectModel;
using MvvmRar.Rar;
using System;

namespace MvvmRar.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// See http://www.mvvmlight.net
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private readonly IDataService _dataService;

        private RarFormF6 _RarFile;

        public DateTime DocumentDate
        {
            get
            {
                return _RarFile.DocumentDate;
            }

            set
            {
                _RarFile.DocumentDate = value;
                RaisePropertyChanged("DocumentDate");
            }
        }

        public ObservableCollection<RarCompany> BuyersList { set; get; }




        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IDataService dataService)
        {
            _dataService = dataService;
            _dataService.GetCompanies(
                (list, error) =>
                {
                    if (error != null)
                    {
                        // Report error here
                        return;
                    }

                    BuyersList = new ObservableCollection<RarCompany>(list);
                });
        }

        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}
    }
}
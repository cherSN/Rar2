using GalaSoft.MvvmLight;
using MvvmRar.Model;
using System.Collections.ObjectModel;
using MvvmRar.Rar;
using System;
using System.Windows.Data;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using GalaSoft.MvvmLight.Command;
using System.Windows;

namespace MvvmRar.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        #region  - Private Fields -
        private readonly IDataService _dataService;

        private RarFormF6 _RarFile;
        private ObservableCollection<string> alcoCodesList;
        private ObservableCollection<RarCompany> buyersList;
        private ObservableCollection<RarCompany> manufacturersList;
        private ObservableCollection<RarTurnoverData> turnoverDataList;
        private ListCollectionView turnoverDataListCollectionView;
        private RarCompany selectedBuyer;
        private ObservableCollection<RarCompany> savingCompaniesList;

        #endregion

        public Visibility WindowIsVisible {
            get {
                if (SelectedPath == null) return System.Windows.Visibility.Hidden;
                    else return System.Windows.Visibility.Visible;
            }
        }

        //public System.Windows.Visibility GetWindowIsVisible()
        //{
        //    if (SelectedPath == null) return System.Windows.Visibility.Hidden;
        //    else return System.Windows.Visibility.Visible;
        //}



        #region - Public Properties -
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
        public string Version
        {
            get
            {
                return _RarFile.Version;
            }

            set
            {
                _RarFile.Version = value;
                RaisePropertyChanged("Version");

            }
        }
        public string ProgramName
        {
            get { return _RarFile.ProgramName; }
            set
            {
                _RarFile.ProgramName = value;
                RaisePropertyChanged("ProgramName");
            }
        }
        public string FormNumber
        {
            get { return _RarFile.FormNumber; }
            set
            {
                _RarFile.FormNumber = value;
                RaisePropertyChanged("FormNumber");
            }
        }
        public string ReportPeriod
        {
            get { return _RarFile.ReportPeriod; }
            set
            {
                _RarFile.ReportPeriod = value;
                RaisePropertyChanged("ReportPeriod");
            }
        }
        public string YearReport
        {
            get { return _RarFile.ReportYear; }
            set
            {
                _RarFile.ReportYear = value;
                RaisePropertyChanged("YearReport");
            }
        }
        public string CorrectionNumber
        {
            get { return _RarFile.CorrectionNumber; }
            set
            {
                _RarFile.CorrectionNumber = value;
                RaisePropertyChanged("CorrectionNumber");
            }
        }
        public RarOurCompany OurCompany
        {
            get
            {
                return _RarFile.OurCompany;
            }

            set
            {
                _RarFile.OurCompany = value;
                RaisePropertyChanged("OurCompany");

            }
        }

        public ObservableCollection<string> AlcoCodesList
        {
            get
            {
                return alcoCodesList;
            }

            set
            {
                alcoCodesList = value;
            }
        }
        public ObservableCollection<RarCompany> BuyersList
        {
            get
            {
                return buyersList;
            }

            set
            {
                buyersList = value;
                //RaisePropertyChanged("BuyersList");
            }
        }
        public ObservableCollection<RarCompany> ManufacturersList
        {
            get
            {
                return manufacturersList;
            }

            set
            {
                manufacturersList = value;
                //RaisePropertyChanged("ManufacturersList");

            }
        }
        public ObservableCollection<RarTurnoverData> TurnoverDataList
        {
            get
            {
                return turnoverDataList;
            }

            set
            {
                turnoverDataList = value;
                //RaisePropertyChanged("TurnoverDataList");

            }
        }
        public ListCollectionView TurnoverDataListCollectionView
        {
            get
            {
                return turnoverDataListCollectionView;
            }

            set
            {
                turnoverDataListCollectionView = value;
                //RaisePropertyChanged("TurnoverDataListCollectionView");

            }
        }
        public ObservableCollection<RarCompany> SavingCompaniesList
        {
            get
            {
                return savingCompaniesList;
            }

            set
            {
                savingCompaniesList = value;
                //RaisePropertyChanged("SavingCompaniesList");

            }
        }
        public RarCompany SelectedBuyer
        {
            get
            {
                return selectedBuyer;
            }

            set
            {
                selectedBuyer = value;
                RaisePropertyChanged("SelectedBuyer");

            }
        }
        #endregion

        public MainViewModel(IDataService dataService)
        {
            //WindowIsVisible = System.Windows.Visibility.Hidden;
            _dataService = dataService;
            _dataService.GetData(
                (data, error) =>
                {
                    if (error != null)
                    {
                        // Report error here
                        return;
                    }

                    _RarFile = data;
                });

            TurnoverDataList = new ObservableCollection<RarTurnoverData>(_RarFile.TurnoverDataList);
            //_RarFile.BuyersList.Sort( (s1, s2) => String.Compare(s1.Name, s2.Name) );
            _RarFile.BuyerList.Sort((s1, s2) => SortStringsAsNumbers(s1.ID, s2.ID));

            //List<VMRarCompany> cmplst = _RarFile.BuyerList.Select(s => new VMRarCompany(s)).ToList();
            //BuyersList = new ObservableCollection<VMRarCompany>(cmplst);
            BuyersList = new ObservableCollection<RarCompany>(_RarFile.BuyerList);

            ManufacturersList = new ObservableCollection<RarCompany>(_RarFile.ManufacturerList);

            TurnoverDataListCollectionView = new ListCollectionView(TurnoverDataList);
            //TurnoverDataListCollectionView.GroupDescriptions.Add(new PropertyGroupDescription("Subdevision"));
            TurnoverDataListCollectionView.SortDescriptions.Add(new SortDescription("DocumentNumber", ListSortDirection.Ascending));
            TurnoverDataListCollectionView.Filter = Buyer_Filter;
            AlcoCodesList = new ObservableCollection<string>() {"200", "400" };
            AlcoCodesList.Add("210");
            AlcoCodesList.Add("410");

            //UpdateAll();

            OpenFileCommand = new RelayCommand(OpenFile);

        }

        private void UpdateAll()
        {
            RaisePropertyChanged("DocumentDate");
            RaisePropertyChanged("Version");
            RaisePropertyChanged("ProgramName");
            RaisePropertyChanged("FormNumber");
            RaisePropertyChanged("ReportPeriod");
            RaisePropertyChanged("YearReport");
            RaisePropertyChanged("CorrectionNumber");
            RaisePropertyChanged("OurCompany");
            //OnPropertyChanged("TurnoverDataList");
            //OnPropertyChanged("BuyersList");

            //OnCollectionChanged();

        }

        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}

        private bool IsInnValid(string inn)
        {
            return true;
        }
        private bool IsKppValid(string inn)
        {
            return true;
        }

        public void InitializeCompaniesList()
        {
            List<RarCompany> companiesList = TurnoverDataList.Where(s => s.Buyer == SelectedBuyer).Select(p => p.Manufacturer).Distinct().ToList();
            foreach (RarCompany company in companiesList)
            {
                string Inn = company.INN == null ? "" : company.INN.Trim();
                string Kpp = company.KPP == null ? "" : company.KPP.Trim();
                if ((Inn.Length != 10) || (Kpp.Length != 9)) continue;
                if (IsInnValid(Inn) && IsKppValid(Kpp))
                {
                    company.Adress.CountryId = "643";
                    company.Adress.RegionId = "77";
                }


            }
            SavingCompaniesList = new ObservableCollection<RarCompany>(companiesList);

        }

        private bool Buyer_Filter(object item)
        {
            if (SelectedBuyer == null) return true;
            RarTurnoverData dt = (RarTurnoverData)item;
            if (dt.Buyer == SelectedBuyer)
                return true;
            else return false;
        }

        private int SortStringsAsNumbers(string s1, string s2)
        {
            double num1;
            double num2;
            if (Double.TryParse(s1, out num1))
            {
                if (Double.TryParse(s2, out num2))
                {
                    return num1.CompareTo(num2);
                }

            }
            return String.Compare(s1, s2);
        }


        public RelayCommand OpenFileCommand { get; set; }
        private void OpenFile()
        {
            SelectedPath = _ioService.OpenFileDialog(@"c:\Is.txt");
            if (SelectedPath == null)
            {
                SelectedPath = string.Empty;
            }

            //OpenFileDialog openFileDialog = new OpenFileDialog();
            //if (openFileDialog.ShowDialog() == true)
            //{

            //    _RarFile.LoadF6(openFileDialog.FileName);
            //    TurnoverDataList = new ObservableCollection<RarTurnoverData>(_RarFile.TurnoverDataList);
            //    //_RarFile.BuyersList.Sort( (s1, s2) => String.Compare(s1.Name, s2.Name) );
            //    _RarFile.BuyersList.Sort((s1, s2) => SortStringsAsNumbers(s1.ID, s2.ID));

            //    BuyersList = new ObservableCollection<RarCompany>(_RarFile.BuyersList);

            //    ManufacturersList = new ObservableCollection<RarCompany>(_RarFile.ManufacturersList);

            //    TurnoverDataListCollectionView = new ListCollectionView(TurnoverDataList);
            //    //TurnoverDataListCollectionView.GroupDescriptions.Add(new PropertyGroupDescription("Subdevision"));
            //    TurnoverDataListCollectionView.SortDescriptions.Add(new SortDescription("DocumentNumber", ListSortDirection.Ascending));
            //    TurnoverDataListCollectionView.Filter = Buyer_Filter;

            //    UpdateAll();
            //}
        }

        private string _selectedPath;
        public string SelectedPath
        {
            get { return _selectedPath; }
            set { _selectedPath = value; RaisePropertyChanged("SelectedPath"); }
        }



        private IOService _ioService;
    }
}
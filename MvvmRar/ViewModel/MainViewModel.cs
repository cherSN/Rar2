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
using System.IO;
using MvvmDialogs.ViewModels;
using System.Text.RegularExpressions;
using System.Collections.Specialized;
//using MvvmDialogs.ViewModels;

namespace MvvmRar.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        #region  - Private Fields -
        private string _FileName;
        private readonly IDataService _DataService;
        private bool _WindowIsVisible;
        //private ObservableCollection<IDialogViewModel> _Dialogs = new ObservableCollection<IDialogViewModel>();
        private ObservableCollection<IDialogViewModel> _Dialogs;
        private RarFormF6 _RarFile;
        private ObservableCollection<string> _AlcoCodeList;
        private ObservableCollection<RarCompanyViewModelWrapper> _BuyersList;
        private ObservableCollection<RarCompany> manufacturersList;
        private ObservableCollection<RarTurnoverDataViewModelWrapper> _TurnoverDataList;
        private ListCollectionView turnoverDataListCollectionView;
        private RarCompanyViewModelWrapper _SelectedBuyer;
        private ObservableCollection<RarCompany> savingCompaniesList;

        #endregion

        private string cou;
        public string Cou { get => cou; set  { cou = value; RaisePropertyChanged("Cou"); } }


        #region - Public Properties -
        public ObservableCollection<IDialogViewModel> Dialogs { get { return _Dialogs; } }
        public bool WindowIsVisible {
            get {
                return true; // _WindowIsVisible;
            }
            set {
                _WindowIsVisible = value;
                RaisePropertyChanged("WindowIsVisible");
            }
        }
        public string FileName
        {
            get
            {
                return _FileName;
            }

            set
            {
                _FileName = value;
                RaisePropertyChanged("FileName");
                WindowIsVisible = true;
                OpenFile();
            }
        }

        public DateTime DocumentDate
        {
            get {  return _RarFile.DocumentDate; }
            set { _RarFile.DocumentDate = value; RaisePropertyChanged("DocumentDate");  }
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

        public ObservableCollection<string> AlcoCodeList
        {
            get
            {
                return _AlcoCodeList;
            }

            set
            {
                _AlcoCodeList = value;
            }
        }
        public ObservableCollection<RarCompanyViewModelWrapper> BuyersList
        {
            get
            {
                return _BuyersList;
            }

            set
            {
                _BuyersList = value;
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

        public ObservableCollection<RarTurnoverDataViewModelWrapper> TurnoverDataList
        {
            get
            {
                return _TurnoverDataList;
            }

            set
            {
                _TurnoverDataList = value;
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
        public RarCompanyViewModelWrapper SelectedBuyer
        {
            get
            {
                return _SelectedBuyer;
            }

            set
            {
                _SelectedBuyer = value;
                RaisePropertyChanged("SelectedBuyer");

            }
        }
        #endregion

        public MainViewModel(IDataService dataService)
        {
            //WindowIsVisible = System.Windows.Visibility.Hidden;
            _Dialogs = new ObservableCollection<IDialogViewModel>();

            _DataService = dataService;
            _DataService.GetData(
                (data, error) =>
                {
                    if (error != null)
                    {
                        // Report error here
                        return;
                    }

                    _RarFile = data;
                });

            //TurnoverDataList = new ObservableCollection<RarTurnoverData>(_RarFile.TurnoverDataList);
            List<RarTurnoverDataViewModelWrapper>  TurnoverDataViewModelWrapperList = _RarFile.TurnoverDataList.Select(s=>new RarTurnoverDataViewModelWrapper(s)).ToList();
            _TurnoverDataList = new ObservableCollection<RarTurnoverDataViewModelWrapper>(TurnoverDataViewModelWrapperList);

            //_RarFile.BuyersList.Sort( (s1, s2) => String.Compare(s1.Name, s2.Name) );
            //_RarFile.BuyerList.Sort((s1, s2) => SortStringsAsNumbers(s1.ID, s2.ID));

            List<RarCompanyViewModelWrapper> ccc = TurnoverDataList.Select(s => s.Buyer).Distinct().ToList();

            List<RarCompanyViewModelWrapper> cmplst = _RarFile.BuyerList.Select(s => new RarCompanyViewModelWrapper(s)).ToList();
            _BuyersList = new ObservableCollection<RarCompanyViewModelWrapper>(ccc);
            //BuyersList = new ObservableCollection<RarCompany>(_RarFile.BuyerList);

            manufacturersList = new ObservableCollection<RarCompany>(_RarFile.ManufacturerList);

            TurnoverDataListCollectionView = new ListCollectionView(TurnoverDataList);
            //TurnoverDataListCollectionView.GroupDescriptions.Add(new PropertyGroupDescription("Subdevision"));
            //TurnoverDataListCollectionView.SortDescriptions.Add(new SortDescription("DocumentNumber", ListSortDirection.Ascending));
            //TurnoverDataListCollectionView.Filter = Buyer_Filter;
            AlcoCodeList = new ObservableCollection<string>() { "200",  "210", "400", "410" };

            //UpdateAll();

            //OpenFileCommand = new RelayCommand(OpenFile);

        }

        public RelayCommand NewOpenFileDialogCommand { get { return new RelayCommand(OnNewOpenFileDialog); } }
        public void OnNewOpenFileDialog()
        {
            var dlg = new OpenFileDialogViewModel
            {
                Title = "Select a file (I won't actually do anything with it)",
                Filter = "All files (*.*)|*.*",
                Multiselect = false
            };

            if (!dlg.Show(this.Dialogs)) return;

            FileName = dlg.FileName;
            RarFormF6Formatter F6formatter = new RarFormF6Formatter();
            if (FileName == null) return;
            using (FileStream fileStream = new FileStream(FileName, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                _RarFile = (RarFormF6)F6formatter.Deserialize(fileStream);
            }
            Cou = _RarFile.BuyerList.Count.ToString();

            BuyersList.Clear();
            List<RarCompanyViewModelWrapper> cmplst = _RarFile.BuyerList.Select(s => new RarCompanyViewModelWrapper(s)).ToList();
            //cmplst.Select(s=>BuyersList.Add((RarCompanyViewModelWrapper)s));
            foreach (RarCompanyViewModelWrapper item in cmplst)
            {
                BuyersList.Add(item);
            }

            TurnoverDataList.Clear();
            List<RarTurnoverDataViewModelWrapper> turnoverDataViewModelWrapperList = _RarFile.TurnoverDataList.Select(s => new RarTurnoverDataViewModelWrapper(s)).ToList();
            foreach (RarTurnoverDataViewModelWrapper item in turnoverDataViewModelWrapperList)
            {
                TurnoverDataList.Add(item);
            }
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
            //RaisePropertyChanged("WindowIsVisible");
            RaisePropertyChanged("TurnoverDataList");
            RaisePropertyChanged("BuyersList");
            //TurnoverDataList.CollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            //OnPropertyChanged("BuyersList");

            //OnCollectionChanged();

        }

        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}

        private bool IsInnValid(string INNstring)
        {
            // является ли вообще числом
            try { Int64.Parse(INNstring); } catch { return false; }

            // проверка на 10 и 12 цифр
            if (INNstring.Length != 10 && INNstring.Length != 12) { return false; }

            // проверка по контрольным цифрам
            if (INNstring.Length == 10) // для юридического лица
            {
                int dgt10 = 0;
                try
                {
                    dgt10 = (((2 * Int32.Parse(INNstring.Substring(0, 1))
                        + 4 * Int32.Parse(INNstring.Substring(1, 1))
                        + 10 * Int32.Parse(INNstring.Substring(2, 1))
                        + 3 * Int32.Parse(INNstring.Substring(3, 1))
                        + 5 * Int32.Parse(INNstring.Substring(4, 1))
                        + 9 * Int32.Parse(INNstring.Substring(5, 1))
                        + 4 * Int32.Parse(INNstring.Substring(6, 1))
                        + 6 * Int32.Parse(INNstring.Substring(7, 1))
                        + 8 * Int32.Parse(INNstring.Substring(8, 1))) % 11) % 10);
                }
                catch { return false; }

                if (Int32.Parse(INNstring.Substring(9, 1)) == dgt10) { return true; }
                else { return false; }
            }
            else // для физического лица
            {
                int dgt11 = 0, dgt12 = 0;
                try
                {
                    dgt11 = (((
                        7 * Int32.Parse(INNstring.Substring(0, 1))
                        + 2 * Int32.Parse(INNstring.Substring(1, 1))
                        + 4 * Int32.Parse(INNstring.Substring(2, 1))
                        + 10 * Int32.Parse(INNstring.Substring(3, 1))
                        + 3 * Int32.Parse(INNstring.Substring(4, 1))
                        + 5 * Int32.Parse(INNstring.Substring(5, 1))
                        + 9 * Int32.Parse(INNstring.Substring(6, 1))
                        + 4 * Int32.Parse(INNstring.Substring(7, 1))
                        + 6 * Int32.Parse(INNstring.Substring(8, 1))
                        + 8 * Int32.Parse(INNstring.Substring(9, 1))) % 11) % 10);
                    dgt12 = (((
                        3 * Int32.Parse(INNstring.Substring(0, 1))
                        + 7 * Int32.Parse(INNstring.Substring(1, 1))
                        + 2 * Int32.Parse(INNstring.Substring(2, 1))
                        + 4 * Int32.Parse(INNstring.Substring(3, 1))
                        + 10 * Int32.Parse(INNstring.Substring(4, 1))
                        + 3 * Int32.Parse(INNstring.Substring(5, 1))
                        + 5 * Int32.Parse(INNstring.Substring(6, 1))
                        + 9 * Int32.Parse(INNstring.Substring(7, 1))
                        + 4 * Int32.Parse(INNstring.Substring(8, 1))
                        + 6 * Int32.Parse(INNstring.Substring(9, 1))
                        + 8 * Int32.Parse(INNstring.Substring(10, 1))) % 11) % 10);
                }
                catch { return false; }
                if (Int32.Parse(INNstring.Substring(10, 1)) == dgt11
                    && Int32.Parse(INNstring.Substring(11, 1)) == dgt12) { return true; }
                else { return false; }
            }
        }
        private bool IsKppValid(string KPPstring)
        {
            return new Regex(@"\d{4}[\dA-Z][\dA-Z]\d{3}").IsMatch(KPPstring);
        }



        public void InitializeCompaniesList()
        {
            //List<RarCompanyViewModelWrapper> companiesList = TurnoverDataList.Where(s => s.Buyer == SelectedBuyer).Select(p => p.Manufacturer).Distinct().ToList();
            //foreach (RarCompanyViewModelWrapper company in companiesList)
            //{
            //    string Inn = company.INN == null ? "" : company.INN.Trim();
            //    string Kpp = company.KPP == null ? "" : company.KPP.Trim();
            //    if ((Inn.Length != 10) || (Kpp.Length != 9)) continue;
            //    if (IsInnValid(Inn) && IsKppValid(Kpp))
            //    {
            //        //company.Adress.CountryId = "643";
            //        //company.Adress.RegionId = "77";
            //    }


            //}
            //SavingCompaniesList = new ObservableCollection<RarCompany>(companiesList);

        }

        private bool Buyer_Filter(object item)
        {
            return true;
            //if (SelectedBuyer == null) return true;
            //RarTurnoverData dt = (RarTurnoverData)item;
            //if (dt.Buyer == SelectedBuyer)
            //    return true;
            //else return false;
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
            //SelectedPath = _ioService.OpenFileDialog(@"c:\Is.txt");
            //if (SelectedPath == null)
            //{
            //    SelectedPath = string.Empty;
            //}

            //OpenFileDialog openFileDialog = new OpenFileDialog();
            //if (openFileDialog.ShowDialog() == true)
            //{

            RarFormF6Formatter F6formatter = new RarFormF6Formatter();
            if (FileName == null) return;
            using (FileStream fileStream = new FileStream(FileName, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                _RarFile = (RarFormF6)F6formatter.Deserialize(fileStream);
            }

            UpdateAll();
            //_RarFile.LoadF6(openFileDialog.FileName);
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






    }
}
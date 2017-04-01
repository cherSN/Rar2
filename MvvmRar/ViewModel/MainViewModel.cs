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
//using MvvMDialogs.ViewModels;
//using MvvmRar.MvvMDialog.ViewModels;

namespace MvvmRar.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        #region  - Private Fields -
        private string _FileName;
        private readonly IDataService _DataService;
        private bool _WindowIsVisible;
        private ObservableCollection<IDialogViewModel> _Dialogs = new ObservableCollection<IDialogViewModel>();

        private RarFormF6 _RarFile;
        private ObservableCollection<string> _AlcoCodeList;
        private ObservableCollection<RarCompanyWrapper> _BuyerList;
        private ObservableCollection<RarCompanyWrapper> _ManufacturerList;
        private ObservableCollection<RarTurnoverDataWrapper> _TurnoverDataList;
        private RarCompanyWrapper _SelectedBuyer;

        private ListCollectionView turnoverDataListCollectionView;
        private ObservableCollection<RarCompany> savingCompaniesList;

        #endregion

        #region - Public Properties -
        public ObservableCollection<IDialogViewModel> Dialogs { get { return _Dialogs; } }
        public bool WindowIsVisible {
            get => true; // _WindowIsVisible; 
            set {_WindowIsVisible = value;  RaisePropertyChanged("WindowIsVisible");  }
        }
        public string FileName {
            get=>_FileName; 
            set {
                _FileName = value;
                RaisePropertyChanged("FileName");
                WindowIsVisible = true;
            }
        }

        public DateTime DocumentDate {get=>_RarFile.DocumentDate; set { _RarFile.DocumentDate = value; RaisePropertyChanged("DocumentDate");}}
        public string Version {get=>  _RarFile.Version; set {_RarFile.Version = value; RaisePropertyChanged("Version");}}
        public string ProgramName {get=>_RarFile.ProgramName; set {_RarFile.ProgramName = value; RaisePropertyChanged("ProgramName");}}
        public string FormNumber {get=>_RarFile.FormNumber; set {_RarFile.FormNumber = value; RaisePropertyChanged("FormNumber");}}
        public string ReportPeriod {get=>_RarFile.ReportPeriod; set {_RarFile.ReportPeriod = value; RaisePropertyChanged("ReportPeriod");}}
        public string YearReport {get=>_RarFile.ReportYear; set {_RarFile.ReportYear = value;RaisePropertyChanged("YearReport");}}
        public string CorrectionNumber {get=>_RarFile.CorrectionNumber; set {_RarFile.CorrectionNumber = value; RaisePropertyChanged("CorrectionNumber");}}
        public RarOurCompany OurCompany {get=>_RarFile.OurCompany; set {_RarFile.OurCompany = value; RaisePropertyChanged("OurCompany");}}

        public ObservableCollection<string> AlcoCodeList { get=> _AlcoCodeList; set=>_AlcoCodeList = value;}

        public ObservableCollection<RarCompanyWrapper> BuyerList {get=>_BuyerList; set=>_BuyerList = value;}
        public ObservableCollection<RarCompanyWrapper> ManufacturerList {get=>_ManufacturerList; set=>_ManufacturerList = value;}
        public ObservableCollection<RarTurnoverDataWrapper> TurnoverDataList {get=>_TurnoverDataList; set=>_TurnoverDataList = value;}

        public ListCollectionView TurnoverDataListCollectionView {get=>turnoverDataListCollectionView; set=>turnoverDataListCollectionView = value;}
        
        public ObservableCollection<RarCompany> SavingCompaniesList {get=>savingCompaniesList; set=>savingCompaniesList = value;}
        public RarCompanyWrapper SelectedBuyer {
            get =>_SelectedBuyer;
            set {
                _SelectedBuyer = value;
                RaisePropertyChanged("SelectedBuyer");
                TurnoverDataListCollectionView.Refresh();
            }
        }
        #endregion

        private void SetupCollections()
        {
            List<RarCompanyWrapper> rarBuyerViewModelWrapperList = _RarFile.BuyerList.Distinct().Select(s => new RarCompanyWrapper(s)).ToList();
            BuyerList.Clear();
            foreach (RarCompanyWrapper item in rarBuyerViewModelWrapperList) BuyerList.Add(item);


            ManufacturerList.Clear();
            List<RarCompanyWrapper> rarManufacturerViewModelWrapperList = _RarFile.ManufacturerList.Distinct().Select(s => new RarCompanyWrapper(s)).ToList();
            foreach (RarCompanyWrapper item in rarManufacturerViewModelWrapperList) ManufacturerList.Add(item);
            AlcoCodeList.Clear();
            List<string> validAlcoCodeList = RarValidAlcoCodeList.GetAlcoCodesListFromXSD();
            foreach (string item in validAlcoCodeList) AlcoCodeList.Add(item);

            TurnoverDataList.Clear();
            foreach (RarTurnoverData item in _RarFile.TurnoverDataList)
            {
                RarTurnoverDataWrapper turnoverDataViewModelWrapper = new RarTurnoverDataWrapper(item);

                List<RarCompanyWrapper> findedBuyerList = BuyerList.Where(s => s.Company == item.Buyer).ToList();
                if (findedBuyerList.Count == 1) turnoverDataViewModelWrapper.Buyer = findedBuyerList.First();
                else throw new ApplicationException("Ошибка синхронизации контрагентов");

                List<RarCompanyWrapper> findedManufacturerList =ManufacturerList.Where(s => s.Company == item.Manufacturer).ToList();
                if (findedManufacturerList.Count == 1) turnoverDataViewModelWrapper.Manufacturer = findedManufacturerList.First();
                else throw new ApplicationException("Ошибка синхронизации контрагентов");

                TurnoverDataList.Add(turnoverDataViewModelWrapper);
            }
        }

        public MainViewModel(IDataService dataService)
        {
            //WindowIsVisible = System.Windows.Visibility.Hidden;
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

            _AlcoCodeList = new ObservableCollection<string>();
            _BuyerList = new ObservableCollection<RarCompanyWrapper>();
            _ManufacturerList = new ObservableCollection<RarCompanyWrapper>();
            _TurnoverDataList = new ObservableCollection<RarTurnoverDataWrapper>();
            SetupCollections();


            TurnoverDataListCollectionView = new ListCollectionView(TurnoverDataList);
            TurnoverDataListCollectionView.Filter = Buyer_Filter;
            //UpdateAll();

        }

        public RelayCommand EditCompanyDialogCommand { get { return new RelayCommand(EditCompanyDialog); } }
        public void EditCompanyDialog()
        {
            var dlg = new EditCompanyDialogViewModel();
            dlg.Show(this.Dialogs);
            //    OpenFileDialogViewModel
            //{
            //    Title = "Выберите файл",
            //    Filter = "xml files (*.xml)|*.xml",
            //    Multiselect = false
            //};

            //if (!dlg.Show(this.Dialogs)) return;

            //FileName = dlg.FileName;
            //RarFormF6Formatter F6formatter = new RarFormF6Formatter();
            //if (FileName == null) return;
            //using (FileStream fileStream = new FileStream(FileName, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            //{
            //    _RarFile = (RarFormF6)F6formatter.Deserialize(fileStream);
            //    _RarFile.BuyerList.Sort((s1, s2) => String.Compare(s1.Name, s2.Name));
            //}
            //SetupCollections();
        }

        public RelayCommand OpenFileDialogCommand { get { return new RelayCommand(OpenFileDialog); } }
        public void OpenFileDialog()
        {
            var dlg = new OpenFileDialogViewModel
            {
                Title = "Выберите файл",
                Filter = "xml files (*.xml)|*.xml",
                Multiselect = false
            };

            if (!dlg.Show(this.Dialogs)) return;

            FileName = dlg.FileName;
            RarFormF6Formatter F6formatter = new RarFormF6Formatter();
            if (FileName == null) return;
            using (FileStream fileStream = new FileStream(FileName, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                _RarFile = (RarFormF6)F6formatter.Deserialize(fileStream);
                _RarFile.BuyerList.Sort((s1, s2) => String.Compare(s1.Name, s2.Name));
            }
            SetupCollections();
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
            if (SelectedBuyer == null) return true;
            RarTurnoverDataWrapper dt = (RarTurnoverDataWrapper)item;
            if (dt.Buyer == SelectedBuyer) return true;
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
          }
}
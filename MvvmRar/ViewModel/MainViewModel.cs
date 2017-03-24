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
//using MvvmDialogs.ViewModels;

namespace MvvmRar.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        #region  - Private Fields -
        private string fileName;
        private readonly IDataService _dataService;
        private bool windowIsVisible;

        private RarFormF6 _RarFile;
        private ObservableCollection<string> alcoCodesList;
        private ObservableCollection<RarCompany> buyersList;
        private ObservableCollection<RarCompany> manufacturersList;
        private ObservableCollection<RarTurnoverData> turnoverDataList;
        private ListCollectionView turnoverDataListCollectionView;
        private RarCompany selectedBuyer;
        private ObservableCollection<RarCompany> savingCompaniesList;

        #endregion


        private ObservableCollection<IDialogViewModel> _Dialogs = new ObservableCollection<IDialogViewModel>();
        public ObservableCollection<IDialogViewModel> Dialogs { get { return _Dialogs; } }

        #region - Public Properties -
        public bool WindowIsVisible {
            get {return windowIsVisible;}
            set {
                windowIsVisible = value;
                RaisePropertyChanged("WindowIsVisible");
            }
        }
        public string FileName
        {
            get
            {
                return fileName;
            }

            set
            {
                fileName = value;
                RaisePropertyChanged("FileName");
                //RaisePropertyChanged("WindowIsVisible");
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

            if (dlg.Show(this.Dialogs))
                FileName = dlg.FileName;
            //    MessageBox(You selected the following file: );
            //new MessageBoxViewModel { Message = "You selected the following file: " + dlg.FileName + "." }.Show(this.Dialogs);
            //else
            //new MessageBoxViewModel { Message = "You didn't select a file." }.Show(this.Dialogs);
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
            //OnPropertyChanged("TurnoverDataList");
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
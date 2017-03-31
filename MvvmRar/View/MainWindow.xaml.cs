using System.Windows;
using MvvmRar.ViewModel;
using System.Windows.Controls;
using System.Windows.Media;
using MvvmRar.View;

namespace MvvmRar
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the MainWindow class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            Closing += (s, e) => ViewModelLocator.Cleanup();
        }

        private bool IsDatePickerUsed(DependencyObject obj)
        {
            if (obj.DependencyObjectType.Name.Equals("DatePicker")) return true;
            else
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                    if (IsDatePickerUsed(child)) return true;

                }
            }
            return false;
        }

        private void dataGridF6_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (e.AddedCells.Count == 0) return;
            var cellInfo = e.AddedCells[0];

            if (cellInfo.Column.GetType().Equals(typeof(DataGridTemplateColumn)))
            {
                DataGridTemplateColumn column = (DataGridTemplateColumn)cellInfo.Column;
                DataTemplate myDataTemplate = column.CellEditingTemplate;
                DependencyObject obj = (DependencyObject)myDataTemplate.LoadContent();
                if (IsDatePickerUsed(obj))
                {
                    dataGridF6.BeginEdit();
                }
            }


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainViewModel vm = DataContext as MainViewModel;
            if (vm!= null && vm.SelectedBuyer!=null)
            {
                //RarCompanyWrapper vm2 = new RarCompanyWrapper(vm.SelectedBuyer);
                RarCompanyView v = new RarCompanyView();
                v.DataContext = vm.SelectedBuyer;
                v.Show();
            }
        }
    }


}
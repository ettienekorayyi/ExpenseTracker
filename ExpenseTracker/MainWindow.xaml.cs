using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ExpenseTracker.Common;
using System.Data;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Data.SqlClient;

using System.Linq;

namespace ExpenseTracker
{
    /// <summary>
    /// Author: Stephen Melben Corral
    /// </summary>
    

    public partial class MainWindow : Window
    {
        public static ListView listview { get; set; }
        private static ComboBox comboBox { get; set; }
        private static DateTime? date { get; set; }
        private int? establishment_Id { get; set; }
        private string store { get; set; }
        private DatePicker picker { get; set; }
        private TransactionHistory trans { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            NewEstablishmentDialog addNewEstablishment = new NewEstablishmentDialog();
            addNewEstablishment.Show();
        }

        private void establishmentCbox_Loaded(object sender, RoutedEventArgs e)
        {
            comboBox = sender as ComboBox;
            this.RefreshComboBox();
        }

        private void historyListView_Loaded(object sender, RoutedEventArgs e)
        {
            DatabaseConnector.ViewData(historyListView);
            listview = historyListView;
        }

        internal void CustomRefresh()
        {
            this.RefreshComboBox();
            DatabaseConnector.ViewData(listview);
        }

        internal void RefreshComboBox()
        {
            DatabaseConnector.PopulateComboBox();
            comboBox.ItemsSource = EstablishmentData.estList;
            
            comboBox.SelectedIndex = -1;
        }
 
        private void establishmentCbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBox.SelectedItem == null)
            {
                establishmentCbox.Text = String.Empty;
            }
            else
            {
                // get the pk id of the selected item
                // pass that id to stored proc
                
                establishmentCbox.Text = comboBox.SelectedItem.ToString();
                establishment_Id = ((EstablishmentData)comboBox.SelectedItem).HiddenValue;
            }
        }
        /// <summary>
        /// For Next Iteration:
        /// total is not included bec the stored proc will be the one to populate this field
        /// change is not included bec the stored proc will be the one to populate this field
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            try
            { 
                DatabaseConnector.UpdataDataFromTransaction
                (
                    Constants.UpdateTransactionRecord,
                    trans.primaryKey,
                    itemTxt.Text,
                    qtyTxt.Text,
                    amountTxt.Text,
                    totalTxt.Text,
                    cashTxt.Text,
                    changeTxt.Text,
                    taxTxt.Text,
                    date.Value.ToShortDateString(),
                    establishment_Id
                );

                btnCreate.Visibility = Visibility.Visible;
                this.CustomRefresh();
                this.Clear();
            }
            catch(System.InvalidOperationException invalidUpdateOperation)
            {
                MessageBox.Show("The " + invalidUpdateOperation.Message + ". Please input values to the fields.", "Record Creation Failed!");
            }
            catch (NullReferenceException nullEx)
            {
                MessageBox.Show("Please select a record from the transaction list to update.", "Record Update Failed!");
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DatabaseConnector.DeleteDataFromTransaction(Constants.DeleteTransactionRecord, int.Parse(trans.primaryKey));
                btnCreate.Visibility = Visibility.Visible;
                this.CustomRefresh();
                this.Clear();
            }
            catch (NullReferenceException nullEx)
            {
                MessageBox.Show("Please select a record from the transaction list to delete.", "Record Deletion Failed!");
            }
        }
        /// <summary>
        /// For Next Iteration:
        /// total is not included bec the stored proc will be the one to populate this field. pending
        /// change is not included bec the stored proc will be the one to populate this field. pending
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateRecord_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DatabaseConnector.InsertDataToTransaction
                (
                    Constants.CreateTransactionRecord,
                    itemTxt.Text,
                    qtyTxt.Text,
                    amountTxt.Text,

                    totalTxt.Text,
                    cashTxt.Text,

                    changeTxt.Text,
                    taxTxt.Text,
                    date.Value.ToShortDateString(),
                    establishment_Id
                );
                this.CustomRefresh();
                this.Clear();
            }
            catch (System.InvalidOperationException invalidCreateOperation)
            {
                MessageBox.Show("The fields are empty" + 
                    ". Please input values to the fields.", "Record Creation Failed!");
            }
        }

        private void datePckr_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            picker = sender as DatePicker;
            date = picker.SelectedDate;
        }

        public void Clear()
        {
            itemTxt.Text = String.Empty;
            qtyTxt.Text  = String.Empty;
            amountTxt.Text  = String.Empty;
                
            totalTxt.Text  = String.Empty;
            cashTxt.Text  = String.Empty;
            changeTxt.Text  = String.Empty;
            
            taxTxt.Text  = String.Empty;
            datePckr.Text = null;
            establishment_Id = null;
        }

        private void historyListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            btnCreate.Visibility = Visibility.Hidden;

            trans = historyListView.SelectedItem as TransactionHistory;
            
            itemTxt.Text = trans.itemName;
            qtyTxt.Text = trans.qty;
            amountTxt.Text = trans.amount;

            totalTxt.Text = trans.total;
            cashTxt.Text = trans.cash;
            changeTxt.Text = trans.change;
            taxTxt.Text = trans.tax;
            datePckr.Text = trans.transDate;

            establishmentCbox.Text = trans.estName;
            establishment_Id = ((EstablishmentData)comboBox.SelectedItem).HiddenValue;
           
        }
    }
    public class EstablishmentData
    {
        public static List<EstablishmentData> estList { get; set; }

        public int estId { get; set; }
        public string establishments { get; set; }

        public EstablishmentData() { }

        public EstablishmentData(int pkId, string eName)
        {
            estId = pkId;
            establishments = eName;
        }

        public int HiddenValue 
        {
            get { return estId; }
        }

        public override string ToString()
        {
            return establishments;
        }
    }

    public class TransactionHistory
    {
        public string primaryKey { get; set; }
        public string itemName { get; set; }
        public string qty { get; set; }
        public string amount { get; set; }
        public string cash { get; set; }
        public string total { get; set; }
        public string change { get; set; }
        public string tax { get; set; }
        public string transDate { get; set; }
        public string estName { get; set; }
    }
}

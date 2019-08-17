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

using ExpenseTracker.Model;
using ExpenseTracker.Classes;

namespace ExpenseTracker
{
    /// <summary>
    /// Author: Stephen Melben Corral
    /// </summary>
    

    public partial class MainWindow : Window
    {
        public static ComboBox comboBox { get; set; }
        public static DateTime? date { get; set; }
        public int? establishment_Id { get; set; }
        public DatePicker picker { get; set; }
        public TransactionHistory transaction { get; set; }

        private Utility _utility;

        public MainWindow()
        {
            InitializeComponent();
            _utility = new Utility();
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            new NewEstablishmentDialog().Show();
        }

        private void establishmentCbox_Loaded(object sender, RoutedEventArgs e)
        {
            comboBox = sender as ComboBox;
            this.RefreshComboBox();
        }

        private void historyListView_Loaded(object sender, RoutedEventArgs e)
        {
            historyListView.ItemsSource = _utility
                .UseTransactionHistory(Constants.SqlServerClient.ToString(),
                 Constants.SqlServerConnection);
        }

        private void CustomRefresh()
        {
            this.RefreshComboBox();
            historyListView.ItemsSource = _utility
                .UseTransactionHistory(Constants.SqlServerClient.ToString(),
                 Constants.SqlServerConnection);
        }

        public void RefreshComboBox()
        {
            comboBox.ItemsSource = _utility
                .UseEstablishment(Constants.SqlServerClient.ToString(),
                 Constants.SqlServerConnection);
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
                establishmentCbox.Text = comboBox.SelectedItem.ToString();
                establishment_Id = ((Establishment)comboBox.SelectedItem).HiddenValue;
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
                string id = transaction.primaryKey;

                _utility.TransactionHistoryUpdate
                (
                   Constants.SqlServerClient.ToString(),
                   Constants.SqlServerConnection,
                   transaction = new TransactionHistory
                   {
                       primaryKey = id,
                       itemName = itemTxt.Text,
                       qty = qtyTxt.Text,
                       amount = amountTxt.Text,
                       total = totalTxt.Text,
                       cash = cashTxt.Text,
                       change = changeTxt.Text,
                       tax = taxTxt.Text,
                       transDate = date.Value.ToShortDateString(),
                       establishment_Id = establishment_Id
                   }
                );

                btnCreate.Visibility = Visibility.Visible;
                this.CustomRefresh();
                this.Clear();
            }
            catch(System.InvalidOperationException invalidUpdateOperation)
            {
                MessageBox.Show(invalidUpdateOperation.Message + ". Please input values to the fields.", "Record Creation Failed!");
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Please select a record from the transaction list to update.", "Record Update Failed!");
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _utility.TransactionHistoryDelete(Constants.SqlServerClient.ToString(),
                   Constants.SqlServerConnection,int.Parse(transaction.primaryKey));

                btnCreate.Visibility = Visibility.Visible;
                this.CustomRefresh();
                this.Clear();
            }
            catch (NullReferenceException nullException)
            {
                MessageBox.Show(nullException.Message, "Record Deletion Failed!");
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
                _utility.TransactionHistoryCreate
                (
                   Constants.SqlServerClient.ToString(),
                   Constants.SqlServerConnection,
                   transaction = new TransactionHistory
                   {
                       itemName = itemTxt.Text,
                       qty = qtyTxt.Text,
                       amount = amountTxt.Text,
                       total = totalTxt.Text,
                       cash = cashTxt.Text,
                       change = changeTxt.Text,
                       tax = taxTxt.Text,
                       transDate = date.Value.ToShortDateString(),
                       establishment_Id = establishment_Id
                   }
                );
                
                this.CustomRefresh();
                this.Clear();
            }
            catch (System.InvalidOperationException)
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

            transaction = historyListView.SelectedItem as TransactionHistory;

            itemTxt.Text   = transaction.itemName;
            qtyTxt.Text    = transaction.qty;
            amountTxt.Text = transaction.amount;
            totalTxt.Text  = transaction.total;
            cashTxt.Text   = transaction.cash;
            changeTxt.Text = transaction.change;
            taxTxt.Text    = transaction.tax;
            datePckr.Text  = transaction.transDate;
            establishmentCbox.Text = transaction.estName;
            establishment_Id = ((Establishment)comboBox.SelectedItem).HiddenValue;

            btnUpdate.IsEnabled = true;
            btnDelete.IsEnabled = true;
            

        }
    }
    

  
}

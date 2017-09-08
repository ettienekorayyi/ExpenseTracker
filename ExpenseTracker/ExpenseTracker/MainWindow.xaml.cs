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
using ExpenseTracker.FactoryPattern;

using ExpenseTracker.DbClasses;

namespace ExpenseTracker
{
    /// <summary>
    /// Author: Stephen Melben Corral
    /// </summary>
    

    public partial class MainWindow : Window
    {
        //public static ListView listview { get; set; }
        private static ComboBox comboBox { get; set; }
        private static DateTime? date { get; set; }
        private int? establishment_Id { get; set; }
        private string store { get; set; }
        private DatePicker picker { get; set; }
        private TransactionHistory transaction { get; set; }

        public MainWindow()
        {
            InitializeComponent();
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
        //done
        private void historyListView_Loaded(object sender, RoutedEventArgs e)
        {
            historyListView.ItemsSource = new Utility()
                .UseTransactionHistory(Constants.SqlServerClient.ToString(),
                 Constants.SqlServerConnection);
        }

        internal void CustomRefresh()// Refactor
        {
            this.RefreshComboBox();
            historyListView.ItemsSource = new Utility()
                .UseTransactionHistory(Constants.SqlServerClient.ToString(),
                 Constants.SqlServerConnection);
            

        }

        internal void RefreshComboBox()
        {
            comboBox.ItemsSource = new Utility()
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
                // get the pk id of the selected item
                // pass that id to stored proc
                
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

                new Utility().TransactionHistoryUpdate
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
                new Utility().TransactionHistoryDelete(Constants.SqlServerClient.ToString(),
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
                new Utility().TransactionHistoryCreate
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
        //
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

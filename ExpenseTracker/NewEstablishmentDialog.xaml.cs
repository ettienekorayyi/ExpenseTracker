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
using System.Windows.Shapes;
using ExpenseTracker.Common;

namespace ExpenseTracker
{
    /// <summary>
    /// Interaction logic for NewEstablishmentDialog.xaml
    /// </summary>
    
   

    public partial class NewEstablishmentDialog : Window
    {
       

        public NewEstablishmentDialog()
        {
            InitializeComponent();
            
        }
        private void btnDialogOk_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
        private void Window_ContentRendered(object sender, EventArgs e)
        {
            
            txtEstablishment.Focus();
        }
       
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            
            DatabaseConnector.InsertDataToEstablishment(Constants.CreateEstablishment, txtEstablishment.Text, txtDescription.Text);
            
            main.RefreshComboBox();
            
            this.Close();
        }
    }
}

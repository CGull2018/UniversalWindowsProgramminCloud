using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.Storage;
using System.Net.Http;
using Newtonsoft.Json;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace InventoryMobileBank
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CreateUser : Page
    {
        public CreateUser()
        {
            this.InitializeComponent();

        }

         IMobileServiceTable<User_Cred> todoTable = App.BankInventory1Client.GetTable<User_Cred>();
        MobileServiceCollection<User_Cred, User_Cred> user;

        private void HyperlinkButton_Click_1(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(UserAuth));

        }

       async  private void btnCreateUser_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                User_Cred user = new User_Cred
                {
                    FirstName = txtBoxFirstName.Text,
                    LastName = txtBoxLastName.Text,
                    UserID = txtBoxUserId.Text,
                    Password = txtBoxPassword.Text,
                    AccountBalance = txtBoxAccountStart.Text,
                };
                await App.BankInventory1Client.GetTable<User_Cred>().InsertAsync(user);
                var dialog = new MessageDialog("Successful!");
                await dialog.ShowAsync();
            }
            catch (Exception em)
            {
                var dialog = new MessageDialog("An Error Occured: " + em.Message);
                await dialog.ShowAsync();
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Microsoft.WindowsAzure.MobileServices.Sync;



// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace InventoryMobileBank
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class UserAuth : Page
    {
        public UserAuth()
        {
            this.InitializeComponent();
            this.CheckDatabase();

        }

        public int IsAuth { get; set; }
        public int IsUser { get; set; }
        public int IsPassword { get; set; }
        public int IsLogin { get; set; }
        public int badCred { get; set; }

        public int IsFirst { get; set; }
        public int IsLast { get; set; }


        IMobileServiceTable<User_Cred> todoTable = App.BankInventory1Client.GetTable<User_Cred>();
        MobileServiceCollection<User_Cred, User_Cred> user;

        //Link Button to Main Page
        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        //Link Button to Create User
        private void HyperlinkButton_Click2(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(CreateUser));
        }

        // Link Button to Creat Bill
        private void HyperlinkButton_Click_3(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ToDoItem));
        }





        // validate there is a current user to the application
        async private void CheckDatabase()
        {
            try
            {
                List<User_Cred> login = await todoTable
                .Where(User_Cred => User_Cred.UserID != null)
                .ToListAsync();

                IsLogin = login.Count();

                var dialog = new MessageDialog("Number of Users in Database " + IsLogin);
                await dialog.ShowAsync();

                if(IsLogin == 0)
                {
                    CreateUser.IsEnabled = true;
                }

            }

            catch (Exception em)
            {
                var dialog = new MessageDialog("An Error Occured: " + em.Message);
                await dialog.ShowAsync();
            }
        }

        // User Login Button
       async private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                List<User_Cred> id = await todoTable
                    .Where(User_Cred => User_Cred.UserID == txtBoxUserId.Text)
                    .ToListAsync();
                List<User_Cred> password = await todoTable
                    .Where(User_Cred => User_Cred.Password == txtBoxPasswd.Text)
                    .ToListAsync();

                IsUser = id.Count();
                IsPassword = password.Count();


                if (IsUser > 0 && IsPassword > 0)
                {
                    badCred = 0;
                    //enable bill list option
                    BillList.IsEnabled = true;

                    // dialog box for authentication 
                    var dialog = new MessageDialog("You are Authenticated!");
                    await dialog.ShowAsync();

                    foreach (var value in id)
                    {
                        txtboxBalance.Text = ("$ ") + value.AccountBalance;

                        var dialog2 = new MessageDialog("User Name: " + value.FirstName + " Starting Account Balance: $" + value.AccountBalance);
                        await dialog2.ShowAsync();
                        
                    }

                }
                else if (IsUser > 0 && IsPassword == 0) {
                    var dialog = new MessageDialog("Invalid Password: ");
                    await dialog.ShowAsync();
                    badCred += 1;
                }

                else if (IsUser == 0 && IsPassword > 0)
                {
                    var dialog = new MessageDialog("Invalid Username: ");
                    await dialog.ShowAsync();
                    badCred += 1;
                }

                else
                {
                    var dialog = new MessageDialog("Account Username and Password Does Not Exist");
                    await dialog.ShowAsync();
                    badCred += 1;
                }

                if(badCred >= 2)
                {
                    var dialog = new MessageDialog("ERROR: Too many invalid attemps");
                    await dialog.ShowAsync();
                    lblForgotCred.Visibility = Visibility;
                    txtBoxFirstName.Visibility = Visibility;
                    txtBoxLastName.Visibility = Visibility;
                    lblFirstName.Visibility = Visibility;
                    lblLastName.Visibility = Visibility;
                    btnLoginCred.Visibility = Visibility;
                }
            }

            catch (Exception em)
            {
                var dialog = new MessageDialog("An Error Occured: " + em.Message);
                await dialog.ShowAsync();
            }
        }

        // validate user first name and last name variables
        async private void CheckUser()
        {
            try
            {
                List<User_Cred> firstName = await todoTable
                .Where(User_Cred => User_Cred.FirstName == txtBoxFirstName.Text)
                .ToListAsync();
                List<User_Cred> lastName = await todoTable
                .Where(User_Cred => User_Cred.LastName == txtBoxLastName.Text)
                .ToListAsync();


                IsFirst = firstName.Count();
                IsLast = lastName.Count();

                if (IsFirst > 0 && IsLast > 0)
                {
                    

                    // dialog box for authentication 
                    var dialog = new MessageDialog("Account Accepted!");
                    await dialog.ShowAsync();

                    foreach (var value in firstName)
                    {
                        txtBoxUserId.Text = value.UserID;
                        txtBoxPasswd.Text = value.Password;

                    }

                }
                else if (IsUser > 0 || IsPassword == 0)
                {
                    var dialog = new MessageDialog("Invalid First Name or Last Name: ");
                    await dialog.ShowAsync();
                }

            }

            catch (Exception em)
            {
                var dialog = new MessageDialog("An Error Occured: " + em.Message);
                await dialog.ShowAsync();
            }
        }

        // pass user first name and last name
        private void btnLoginCred_Click(object sender, RoutedEventArgs e)
        {
            CheckUser();
        }

        //Application Sync Button
        async private void btnSync_Click(object sender, RoutedEventArgs e)
        {
            await RefreshTodoItems();
        }

        // Referesh List Method
        private async Task RefreshTodoItems()
        {
            MobileServiceInvalidOperationException exception = null;
            try
            {

                // This code refreshes the entries in the list view by querying the TodoItems table.
                // The query excludes completed TodoItems
                user = await todoTable
                   .Where(TodoItem => TodoItem.Complete == false)
                   .ToCollectionAsync();

            }
            catch (MobileServiceInvalidOperationException e)
            {
                exception = e;
            }

            if (exception != null)
            {
                await new MessageDialog(exception.Message, "Error loading items").ShowAsync();
            }
            else
            {
                var dialog = new MessageDialog("Successfully Updated");
                await dialog.ShowAsync();
            }
        }


    }
}
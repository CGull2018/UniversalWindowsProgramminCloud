using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;
using Windows.UI.Popups;
using System.Collections.Generic;
using System.Linq;



// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace InventoryMobileBank
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ToDoItem : Page
    {
        public ToDoItem()
        {
            this.InitializeComponent();
            this.CreateUser();
        }
        IMobileServiceTable<TodoItem> todoTable = App.BankInventory1Client.GetTable<TodoItem>();
        MobileServiceCollection<TodoItem, TodoItem> items;

        IMobileServiceTable<User_Cred> UserTable = App.BankInventory1Client.GetTable<User_Cred>();
        MobileServiceCollection<User_Cred, User_Cred> users;


        public int IsUser { get; set; }
        public double startingTotal { get; set; }
        public double cost { get; set; }
        public string balance { get; set; }
        public string payment { get; set; }


        public string AccountBalance { get; private set; }


        // Display user starting balance
        async private void CreateUser()
        {
            try
            {
                List<User_Cred> balance = await UserTable
                .Where(User_Cred => User_Cred.AccountBalance != null)
                .ToListAsync();

                foreach (var value in balance)
                {
                    txtboxBalance.Text = ("$ ") + value.AccountBalance;

                    var dialog2 = new MessageDialog(" Starting Account Balance: $" + value.AccountBalance);
                    await dialog2.ShowAsync();

                }

            }

            catch (Exception em)
            {
                var dialog = new MessageDialog("An Error Occured: " + em.Message);
                await dialog.ShowAsync();
            }
        }

        async private void Submit_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                TodoItem item = new TodoItem
                {
                    
                    BillingCompany = txtBoxBillCompany.Text,
                    Text = txtBoxItem.Text,
                    Type = TypeBox.SelectedItem.ToString(),
                    DateRecieved = DateReceived.Date.ToString(),
                    DateDue = DateDue.Date.ToString(),
                    payment = txtBoxPayment.Text,
                    Complete = false
                };
                await App.BankInventory1Client.GetTable<TodoItem>().InsertAsync(item);
                var dialog = new MessageDialog("Successful!");
                await dialog.ShowAsync();
            }
            catch (Exception em)
            {
                var dialog = new MessageDialog("An Error Occured: " + em.Message);
                await dialog.ShowAsync();
            }
        }

        //Button Refresh List
        async private void btnRefresh_Click_1(object sender, RoutedEventArgs e)
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
                 items = await todoTable
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
                //create checkbox list
                ListItems.ItemsSource = items ;
                this.btnRefresh.IsEnabled = true;
          
            }
        }

        private async void CheckBoxComplete_Checked(object sender, RoutedEventArgs e)
        {

            CheckBox cb = (CheckBox)sender;
            TodoItem item = cb.DataContext as TodoItem;


            var dialog2 = new MessageDialog("Item Payment $" + item.payment);
            await dialog2.ShowAsync();

            double result = 0;

            List<User_Cred> account = await UserTable
           .Where(User_Cred => User_Cred.AccountBalance != null)
           .ToListAsync();

            users = await UserTable
                    .Where(UserTable => UserTable.AccountBalance != null)
                    .ToCollectionAsync();
            foreach (var value in account)
            {
                balance = value.AccountBalance ;
                payment = item.payment;
                startingTotal = Convert.ToDouble(balance);
                cost = Convert.ToDouble(payment);

                result = startingTotal - cost;

                var dialog = new MessageDialog(" New Balance: $" + result);
                await dialog.ShowAsync();
                await UserTable
                .Where(User_Cred => User_Cred.UserID != null).ToListAsync();

                
            }

            txtboxBalance.Text = "$ " + result.ToString();
            await UpdateCheckedTodoItem(item);

        }



        private async Task UpdateCheckedTodoItem(TodoItem item)
        {
            await todoTable.UpdateAsync(item);
            items.Remove(item);
            ListItems.Focus(Windows.UI.Xaml.FocusState.Unfocused);
        }




        //Main Page HyperLink
        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

    }
}

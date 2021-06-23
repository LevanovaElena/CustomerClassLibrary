using CustomerClassLibrary;
using CustomerClassLibrary.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CustomerClassLibrary.WebForm
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CustomerService customerService = new CustomerService();
            Customer customer = customerService.CreateCustomer();
            customer.AddressesList = customerService.CreateAddressList();

            CreateDateOfCustomer(customer);
            
        }

        private void CreateDateOfCustomer(Customer customer)
        {
            txtFirstName.Text = customer.FirstName;
            txtLastName.Text = customer.LastName;
            txtPhoneNumber.Text = customer.PhoneNumber;
            txtTotalPurchasesAmount.Text = customer.TotalPurchasesAmount.ToString();
            txtEmail.Text = customer.Email;
            CreateTableOfAddresses(customer.AddressesList);
            CreateListOfNodes(customer.Notes);
        }

        private void CreateListOfNodes(List<string> notes)
        {
            foreach(string note in notes)
            {
                ListItem item = new ListItem(note);
                listBoxNotes.Items.Add(item);
            }
        }

        private void CreateTableOfAddresses(List<Address> addressesList)
        {
            if (addressesList is null)
            {
                throw new ArgumentNullException(nameof(addressesList));
            }

            if (addressesList.Count > 0)
            {
                // Generate rows and cells.
                
                foreach(Address address in addressesList)
                {
                    TableRow r = new TableRow();

                    Type myType = Type.GetType("Address", false, true);
                    foreach (PropertyInfo prop in typeof(Address).GetProperties())
                    {
                        if (prop.Name != "IdAddress")
                        {
                            TableCell c = new TableCell();
                            c.Controls.Add(new LiteralControl(prop.GetValue(address).ToString()));
                            r.Cells.Add(c);
                        }
                        else
                        {
                            TableCell c = new TableCell();
                            c.Controls.Add(new LiteralControl(prop.GetValue(address).ToString()));
                            c.Visible = false;
                            r.Cells.Add(c);
                        }
                    }

                    Table1.Rows.Add(r);
                }
            }
        }

        public void OnSaveClick(object sender, EventArgs e)
        {
            //save in db
            txtFirstName.Text = "First Name";
            txtLastName.Text = "Last Name";
        }
    }
}
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
    public partial class CustomerEdit : System.Web.UI.Page
    {
        private readonly ICustomerService _customerService;
        public Customer Customer { get; private set; }

        public CustomerEdit()
        {
            _customerService = new CustomerService();
        }
        public CustomerEdit(ICustomerService customerService)
        {
            _customerService = customerService as CustomerService;
        }



        public void LoadCustomer(string idCustomerReq)
        {
            if (idCustomerReq != null)
            {
               int idCustomer= int.Parse(idCustomerReq);
               Customer = _customerService.GetCustomer(idCustomer);
                if (Customer != null) CreateDateOfCustomer(Customer);
            }
            else
            {
                Customer = new Customer()
                {
                    AddressesList = new List<Address>()
                    {
                        new Address()
                    }
                };
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var idCustomer = Request.QueryString["idCustomer"];
                this.LoadCustomer(idCustomer);
            }

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
            listBoxNotes.Items.Clear();
            foreach (string note in notes)
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

                foreach (Address address in addressesList)
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
                            TextBox txt = new TextBox();
                            txt.Text = prop.GetValue(address).ToString();
                            txt.Enabled = false;
                            c.Controls.Add(txt);
                            c.Visible = false;
                            r.Cells.Add(c);
                        }
                    }
                    TableCell c1 = new TableCell();
                    Button btnChange = new Button();
                    btnChange.Text = "Change";
                    c1.Controls.Add(btnChange);
                    r.Cells.Add(c1);
                    Table1.Rows.Add(r);
                }
            }
        }

        public void OnSaveClick(object sender, EventArgs e)
        {
            //save in db

            CreateTableOfAddresses(Customer.AddressesList);
        }
       
    }
}
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

                var idCustomer = Request.QueryString["idCustomer"];
                this.LoadCustomer(idCustomer);

            

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

        private void CreateTableOfAddresses(List<Address> addressesList,bool edit=false)
        {
            if (addressesList is null)
            {
                throw new ArgumentNullException(nameof(addressesList));
            }

            if (addressesList.Count > 0)
            {
                // Generate rows and cells.
                int idAddress = 0;
                var idAddressEdit = Request.QueryString["idAddressEdit"];
                if (idAddressEdit != null) idAddress = int.Parse(idAddressEdit);

                foreach (Address address in addressesList)
                {
                    TableRow r = new TableRow();

                    Type myType = Type.GetType("Address", false, true);
                    foreach (PropertyInfo prop in typeof(Address).GetProperties())
                    {
                        if (prop.Name != "IdAddress"&& prop.Name != "IdCustomer")
                        {
                            TableCell c = new TableCell();
                            TextBox txt = new TextBox();
                            txt.ID = "txt"+prop.Name + address.IdAddress.ToString();
                            txt.Text = prop.GetValue(address).ToString();
                            if (address.IdAddress == idAddress)
                            {
                                txt.Enabled = true;
                                txt.CssClass = "form-control";
                            }
                            else
                            {
                                txt.Enabled = false;
                                txt.CssClass = "form-label";
                            }
                            c.Controls.Add(txt);
                            r.Cells.Add(c);
                        }
                        else
                        {
                            TableCell c = new TableCell();
                            TextBox txt = new TextBox();
                            txt.ID = "txt" + prop.Name + address.IdAddress.ToString();
                            txt.Text = prop.GetValue(address).ToString();
                            if (address.IdAddress == idAddress)
                            {
                                txt.Enabled = true;
                                txt.CssClass = "form-control";
                            }
                            else txt.Enabled = false;
                            txt.CssClass = "form-control";
                            c.Controls.Add(txt);
                            c.Visible = false;
                            r.Cells.Add(c);
                        }
                    }
                    TableCell c1 = new TableCell();
                   Button btnChange = new Button();
                    if (address.IdAddress == idAddress)  btnChange.Text = "Save";
                    else btnChange.Text = "Edit";
                    btnChange.CssClass = "btn btn-success mr-1";
                    btnChange.Click += OnEditAddress;
                    btnChange.ID = "btnEdit_" + address.IdAddress.ToString();
                    c1.Controls.Add(btnChange);
                    r.Cells.Add(c1);

                    Button btnDelete = new Button();
                    btnDelete.Text = "Delete";
                    btnDelete.CssClass = "btn btn-danger";
                    c1.Controls.Add(btnDelete);
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
        public void OnEditAddress(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            btn.Text = "Save";
            var buttonId = btn.ID;
            string idAddress = buttonId.Split('_')[1];
            Response.Redirect("CustomerEdit.aspx?idCustomer=" + Customer.IdCustomer + " &idAddressEdit="+ idAddress);
        }


    }
}
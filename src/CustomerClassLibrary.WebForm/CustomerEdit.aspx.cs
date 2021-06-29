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
        public Customer Customer { get; private set; } = new Customer();

        public string IdAddressEdit { get; set; } = "";

        public CustomerEdit()
        {
            _customerService = new CustomerService();
        }
        public CustomerEdit(ICustomerService customerService)
        {
            _customerService = customerService as CustomerService;
        }


        protected override void LoadViewState(object savedState)
        {
            base.LoadViewState(savedState);

            Customer = ViewState["MyObject"] as Customer;


            if (Customer != null)
            {
                Repeater.DataSource = GetValidationListAddress(Customer.AddressesList);
                Repeater.DataBind();
            }
        }
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            Repeater.DataBind();
            DataBind();
            ViewState["MyObject"] = Customer;
        }

        public void LoadCustomer(string idCustomerReq)
        {
            if (idCustomerReq != null)
            {
                int idCustomer = int.Parse(idCustomerReq);
                Customer = _customerService.GetCustomer(idCustomer);

                Customer.IdCustomer = idCustomer;
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
            Repeater.DataSource = GetValidationListAddress(Customer.AddressesList);
            repeaterNotes.DataSource = Customer.Notes;

        }

        public List<AddressWithValidationField> GetValidationListAddress(List<Address> addresses)
        {
            List<AddressWithValidationField> newListAddress = new List<AddressWithValidationField>();
            foreach (Address address in addresses)
            {
                newListAddress.Add(new AddressWithValidationField(address));
            }
            return newListAddress;
        }
        protected override void OnPreLoad(EventArgs e)
        {


        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var idCustomer = Request.QueryString["idCustomer"];
                this.LoadCustomer(idCustomer);
            }
            else
            {
                ReadValues();
            }

        }

        public void ReadValues()
        {
            int id = 100;
            int.TryParse(txtIdCustomer.Text, out id);
            Customer.IdCustomer = id;
            Customer.FirstName = txtFirstName.Text;
            Customer.LastName = txtLastName.Text;
            Customer.PhoneNumber = txtPhoneNumber.Text;
            Customer.Email = txtEmail.Text;
            int amount;
            int.TryParse(txtTotalPurchasesAmount.Text, out amount);
            Customer.TotalPurchasesAmount = amount;
            Customer.AddressesList = ReadAddresses();
            Customer.Notes = ReadNotes();
            Repeater.DataSource = GetValidationListAddress(Customer.AddressesList);
            Repeater.DataBind();
            repeaterNotes.DataSource = Customer.Notes;
            repeaterNotes.DataBind();
        }

        private List<string> ReadNotes()
        {
            var list = new List<string>();
            for (var index = 0; index < repeaterNotes.Items.Count; index++)
            {
                var item = repeaterNotes.Items[index];

               list.Add( ((TextBox)item.FindControl("Notes")).Text);
            }
            return list;
        }

        private List<Address> ReadAddresses()
        {
            var list = new List<Address>();

            for (var index = 0; index < Repeater.Items.Count; index++)
            {
                var item = Repeater.Items[index];
                var address = new Address();

                address.AddressLine = ((TextBox)item.FindControl("AddressLine")).Text;
                address.AddressLine2 = ((TextBox)item.FindControl("AddressLine2")).Text;
                //address.TypeAddress =(AddressType) ((TextBox)item.FindControl("TypeAddress")).Text;
                address.State = ((TextBox)item.FindControl("State")).Text;
                address.Country = ((TextBox)item.FindControl("Country")).Text;
                address.City = ((TextBox)item.FindControl("City")).Text;
                address.PostalCode = ((TextBox)item.FindControl("PostalCode")).Text;
                address.AddressLine2 = ((TextBox)item.FindControl("AddressLine2")).Text;

                address.IdAddress = int.Parse(((TextBox)item.FindControl("IdAddress")).Text);
                address.IdCustomer= int.Parse(((TextBox)item.FindControl("IdCustomer")).Text);
                list.Add(address);
            }
            return list;
        }
        private void CreateDateOfCustomer(Customer customer)
        {
            txtFirstName.Text = customer.FirstName;
            txtLastName.Text = customer.LastName;
            txtPhoneNumber.Text = customer.PhoneNumber;
            txtTotalPurchasesAmount.Text = customer.TotalPurchasesAmount.ToString();
            txtEmail.Text = customer.Email;
            txtIdCustomer.Text = customer.IdCustomer.ToString();

            CreateListOfNodes(customer.Notes);
        }

        private void CreateListOfNodes(List<string> notes)
        {
            repeaterNotes.DataSource = Customer.Notes;
            repeaterNotes.DataBind();
        }

        public void OnSaveClick(object sender, EventArgs e)
        {
            //save in db
            Customer customer = new Customer();
            int id = 100;
            int.TryParse(txtIdCustomer.Text, out id);
            customer.IdCustomer = id;
            customer.FirstName = txtFirstName.Text;
            customer.LastName = txtLastName.Text;
            customer.PhoneNumber = txtPhoneNumber.Text;
            customer.Email = txtEmail.Text;
            int amount;
            int.TryParse(txtTotalPurchasesAmount.Text, out amount);
            customer.TotalPurchasesAmount = amount;
            customer.AddressesList = ReadAddresses();
            customer.Notes = ReadNotes();

            List<AddressWithValidationField> listValidateAddress = new List<AddressWithValidationField>();
            bool isUpdate = true;
            TextErrorClear();
            var resultValidation = _customerService.Validate(customer);
            if (resultValidation.Count > 0)
            {
                isUpdate = false;
                foreach (var error in resultValidation)
                {
                    switch (error.Item1)
                    {
                        case "FirstName":
                            txtFirstNameError.Text = error.Item2;
                            break;
                        case "LastName":
                            txtLastNameError.Text = error.Item2;
                            break;
                        case "PhoneNumber":
                            txtPhoneNumberError.Text = error.Item2;
                            break;
                        case "Email":
                            txtEmailError.Text = error.Item2;
                            break;
                        case "TotalPurchasesAmount":
                            txtTotalPurchasesAmountError.Text = error.Item2;//txtNotesError
                            break;
                        case "Notes":
                            txtNotesError.Text = error.Item2;//txtNotesError
                            break;
                    }
                }
            }

            foreach (Address address in customer.AddressesList)
            {
                AddressWithValidationField addressWithValidation = new AddressWithValidationField(address);
                addressWithValidation.ValidatorAddress(address);
                if (addressWithValidation.IsError) isUpdate = false;
                listValidateAddress.Add(addressWithValidation);
            }
             
            if(isUpdate)_customerService.UpdateCustomer(customer);
            
            this.Customer = customer;
            if(isUpdate)Repeater.DataSource = GetValidationListAddress(Customer.AddressesList);
            else Repeater.DataSource =listValidateAddress;
            Repeater.DataBind();
        }

        public void TextErrorClear()
        {
            txtEmailError.Text = "";
            txtFirstNameError.Text = "";
            txtLastNameError.Text = "";
            txtNotesError.Text = "";
            txtPhoneNumberError.Text = "";
            txtTotalPurchasesAmountError.Text = "";
        }

        public void OnSaveAddress(object sender, EventArgs e)
        {
            Customer.AddressesList.Add(new Address()
            {
                IdCustomer = Customer.IdCustomer
            });
            Repeater.DataSource =GetValidationListAddress( Customer.AddressesList);
            Repeater.DataBind();
        }

        public void OnSaveNotes(object sender, EventArgs e)
        {
            Customer.Notes.Add("");
            repeaterNotes.DataSource = Customer.Notes;
            repeaterNotes.DataBind();
        }

        public void OnDeleteAddress(object sender, EventArgs e)
        {
            Button btn=(Button) sender;
            txtEmail.Text = btn.ID;
            
         }

        protected void Repeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "deleteAddress")
            {
                // Determine the CategoryID
                int idAddress = Convert.ToInt32(e.CommandArgument);
                Address removeAddress = null;
                
                foreach(Address address in Customer.AddressesList)
                {
                    if (address.IdAddress == idAddress)
                    {
                        removeAddress = address;
                        break;
                    }
                }
                Customer.AddressesList.Remove(removeAddress);
                Repeater.DataSource = GetValidationListAddress( Customer.AddressesList);
                Repeater.DataBind();

            }
        }

        protected void repeaterNotes_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "deleteNote")
            {
                // Determine the CategoryID
                string note = Convert.ToString(e.CommandArgument);

                Customer.Notes.Remove(note);

                repeaterNotes.DataSource = Customer.Notes;
                repeaterNotes.DataBind();

            }
        }

    }
}
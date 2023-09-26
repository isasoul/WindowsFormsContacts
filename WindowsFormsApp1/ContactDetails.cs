using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using WindowsFormsApp1;

namespace WindowsFormsContacts
{
    public partial class ContactDetails : Form
    {
        private BussinesLogicLayer _bussinesLogicLayer;
        private Contact _contact;

        public ContactDetails()
        {
            InitializeComponent(); 
            _bussinesLogicLayer = new BussinesLogicLayer();
        }
        #region EVENTS
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveContact();
            this.Close();
            ((Main)this.Owner).PopulateContacts();


        }
        #endregion

        #region PRIVATE METHODS
        private void SaveContact()
        {
            Contact contact = new Contact();
            contact.FisrtName = txtFirstName.Text;
            contact.LastName = txtLastName.Text;
            contact.Phone = txtPhone.Text;
            contact.Address = txtAddress.Text;

            //de esta forma cubrimos el escenario de que no se nos duplique el contacto al editarlo con un nuevo id
            contact.Id = _contact != null ? _contact.Id : 0;

            _bussinesLogicLayer.SaveContact(contact);

        }

        // metodo para limpiar las cajas de texto y esten vacias antesd de ser llenadas
        private void ClearForm()
        {
            txtFirstName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtPhone.Text = string.Empty;
            txtAddress.Text = string.Empty;
        }
        #endregion

        #region PUBLIC METHODS
        public void LoadContact(Contact contact)
        {
            //de esta forma al hacer save en contact detail vamos a tener el id en nuestro objeto contactos
            _contact = contact;
            if (contact != null)
            {
                txtFirstName.Text = contact.FisrtName;
                txtLastName.Text = contact.LastName;
                txtPhone.Text = contact.Phone;
                txtAddress.Text = contact.Address;
            }
        }

        #endregion



    }
}

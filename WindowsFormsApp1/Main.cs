using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1;

namespace WindowsFormsContacts
{
    public partial class Main : Form
    {
        private BussinesLogicLayer _bussinesLogicLayer;
        public Main()
        {
            InitializeComponent();
            _bussinesLogicLayer = new BussinesLogicLayer();
        }
     

        private void label1_Click(object sender, EventArgs e)
        {

        }

      
        
        
        #region EVENTS
         private void btnAdd_Click(object sender, EventArgs e)
        {
            OpenContactDetailsDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            PopulateContacts();
        }

        private void gridContacts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //de esta forma obtengo la celda que han clickeado sobre edit ,asi me aseguro que fue esa celda clickeada
            DataGridViewLinkCell cell = (DataGridViewLinkCell)gridContacts.Rows[e.RowIndex].Cells[e.ColumnIndex];
            if (cell.Value.ToString() == "Edit")
            {
                ContactDetails contactDetails = new ContactDetails();
                contactDetails.LoadContact(new Contact
                {
                    Id = int.Parse(gridContacts.Rows[e.RowIndex].Cells[0].Value.ToString()),
                    FisrtName = gridContacts.Rows[e.RowIndex].Cells[1].Value.ToString(),
                    LastName = gridContacts.Rows[e.RowIndex].Cells[2].Value.ToString(),
                    Phone = gridContacts.Rows[e.RowIndex].Cells[3].Value.ToString(),
                    Address = gridContacts.Rows[e.RowIndex].Cells[4].Value.ToString(),
                });
                //para que cuando haga click en el save se cierre el dialogo y se edite el valor
                contactDetails.ShowDialog(this);

            }
            else if (cell.Value.ToString() == "Delete")
            {
                DeleteContact(int.Parse(gridContacts.Rows[e.RowIndex].Cells[0].Value.ToString()));
                PopulateContacts();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            PopulateContacts(textSearch.Text);
            textSearch.Text = string.Empty;
        }
        #endregion

        #region PRIVATE METHODS
        private void OpenContactDetailsDialog()
        {
            ContactDetails contactDetails = new ContactDetails();
            contactDetails.ShowDialog(this);
        }

        private void DeleteContact(int id)
        {
            _bussinesLogicLayer.DeleteContact(id);
        }

        #endregion

        #region PUBLIC METHODS
        public void PopulateContacts(string searchText = null)
        {
            List<Contact> contacts = _bussinesLogicLayer.GetContacts(searchText);
            gridContacts.DataSource = contacts;
        }


        #endregion






    }
}

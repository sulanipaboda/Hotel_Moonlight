using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AMRConnector;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace Hotel_Management_System.User_Control
{
    public partial class UserControlClient : UserControl

    {
        DbConnector db;
        private string ID = "";

        public UserControlClient()
        {
            InitializeComponent();
            db = new DbConnector(); 
        }

        public void Clear()
        {
            textBoxAddFirstname.Clear();
            textBoxAddLastname.Clear();
            textBoxAddPhoneno.Clear();
            textBoxAddAddress.Clear();
            tabControlClient.SelectedTab = tabPageAddClient;
        }

        private void Clear1()
        {
            textBoxUDFirstname.Clear();
            textBoxUDLastname.Clear();
            textBoxUDPhoneno.Clear();
            textBoxUDAddress.Clear();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            bool check;
            if(textBoxAddFirstname.Text.Trim() == string.Empty || textBoxAddLastname.Text.Trim() == string.Empty || 
                textBoxAddPhoneno.Text.Trim() == string.Empty || textBoxAddAddress.Text.Trim() == string.Empty)
                MessageBox.Show("Please fill out all the fields.", "Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                check = db.AddClient(textBoxAddFirstname.Text.Trim(), textBoxAddLastname.Text.Trim(), textBoxAddPhoneno.Text.Trim(), textBoxAddAddress.Text.Trim());
                if (check)
                    Clear();

            }
        }

        private void tabPageAddClient_Leave(object sender, EventArgs e)
        {
            Clear();
            Clear1();
        }

        private void tabPageSearchClient_Enter(object sender, EventArgs e)
        {
            db.DisplayAndSearch("SELECT * FROM Client_Table", dataGridViewClient);
        }

        private void tabPageSearchClient_Leave(object sender, EventArgs e)
        {
            textBoxSearchPhoneno.Clear();
        }

        private void textBoxSearchPhoneno_TextChanged(object sender, EventArgs e)
        {
            db.DisplayAndSearch("SELECT * FROM Client_Table WHERE Client_PhoneNo LIKE '%" + textBoxSearchPhoneno.Text + "%'", dataGridViewClient);
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            bool check;
            if (ID != "")
            {
                if (textBoxUDFirstname.Text.Trim() == string.Empty || textBoxUDLastname.Text.Trim() == string.Empty ||
                    textBoxUDPhoneno.Text.Trim() == string.Empty || textBoxUDAddress.Text.Trim() == string.Empty)
                    MessageBox.Show("Please fill out all the fields.", "Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    check = db.UpdateClient(ID, textBoxUDFirstname.Text.Trim(), textBoxUDLastname.Text.Trim(), textBoxAddPhoneno.Text.Trim(), textBoxAddAddress.Text.Trim());
                    if (check)
                        Clear1();
                }
            }
            else
                MessageBox.Show("Please select a row from table", "Select a Row", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            bool check;
            if (ID != "")
            {
                if (textBoxUDFirstname.Text.Trim() == string.Empty || textBoxUDLastname.Text.Trim() == string.Empty ||
                    textBoxUDPhoneno.Text.Trim() == string.Empty || textBoxUDAddress.Text.Trim() == string.Empty)
                    MessageBox.Show("Please fill out all the fields.", "Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    DialogResult result = MessageBox.Show("Do you want to delete this client?", "Delete Client", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (DialogResult.Yes == result)
                    {
                        check = db.DeleteClient(ID);
                        if (check)
                            Clear1();
                    }

                }
            }
            else
                MessageBox.Show("Please select a row from table", "Select a Row", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void tabPageUpdateAndDeleteClient_Leave(object sender, EventArgs e)
        {
            Clear1();
        }

        private void dataGridViewClient_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex != -1)
            {
                DataGridViewRow row = dataGridViewClient.Rows[e.RowIndex];
                ID = row.Cells[0].Value.ToString();
                textBoxUDFirstname.Text = row.Cells[1].Value.ToString();
                textBoxUDLastname.Text = row.Cells[2].Value.ToString();
                textBoxUDPhoneno.Text = row.Cells[3].Value.ToString();
                textBoxUDAddress.Text = row.Cells[4].Value.ToString();
            }
        }

       
    }
    
}

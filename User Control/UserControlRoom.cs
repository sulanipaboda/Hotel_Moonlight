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

namespace Hotel_Management_System.User_Control
{
    public partial class UserControlRoom : UserControl
    {
        DbConnector db;
        private string roomNo = "", free = "";

        public UserControlRoom()
        {
            InitializeComponent();
            db = new DbConnector();
        }
           
        public void Clear()
        {
            comboBoxAddType.SelectedIndex = -1;
            textBoxAddPhoneno.Clear();
            radioButtonAddNo.Checked = false;
            radioButtonAddYes.Checked = false;
            tabControlRoom.SelectedTab = tabPageAddRoom;
        }

        private void Clear1()
        {
            comboBoxUDType.SelectedIndex = -1;
            textBoxUDPhoneno.Clear();
            radioButtonUDNo.Checked = false;
            radioButtonUDYes.Checked = false;
            roomNo = "";
        }

        private void tabPageAddRoom_Leave(object sender, EventArgs e)
        {
            Clear();
            Clear1();
        }

        private void tabPageSearchRoom_Enter(object sender, EventArgs e)
        {
            db.DisplayAndSearch("SELECT * FROM Room_Table", dataGridViewRoom);
        }

        private void tabPageSearchRoom_Leave(object sender, EventArgs e)
        {
            textBoxSearchRoomno.Clear();
        }

        private void textBoxSearchRoomno_TextChanged(object sender, EventArgs e)
        {
            db.DisplayAndSearch("SELECT * FROM Room_Table WHERE Room_Number LIKE '%" + textBoxSearchRoomno.Text + "%'", dataGridViewRoom);
        }

        private void dataGridViewRoom_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataGridViewRow row = dataGridViewRoom.Rows[e.RowIndex];
                roomNo = row.Cells[0].Value.ToString();
                comboBoxUDType.SelectedItem = row.Cells[1].Value.ToString();
                textBoxUDPhoneno.Text = row.Cells[2].Value.ToString();
                free = row.Cells[3].Value.ToString();
                if(free == "Yes")
                    radioButtonUDYes.Checked = true;
                if (free == "No")
                    radioButtonUDNo.Checked = true;

            }
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (radioButtonUDYes.Checked)
                free = "Yes";
            if (radioButtonUDNo.Checked)
                free = "No";
            bool check;
            if (roomNo != "")
            {
                if (comboBoxUDType.SelectedIndex == -1 || textBoxUDPhoneno.Text.Trim() == string.Empty || free == "")
                    MessageBox.Show("Please fill out all the fields.", "Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    check = db.UpdateRoom(roomNo, comboBoxUDType.Text.ToString(), textBoxUDPhoneno.Text.Trim(), free);
                    if (check)
                        Clear1();
                }
            }
            else
                MessageBox.Show("Please select a row from table", "Select a Row", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (radioButtonUDYes.Checked)
                free = "Yes";
            if (radioButtonUDNo.Checked)
                free = "No";
            bool check;
            if (roomNo != "")
            {
                if (comboBoxUDType.SelectedIndex == -1 || textBoxUDPhoneno.Text.Trim() == string.Empty || free == "")
                    MessageBox.Show("Please fill out all the fields.", "Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    DialogResult result = MessageBox.Show("Are you want to delete this room?", "Delete Room", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (DialogResult.Yes == result)
                    {
                        check = db.DeleteRoom(roomNo);
                        if (check)
                            Clear1();
                    }

                }
            }
            else
                MessageBox.Show("Please select a row from table", "Select a Row", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void tabPageUpdateAndDeleteRoom_Leave(object sender, EventArgs e)
        {
            Clear1();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (radioButtonAddYes.Checked)
                free = "Yes";
            if (radioButtonAddNo.Checked)
                free = "No";
            bool check;
            if (comboBoxAddType.SelectedIndex == -1 || textBoxAddPhoneno.Text.Trim() == string.Empty || free == "")
                MessageBox.Show("Please fill out all the fields.", "Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                check = db.AddRoom(comboBoxAddType.Text.ToString(), textBoxAddPhoneno.Text.Trim(), free);
                if (check)
                    Clear();

            }
        }

    }
}

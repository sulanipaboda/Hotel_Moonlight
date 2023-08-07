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
    public partial class UserControlReservation : UserControl
    {
        DbConnector db;
        private string resID = "", roomNo; 
        public UserControlReservation()
        {
            InitializeComponent();
            db = new DbConnector();
        }

        public void Clear()
        {
            comboBoxAddType.SelectedIndex = -1;
            comboBoxAddRoomNo.SelectedIndex = -1;
            textBoxAddClientID.Clear();
            dateTimePickerAddCheckIn.Value = DateTime.Now;
            dateTimePickerAddCheckOut.Value = DateTime.Now;
            tabControlReservation.SelectedTab = tabPageAddReservation;
        }

        private void UserControlReservation_Load(object sender, EventArgs e)
        {
            comboBoxAddType.SelectedIndex = -1;
            comboBoxAddRoomNo.SelectedIndex = -1;
            comboBoxUDType.SelectedIndex = -1;
            comboBoxUDRoomNo.SelectedIndex = -1;
        }

        private void tabPageAddReservation_Leave(object sender, EventArgs e)
        {
            Clear();
            Clear1();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            bool check;
            if (comboBoxAddType.SelectedIndex == -1 || comboBoxAddRoomNo.SelectedIndex == -1 || textBoxAddClientID.Text.Trim() == string.Empty)
                MessageBox.Show("Please fill out all the fields.", "Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                check = db.AddReservation(comboBoxAddType.SelectedIndex.ToString(), comboBoxAddRoomNo.SelectedIndex.ToString(), textBoxAddClientID.Text.Trim(), dateTimePickerAddCheckIn.Text, dateTimePickerAddCheckOut.Text);
                db.UpdateReservationRoom(comboBoxAddRoomNo.SelectedIndex.ToString(), "No");
                if (check)
                    Clear();

            }
        }

        private void tabPageSearchReservation_Enter(object sender, EventArgs e)
        {
            db.DisplayAndSearch("SELECT * FROM Reservation_Table", dataGridViewReservation);
        }

        private void tabPageSearchReservation_Leave(object sender, EventArgs e)
        {
            textBoxSearchClientID.Clear();
        }

        private void textBoxSearchClientID_TextChanged(object sender, EventArgs e)
        {
            db.DisplayAndSearch("SELECT * FROM Reservation_Table WHERE Reservation_ClientID LIKE '%" + textBoxSearchClientID.Text + "%'", dataGridViewReservation);
        }

        private void dataGridViewReservation_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataGridViewRow row = dataGridViewReservation.Rows[e.RowIndex];
                resID = row.Cells[0].Value.ToString();
                comboBoxUDType.SelectedItem = row.Cells[1].Value.ToString();
                roomNo = row.Cells[2].Value.ToString();
                textBoxUDClientID.Text = row.Cells[4].Value.ToString();
                dateTimePickerUDCheckIn.Text = row.Cells[5].Value.ToString();
                dateTimePickerUDCheckOut.Text = row.Cells[6].Value.ToString();
            }
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            bool check;
            if (resID != "")
            {
                if (comboBoxUDType.SelectedIndex == -1 || comboBoxUDRoomNo.SelectedIndex == -1 || textBoxUDClientID.Text.Trim() == string.Empty)
                    MessageBox.Show("Please fill out all the fields.", "Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    check = db.UpdateReservation(resID, comboBoxUDType.SelectedIndex.ToString(), comboBoxUDRoomNo.SelectedIndex.ToString(), textBoxUDClientID.Text.Trim(), dateTimePickerUDCheckIn.Text, dateTimePickerUDCheckOut.Text);
                    db.UpdateReservationRoom(roomNo, "Yes");
                    db.UpdateReservationRoom(comboBoxUDRoomNo.SelectedItem.ToString(), "No");
                    if (check)
                        Clear1();
                }
            }
            else
                MessageBox.Show("Please select a row from table", "Select a Row", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            bool check;
            if (resID != "")
            {
                if (comboBoxUDType.SelectedIndex == -1 || textBoxUDClientID.Text.Trim() == string.Empty)
                    MessageBox.Show("Please fill out all the fields.", "Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    DialogResult result = MessageBox.Show("Do you want to cancel this reservation?", "Cancel Reservation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (DialogResult.Yes == result)
                    {
                        check = db.DeleteReservation(resID);
                        db.UpdateReservationRoom(roomNo, "Yes");
                        if (check)
                            Clear1();
                    }

                }
            }
            else
                MessageBox.Show("Please select a row from table", "Select a Row", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void tabPageUpdateAndCancelReservation_Leave(object sender, EventArgs e)
        {
            Clear1();
        }

        private void comboBoxAddType_SelectedIndexChanged(object sender, EventArgs e)
        {
            db.RoomTypeAndNo("SELECT Room_Number FROM Room_Table WHERE Room_Type = '" + comboBoxAddType.SelectedItem.ToString() + "' AND Room_Free = 'Yes' ORDER BY Room_Number", comboBoxAddRoomNo);
        }

        private void comboBoxUDType_SelectedIndexChanged(object sender, EventArgs e)
        {
            db.RoomTypeAndNo("SELECT Room_Number FROM Room_Table WHERE Room_Type = '" + comboBoxUDType.SelectedItem.ToString() + "' AND Room_Free = 'Yes' ORDER BY Room_Number", comboBoxUDRoomNo);
        }

        private void Clear1()
        {
            comboBoxUDType.SelectedIndex = 0;
            comboBoxUDRoomNo.SelectedIndex = -1;
            textBoxUDClientID.Clear();
            dateTimePickerUDCheckIn.Value = DateTime.Now;
            dateTimePickerUDCheckOut.Value = DateTime.Now;
            tabControlReservation.SelectedTab = tabPageAddReservation;
        }
    }
}

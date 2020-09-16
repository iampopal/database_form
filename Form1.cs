using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DatabaseForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int selectedId = 0;
        private void saveButton_Click(object sender, EventArgs e)
        {
            if (updateButton.Enabled)
            {
                saveButton.Text = "Save";
                updateButton.Enabled = false;
                deleteButton.Enabled = false;
                clearTextBoxies();
            }
            else
            {
                DBUtils.insertStudent(nameTextBox.Text.ToString(), lastNameTextBox.Text.ToString(), Double.Parse(feeTextBox.Text.ToString()));
                dataGridView1.DataSource = DBUtils.getStudents();
                clearTextBoxies();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = DBUtils.getStudents();
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            saveButton.Text = "New";
            updateButton.Enabled = true;
            deleteButton.Enabled = true;

            String id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            String name = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            String lastName = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            String fee = dataGridView1.CurrentRow.Cells[3].Value.ToString();

            selectedId = int.Parse(id);
            nameTextBox.Text = name;
            lastNameTextBox.Text = lastName;
            feeTextBox.Text = fee;
        }

        void clearTextBoxies()
        {
            nameTextBox.Clear();
            lastNameTextBox.Clear();
            feeTextBox.Clear();
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            DBUtils.updateStudent(selectedId, nameTextBox.Text.ToString(), lastNameTextBox.Text.ToString(), Double.Parse(feeTextBox.Text.ToString()));
            dataGridView1.DataSource = DBUtils.getStudents();
            clearTextBoxies();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure want to delete", "Delete", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                DBUtils.deleteStudent(selectedId);
                dataGridView1.DataSource = DBUtils.getStudents();
                clearTextBoxies();
                saveButton.Text = "Save";
                updateButton.Enabled = false;
                deleteButton.Enabled = false;
                clearTextBoxies();
            }
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = DBUtils.searchStudent(searchTextBox.Text.ToString());
        }
    }
}

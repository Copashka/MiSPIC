using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiSPIC
{
    public partial class AdminUsersPanelForm : Form
    {
        public AdminUsersPanelForm()
        {
            InitializeComponent();
        }

        private void userListBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.userListBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.frogmoneyCasinoDatabaseDataSet);

        }

        private void AdminPanelForm_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "frogmoneyCasinoDatabaseDataSet.UserList". При необходимости она может быть перемещена или удалена.
            this.userListTableAdapter.Fill(this.frogmoneyCasinoDatabaseDataSet.UserList);

        }
    }
}

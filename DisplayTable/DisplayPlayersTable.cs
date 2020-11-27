using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;
using System.Data.Entity.Validation;

namespace DisplayTable
{
    public partial class DisplayPlayersTable : Form
    {
        private BaseBallDatabase.BaseballEntities dbContext = new BaseBallDatabase.BaseballEntities();
        public DisplayPlayersTable()
        {
            InitializeComponent();
            queryBox.Items.Add("SELECT PlayerID.LastName, FirstName, BattleAverage FROM Player");
            queryBox.Items.Add("SELECT LastName, FirstName, BattleAverage FROM Player WHERE (BattleAverage >= 0.300)");
            queryBox.Items.Add("SELECT PlayerID, LastName, FirstName, BattleAverage FROM Player WHERE (LastName = \"Hernandez\")");


        }

        

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void DisplayPlayersTable_Load(object sender, EventArgs e)
        {
            dbContext.Players
                .OrderBy(player => player.LastName)
                .ThenBy(player => player.FirstName)
                .Load();

            playerBindingSource1.DataSource = dbContext.Players.Local;
        }

        private void Save_Click(object sender, EventArgs e)
        {
            Validate();
            playerBindingSource1.EndEdit();
            try
            {
                dbContext.SaveChanges();
            }
            catch(DbEntityValidationException)
            {
                MessageBox.Show("FirstName and LastName must contain values", "Error");
            }
        }
    }
}

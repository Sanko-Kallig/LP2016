using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Administration_Sloepke
{
    public partial class HiringContractForm : Form
    {
        private Administration Admin;
        private HiringContract hiringContract;
        public HiringContractForm(Administration admin)
        {
            InitializeComponent();
            this.hiringContract = new HiringContract();
            this.Admin = admin;
            Admin.Products = Admin.GetProducts();
            Admin.Boats = Admin.GetBoats();
            Admin.WaterEntities = Admin.GetWaterEntities();
            cbxNamedWaterEntity.DataSource = Admin.WaterEntities.FindAll(w => w.Name != null);
            cbxNamedWaterEntity.Refresh();
            Admin.TempMotorBoats = Admin.GetMotorBoats();
            Admin.TempMuscleBoats = Admin.GetMuscleBoats();
            UpdateList();
        }

        private void UpdateList()
        {
            lbxMotorBoot.DataSource = Admin.TempMotorBoats;
            lbxMuscleBoat.DataSource = Admin.TempMuscleBoats;
            btnSaveContract.DataSource = Admin.Products;
            lbxMotorBoot.Refresh();
            lbxMuscleBoat.Refresh();
            if (hiringContract.Boats != null)
            {
                if (hiringContract.GetMuscleBoats() != null)
                {
                    lbxContractMuscle.DataSource = hiringContract.GetMuscleBoats();

                }
                if (hiringContract.GetMotorBoats() != null)
                {
                    lbxContractMotor.DataSource = hiringContract.GetMotorBoats();
                }
                if (hiringContract.Products != null)
                {
                    lbxChosenProducts.DataSource = hiringContract.Products;
                }
                if (hiringContract.WaterEntities != null)
                {
                    lbxWaterEntity.DataSource = hiringContract.WaterEntities.FindAll(w => w.Name != null);
                }
                lbxContractMuscle.Refresh();
                lbxContractMotor.Refresh();
                lbxChosenProducts.Refresh();
                lbxWaterEntity.Refresh();


            }
        }

        private void lbxMotorBoot_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnMuscleAdd_Click(object sender, EventArgs e)
        {
            hiringContract.Boats.Add(Admin.TempMuscleBoats[lbxMuscleBoat.SelectedIndex]);
            UpdateList();
        }

        private void btnMotorAdd_Click(object sender, EventArgs e)
        {
            hiringContract.Boats.Add(Admin.TempMotorBoats[lbxMotorBoot.SelectedIndex]);
            UpdateList();
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            hiringContract.Products.Add(Admin.Products[lbxChosenProducts.SelectedIndex]);
            UpdateList();
        }

        private void btnBudget_Click(object sender, EventArgs e)
        {
            try
            {
                hiringContract.CalculateBudget((double)nudBudget.Value);
            }
            catch (Exception exc)
            {

                MessageBox.Show(exc.Message);
            }
        }

        private void btnNamedEntity_Click(object sender, EventArgs e)
        {
            if (hiringContract.GetMotorBoats() != null && hiringContract.GetMotorBoats().Count == 0)
            {
                MessageBox.Show("Kan alleen met motorboten op het IJsselmeer of Noordzee.");
            }
            else
            {
                hiringContract.WaterEntities.Add(Admin.WaterEntities.Find(w => w.Name == cbxNamedWaterEntity.Text));
                UpdateList();
            }
        }
    }
}

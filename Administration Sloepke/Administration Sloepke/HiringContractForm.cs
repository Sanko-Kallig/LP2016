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
            nudFriese.Value = hiringContract.FrieseCount;
            lbxProducts.DataSource = Admin.Products;
            lbxMotorBoot.Refresh();
            lbxMuscleBoat.Refresh();
            if (hiringContract.Boats != null)
            {
                if (hiringContract.GetMuscleBoats() != null)
                {
                    lbxContractMuscle.DataSource = null;
                    lbxContractMuscle.DataSource = hiringContract.GetMuscleBoats();

                }
                if (hiringContract.GetMotorBoats() != null)
                {
                    lbxContractMotor.DataSource = null;
                    lbxContractMotor.DataSource = hiringContract.GetMotorBoats();
                }
                if (hiringContract.Products.Count != 0)
                {
                    lbxChosenProducts.DataSource = null;
                    lbxChosenProducts.DataSource = hiringContract.Products;
                }
                if (hiringContract.WaterEntities != null)
                {
                    lbxWaterEntity.DataSource = null;
                    lbxWaterEntity.DataSource = hiringContract.WaterEntities.FindAll(w => w.Name != null);
                }
                lbxContractMuscle.Refresh();
                lbxContractMotor.Refresh();
                lbxChosenProducts.Refresh();
                lbxWaterEntity.Refresh();


            }
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
            hiringContract.Products.Add(Admin.Products[lbxProducts.SelectedIndex]);
            UpdateList();
        }

        private void btnBudget_Click(object sender, EventArgs e)
        {
            try
            {
                hiringContract.StartDate = dtpStart.Value;
                hiringContract.EndDate = dtpEnd.Value;
                this.nudTotalPrice.Value = (decimal)hiringContract.CalculateBudget((double)nudBudget.Value);
                this.nudFriese.Value = hiringContract.FrieseCount;
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

        private void btnAddContract_Click(object sender, EventArgs e)
        {
            try
            {
                if (tbxEmail.Text != string.Empty && tbxNaam.Text != string.Empty && Admin.CheckEmail(tbxEmail.Text))
                {
                    if (dtpStart.Value > dtpEnd.Value)
                    {
                        MessageBox.Show("De eind datum kan niet eerder zijn dan het start datum");
                    }
                    else if (Admin.GetCustomer(tbxEmail.Text) == null)
                    {
                        Customer customer = new Customer(-1, tbxEmail.Text, tbxNaam.Text);
                        Admin.AddCustomer(customer);
                        hiringContract.Renter = Admin.GetCustomer(tbxEmail.Text);
                        hiringContract.Employee = LoggedInAccount.AccountLoggedIn;
                        hiringContract.StartDate = dtpStart.Value;
                        hiringContract.EndDate = dtpEnd.Value;
                        if (Admin.AddHiringContract(hiringContract))
                        {
                            MessageBox.Show("Contract en nieuwe klant zijn aangemaakt.");
                        }
                    }
                    else if (Admin.GetCustomer(tbxEmail.Text) != null)
                    {
                        hiringContract.Renter = Admin.GetCustomer(tbxEmail.Text);
                        hiringContract.Employee = LoggedInAccount.AccountLoggedIn;
                        hiringContract.StartDate = dtpStart.Value;
                        hiringContract.EndDate = dtpEnd.Value;
                        if (Admin.AddHiringContract(hiringContract))
                        {
                            MessageBox.Show("Contract is aangemaakt");
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            
            
        }
    }
}

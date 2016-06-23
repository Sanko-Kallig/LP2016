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
    public partial class ViewContract : Form
    {
        private HiringContract hiringContract;
        private Administration Admin;
        public ViewContract(HiringContract hiringcontract, Administration admin)
        {
            InitializeComponent();
            this.hiringContract = hiringcontract;
            this.Admin = admin;
            UpdateList();
        }
            
        private void UpdateList()
        {
            nudFriese.Value = hiringContract.FrieseCount;

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
                hiringContract.FrieseCount = hiringContract.WaterEntities.Count(w => w.Name == null);
                tbxEmail.Text = hiringContract.Renter.Email;
                tbxNaam.Text = hiringContract.Renter.Name;
                nudFriese.Value = (decimal) hiringContract.FrieseCount;
                nudBudget.Value = (decimal) hiringContract.TotalPrice();

            }
        }


        private void btnExporteer_Click(object sender, EventArgs e)
        {
            SaveFileDialog Save = new SaveFileDialog();
            Save.DefaultExt = ".txt";
            Save.Filter = "Text file (*.txt)|*.txt|Alle bestanden|*.*";

            if (Save.ShowDialog() == DialogResult.OK)
            {
                hiringContract.ExportHiringContract(Save.FileName);
            }
           
            
        }
    }
}

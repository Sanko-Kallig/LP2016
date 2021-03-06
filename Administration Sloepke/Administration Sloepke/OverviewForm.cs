﻿using System;
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
    public partial class OverviewForm : Form
    {
        private Administration Admin;
        public OverviewForm(Administration admin)
        {
            InitializeComponent();
            this.Admin = admin;
            this.UpdateList();
        }

        public void UpdateList()
        {
            Admin.Constracts = Admin.GetHiringContracts();
            if (Admin.Constracts != null)
            {
                this.lbxContracts.DataSource = Admin.Constracts;
                this.lbxContracts.Refresh();
            }
            else
            {
                this.lbxContracts.Items[0] = "Geen huur contracten beschikbaar";
            }
            
        }

        private void btnNewContract_Click(object sender, EventArgs e)
        {
            this.Hide();
            HiringContractForm hCF = new HiringContractForm(Admin);
            hCF.ShowDialog();
            this.Show();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            this.Hide();
            ViewContract vC = new ViewContract(Admin.Constracts[lbxContracts.SelectedIndex], Admin);
            vC.ShowDialog();
            this.Show();
        }
    }
}

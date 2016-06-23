namespace Administration_Sloepke
{
    partial class OverviewForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbxContracts = new System.Windows.Forms.ListBox();
            this.btnView = new System.Windows.Forms.Button();
            this.btnNewContract = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbxContracts
            // 
            this.lbxContracts.FormattingEnabled = true;
            this.lbxContracts.ItemHeight = 16;
            this.lbxContracts.Location = new System.Drawing.Point(25, 32);
            this.lbxContracts.Name = "lbxContracts";
            this.lbxContracts.Size = new System.Drawing.Size(219, 292);
            this.lbxContracts.TabIndex = 0;
            // 
            // btnView
            // 
            this.btnView.Location = new System.Drawing.Point(25, 330);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(90, 23);
            this.btnView.TabIndex = 1;
            this.btnView.Text = "Inzien";
            this.btnView.UseVisualStyleBackColor = true;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // btnNewContract
            // 
            this.btnNewContract.Location = new System.Drawing.Point(121, 330);
            this.btnNewContract.Name = "btnNewContract";
            this.btnNewContract.Size = new System.Drawing.Size(123, 23);
            this.btnNewContract.TabIndex = 2;
            this.btnNewContract.Text = "Nieuw contract";
            this.btnNewContract.UseVisualStyleBackColor = true;
            this.btnNewContract.Click += new System.EventHandler(this.btnNewContract_Click);
            // 
            // OverviewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(304, 403);
            this.Controls.Add(this.btnNewContract);
            this.Controls.Add(this.btnView);
            this.Controls.Add(this.lbxContracts);
            this.Name = "OverviewForm";
            this.Text = "OverzichtForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lbxContracts;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.Button btnNewContract;
    }
}
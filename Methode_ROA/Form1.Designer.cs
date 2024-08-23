namespace Methode_ROA
{
    partial class Form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.BtValider = new System.Windows.Forms.Button();
            this.txtNbreCD = new System.Windows.Forms.TextBox();
            this.TxtNbreCP = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.BtPontentiel = new System.Windows.Forms.Button();
            this.RdEltMinimal = new System.Windows.Forms.RadioButton();
            this.RdCoinNordOuest = new System.Windows.Forms.RadioButton();
            this.PnlMatrice = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.BtValider);
            this.splitContainer1.Panel1.Controls.Add(this.txtNbreCD);
            this.splitContainer1.Panel1.Controls.Add(this.TxtNbreCP);
            this.splitContainer1.Panel1.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.splitContainer1.Panel1MinSize = 10;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.AutoScroll = true;
            this.splitContainer1.Panel2.Controls.Add(this.panel2);
            this.splitContainer1.Panel2.Controls.Add(this.PnlMatrice);
            this.splitContainer1.Size = new System.Drawing.Size(1004, 441);
            this.splitContainer1.SplitterDistance = 58;
            this.splitContainer1.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(298, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(147, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Nbre Centre Distribution";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "Nbre Centre Production";
            // 
            // BtValider
            // 
            this.BtValider.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtValider.Location = new System.Drawing.Point(627, 18);
            this.BtValider.Name = "BtValider";
            this.BtValider.Size = new System.Drawing.Size(94, 23);
            this.BtValider.TabIndex = 2;
            this.BtValider.Text = "&Valider";
            this.BtValider.UseVisualStyleBackColor = true;
            this.BtValider.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtNbreCD
            // 
            this.txtNbreCD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNbreCD.Location = new System.Drawing.Point(454, 21);
            this.txtNbreCD.Name = "txtNbreCD";
            this.txtNbreCD.Size = new System.Drawing.Size(100, 23);
            this.txtNbreCD.TabIndex = 1;
            this.txtNbreCD.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox2_KeyPress);
            // 
            // TxtNbreCP
            // 
            this.TxtNbreCP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtNbreCP.Location = new System.Drawing.Point(172, 21);
            this.TxtNbreCP.Name = "TxtNbreCP";
            this.TxtNbreCP.Size = new System.Drawing.Size(100, 23);
            this.TxtNbreCP.TabIndex = 0;
            this.TxtNbreCP.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.BtPontentiel);
            this.panel2.Controls.Add(this.RdEltMinimal);
            this.panel2.Controls.Add(this.RdCoinNordOuest);
            this.panel2.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel2.Location = new System.Drawing.Point(1, 5);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(134, 373);
            this.panel2.TabIndex = 1;
            // 
            // BtPontentiel
            // 
            this.BtPontentiel.Location = new System.Drawing.Point(10, 254);
            this.BtPontentiel.Name = "BtPontentiel";
            this.BtPontentiel.Size = new System.Drawing.Size(75, 23);
            this.BtPontentiel.TabIndex = 0;
            this.BtPontentiel.Text = "Pontentiel";
            this.BtPontentiel.UseVisualStyleBackColor = true;
            this.BtPontentiel.Click += new System.EventHandler(this.BtPontentiel_Click);
            // 
            // RdEltMinimal
            // 
            this.RdEltMinimal.AutoSize = true;
            this.RdEltMinimal.Location = new System.Drawing.Point(6, 185);
            this.RdEltMinimal.Name = "RdEltMinimal";
            this.RdEltMinimal.Size = new System.Drawing.Size(120, 20);
            this.RdEltMinimal.TabIndex = 0;
            this.RdEltMinimal.TabStop = true;
            this.RdEltMinimal.Text = "Elément Minimal";
            this.RdEltMinimal.UseVisualStyleBackColor = true;
            this.RdEltMinimal.CheckedChanged += new System.EventHandler(this.RdEltMinimal_CheckedChanged);
            // 
            // RdCoinNordOuest
            // 
            this.RdCoinNordOuest.AutoSize = true;
            this.RdCoinNordOuest.Location = new System.Drawing.Point(4, 112);
            this.RdCoinNordOuest.Name = "RdCoinNordOuest";
            this.RdCoinNordOuest.Size = new System.Drawing.Size(122, 20);
            this.RdCoinNordOuest.TabIndex = 0;
            this.RdCoinNordOuest.TabStop = true;
            this.RdCoinNordOuest.Text = "Coin Nord Ouest";
            this.RdCoinNordOuest.UseVisualStyleBackColor = true;
            this.RdCoinNordOuest.CheckedChanged += new System.EventHandler(this.RdCoinNordOuest_CheckedChanged);
            // 
            // PnlMatrice
            // 
            this.PnlMatrice.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PnlMatrice.AutoScroll = true;
            this.PnlMatrice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlMatrice.Location = new System.Drawing.Point(131, 5);
            this.PnlMatrice.Name = "PnlMatrice";
            this.PnlMatrice.Size = new System.Drawing.Size(873, 373);
            this.PnlMatrice.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1004, 441);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BtValider;
        private System.Windows.Forms.TextBox txtNbreCD;
        private System.Windows.Forms.TextBox TxtNbreCP;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton RdEltMinimal;
        private System.Windows.Forms.RadioButton RdCoinNordOuest;
        private System.Windows.Forms.Panel PnlMatrice;
        private System.Windows.Forms.Button BtPontentiel;
    }
}


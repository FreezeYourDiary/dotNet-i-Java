namespace app
{
    partial class Form1
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
            this.button1 = new System.Windows.Forms.Button();
            this.nItems = new System.Windows.Forms.TextBox();
            this.seed = new System.Windows.Forms.TextBox();
            this.capacity = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(38, 175);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // nItems
            // 
            this.nItems.Location = new System.Drawing.Point(38, 43);
            this.nItems.Name = "nItems";
            this.nItems.Size = new System.Drawing.Size(100, 20);
            this.nItems.TabIndex = 1;
            this.nItems.TextChanged += new System.EventHandler(this.nItems_TextChanged);
            // 
            // seed
            // 
            this.seed.Location = new System.Drawing.Point(38, 78);
            this.seed.Name = "seed";
            this.seed.Size = new System.Drawing.Size(100, 20);
            this.seed.TabIndex = 2;
            // 
            // capacity
            // 
            this.capacity.Location = new System.Drawing.Point(38, 120);
            this.capacity.Name = "capacity";
            this.capacity.Size = new System.Drawing.Size(100, 20);
            this.capacity.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "label1";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.BackColor = System.Drawing.SystemColors.MenuBar;
            this.ClientSize = new System.Drawing.Size(503, 488);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.capacity);
            this.Controls.Add(this.seed);
            this.Controls.Add(this.nItems);
            this.Controls.Add(this.button1);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Name = "Form1";
            this.Text = "App";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox nItems;
        private System.Windows.Forms.TextBox seed;
        private System.Windows.Forms.TextBox capacity;
        private System.Windows.Forms.Label label1;
    }
}


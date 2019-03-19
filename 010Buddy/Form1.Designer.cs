namespace _010Buddy
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
            this.PacketList = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.BTT_OpenCapture = new FontAwesome.Sharp.IconButton();
            this.SuspendLayout();
            // 
            // PacketList
            // 
            this.PacketList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PacketList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.PacketList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.PacketList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.PacketList.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PacketList.FullRowSelect = true;
            this.PacketList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.PacketList.HideSelection = false;
            this.PacketList.LabelWrap = false;
            this.PacketList.Location = new System.Drawing.Point(-1, 63);
            this.PacketList.Name = "PacketList";
            this.PacketList.ShowGroups = false;
            this.PacketList.Size = new System.Drawing.Size(343, 686);
            this.PacketList.TabIndex = 0;
            this.PacketList.UseCompatibleStateImageBehavior = false;
            this.PacketList.View = System.Windows.Forms.View.Details;
            this.PacketList.SelectedIndexChanged += new System.EventHandler(this.PacketList_SelectedIndexChanged);
            this.PacketList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.PacketList_DoubleClick);
            // 
            // BTT_OpenCapture
            // 
            this.BTT_OpenCapture.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTT_OpenCapture.BackColor = System.Drawing.Color.Transparent;
            this.BTT_OpenCapture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BTT_OpenCapture.FlatAppearance.BorderSize = 0;
            this.BTT_OpenCapture.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTT_OpenCapture.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.BTT_OpenCapture.IconChar = FontAwesome.Sharp.IconChar.Star;
            this.BTT_OpenCapture.IconColor = System.Drawing.Color.White;
            this.BTT_OpenCapture.IconSize = 16;
            this.BTT_OpenCapture.Location = new System.Drawing.Point(287, 26);
            this.BTT_OpenCapture.Name = "BTT_OpenCapture";
            this.BTT_OpenCapture.Rotation = 0D;
            this.BTT_OpenCapture.Size = new System.Drawing.Size(55, 36);
            this.BTT_OpenCapture.TabIndex = 1;
            this.BTT_OpenCapture.UseVisualStyleBackColor = false;
            this.BTT_OpenCapture.Click += new System.EventHandler(this.BTT_OpenCapture_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(341, 749);
            this.Controls.Add(this.BTT_OpenCapture);
            this.Controls.Add(this.PacketList);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "010 Buddy";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView PacketList;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private FontAwesome.Sharp.IconButton BTT_OpenCapture;
    }
}


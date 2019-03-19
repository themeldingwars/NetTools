namespace Common.UI
{
    partial class PacketList
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ListView = new MaterialSkin.Controls.MaterialListView();
            this.SuspendLayout();
            // 
            // ListView
            // 
            this.ListView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ListView.Depth = 0;
            this.ListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ListView.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F);
            this.ListView.FullRowSelect = true;
            this.ListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.ListView.Location = new System.Drawing.Point(0, 0);
            this.ListView.MouseLocation = new System.Drawing.Point(-1, -1);
            this.ListView.MouseState = MaterialSkin.MouseState.OUT;
            this.ListView.Name = "ListView";
            this.ListView.OwnerDraw = true;
            this.ListView.Size = new System.Drawing.Size(383, 863);
            this.ListView.TabIndex = 0;
            this.ListView.UseCompatibleStateImageBehavior = false;
            this.ListView.View = System.Windows.Forms.View.List;
            // 
            // PacketList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ListView);
            this.Name = "PacketList";
            this.Size = new System.Drawing.Size(383, 863);
            this.ResumeLayout(false);

        }

        #endregion

        private MaterialSkin.Controls.MaterialListView ListView;
    }
}

using Common;
using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _010Buddy
{
    public partial class Form1 : MaterialForm
    {
        const int WIDTH               = 300;
        const int RowHeight           = 22;
        const string TempPacketFolder = "TempPackets";

        private bool IsFirstEdSearch        = true;
        private IntPtr? EdWindowHdl         = null;
        private Timer AttachTimer           = null;
        private int ClinetPckCount          = 0;
        private int ServerPckCount          = 0;
        private List<PcapPacketLog> Packets = new List<PcapPacketLog>();
        private string CurrentOutputDir     = "";
        private string EditorFilePath       = "";

        public Form1()
        {
            InitializeComponent();

            GetEdWinHdl();
            AttachToEditor();
            SetupAttachTimer();

            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);

            Width = WIDTH;

            PacketList.View             = View.Details;
            ImageList imgList           = new ImageList();
            imgList.ImageSize           = new Size(1, RowHeight);
            PacketList.SmallImageList   = imgList;
            Font                        = new Font(Font, FontStyle.Bold);
            PacketList.Columns[0].Width = this.ClientSize.Width - 50;

            BTT_OpenCapture.IconChar = FontAwesome.Sharp.IconChar.FolderOpen;
            BTT_OpenCapture.IconSize = BTT_OpenCapture.Height;
            BTT_OpenCapture.BringToFront();

            Directory.CreateDirectory(TempPacketFolder);
        }

        private IntPtr? GetEdWinHdl()
        {
            const string ProcessName = @"010Editor";
            Process[] processes = Process.GetProcessesByName(ProcessName);

            if (processes == null || processes.Count() == 0)
            {
                if (IsFirstEdSearch)
                {
                    MessageBox.Show("Could not find 010 Editor instance to attach to");
                    IsFirstEdSearch = false;
                }

                EdWindowHdl = null;
                return null;
            }

            var process    = processes.First();
            EditorFilePath = process.MainModule.FileName;

            EdWindowHdl = process.MainWindowHandle;
            return EdWindowHdl;
        }

        private void AttachToEditor()
        {
            const int MargnOffset = 8;

            if (EdWindowHdl != null)
            {
                var edRect = new User32.RECT();
                User32.GetWindowRect(EdWindowHdl.Value, ref edRect);

                var edPlacement = new User32.WINDOWPLACEMENT();
                User32.GetWindowPlacement(EdWindowHdl.Value, ref edPlacement);
                var isEdMaxmised = edPlacement.showCmd == User32.SW_MAXIMIZE;

                var height  = edRect.bottom - edRect.top;
                Height      = height - MargnOffset;
                Width       = WIDTH;

                if (isEdMaxmised)
                {
                    edPlacement.showCmd = (int)User32.SW_NORMAL;
                    User32.SetWindowPlacement(EdWindowHdl.Value, ref edPlacement);

                    User32.MoveWindow(EdWindowHdl.Value, edRect.left + Width, edRect.top, (edRect.right - edRect.left) - Width, edRect.bottom - edRect.top, true);
                }

                this.SetDesktopLocation(edRect.left - (Width - MargnOffset), edRect.top);
            }
            else
            {
                GetEdWinHdl();
            }
        }

        private void SetupAttachTimer()
        {
            AttachTimer          = new System.Windows.Forms.Timer();
            AttachTimer.Interval = 30;

            AttachTimer.Tick += (object sender, EventArgs e) => {
                AttachToEditor();
            }; 

            AttachTimer.Start();
        }

        private void AddTestItems()
        {
            PacketList.Items.Clear();

            for (int i = 0; i < 10; i++)
            {
                //AddPacket();
            }
        }

        private void AddPacket(PcapPacketLog Pkt)
        {
            Color ServerColor = Color.FromArgb(23, 50, 56);
            Color ClientColor = Color.FromArgb(71, 57, 91);

            var name = Pkt.IsFromServer ? $"[-> S]" : $"[<- C]";
            name    += $" Idx: {PacketList.Items.Count} Size: {Pkt.Data.Length}";

            PacketList.Items.Add(new ListViewItem(name)
            {
                ForeColor = Color.WhiteSmoke,
                BackColor = Pkt.IsFromServer ? ServerColor : ClientColor,
                Tag       = PacketList.Items.Count
            });

            if (Pkt.IsFromServer)
            {
                ServerPckCount++;
            }
            else
            {
                ClinetPckCount++;
            }
        }

        private void LoadPacketCapture()
        {
            var fb         = new OpenFileDialog();
            var showResult = fb.ShowDialog();

            if (showResult == DialogResult.OK || showResult == DialogResult.Yes)
            {
                var filePath     = fb.FileName;
                CurrentOutputDir = Path.Combine(TempPacketFolder, Path.GetFileNameWithoutExtension(filePath));

                Directory.CreateDirectory(CurrentOutputDir);

                if (filePath != null)
                {
                    var loader = new PcapLoader(filePath);

                    Packets.Clear();
                    Packets = loader.GetPacketBytes();

                    PacketList.Items.Clear();

                    PacketList.BeginUpdate();
                    foreach (var pct in Packets)
                    {
                        AddPacket(pct);
                    }

                    PacketList.EndUpdate();
                }
            }
        }

        private void BTT_OpenCapture_Click(object sender, EventArgs e)
        {
            LoadPacketCapture();
        }

        private void PacketList_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*if (PacketList.SelectedIndices.Count == 1)
            {
                var selectedIdx = PacketList.SelectedIndices[0];

                if (Packets.Count <= selectedIdx)
                {
                    var packet = Packets[selectedIdx];
                    SendPacketToEditor(packet, selectedIdx);
                }
            }*/
        }

        private void PacketList_DoubleClick(object sender, MouseEventArgs e)
        {
            ListViewHitTestInfo info = ((ListView)sender).HitTest(e.X, e.Y);
            if (info != null && info.Item != null)
            {
                var idx = (int)info.Item.Tag;
                var packet = Packets[idx];
                SendPacketToEditor(packet, idx);
            }
        }

        private void SendPacketToEditor(PcapPacketLog PacketData, int Idx)
        {
            if (EditorFilePath != null)
            {
                var source   = PacketData.IsFromServer ? "Server" : "Client";
                var name     = $"{source} {Idx}";
                var filePath = Path.Combine(CurrentOutputDir, name);

                File.WriteAllBytes(filePath, PacketData.Data);

                Process.Start(EditorFilePath, $"\"{filePath}\"");
            }
        }
    }
}

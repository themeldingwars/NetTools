using SharpPcap;
using SharpPcap.LibPcap;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Common
{
    public class PcapPacketLog
    {
        public DateTime Time;
        public bool IsMatrixMessage;
        public bool IsFromServer;
        public byte[] Data;
    }

    public class PcapLoader
    {
        ICaptureDevice device;
        List<PcapPacketLog> Packets = new List<PcapPacketLog>();
        private string ClientIP = "";
        private string ServerIP = "";
        private bool HasGottenAllMatrixMessages = false;

        public PcapLoader(string FilePath)
        {
            var errorMsg = OpenCaptureFile(FilePath);
            if (errorMsg == null)
            {
                device.OnPacketArrival += new PacketArrivalEventHandler(OnPacketArrival);

                device.Capture();
                device.Close();
            }
        }

        private string OpenCaptureFile(string FilePath)
        {
            try
            {
                device = new CaptureFileReaderDevice(FilePath);
                device.Open();
                return null;
            }
            catch (Exception e)
            {
                return $"Error opening capture ({FilePath}): {e.ToString()}";
            }
        }

        private void OnPacketArrival(object sender, CaptureEventArgs e)
        {
            PcapPacketLog pckLog = new PcapPacketLog();

            if (e.Packet.LinkLayerType == PacketDotNet.LinkLayers.Ethernet)
            {
                var time = e.Packet.Timeval.Date;
                var packet = PacketDotNet.Packet.ParsePacket(e.Packet.LinkLayerType, e.Packet.Data);
                var ethernetPacket = (PacketDotNet.EthernetPacket)packet;
                var ipPacket = (PacketDotNet.IPPacket)packet.Extract(typeof(PacketDotNet.IPPacket));
                var udpPacket = (PacketDotNet.UdpPacket)packet.Extract(typeof(PacketDotNet.UdpPacket));

                if (udpPacket == null) { return; }

                if (!HasGottenAllMatrixMessages && IsMatrixPacket(udpPacket.Bytes))
                {
                    var matrixMsgType = GetMatrixMessageType(udpPacket.Bytes);

                    if (matrixMsgType == "POKE")
                    {
                        ClientIP = ipPacket.SourceAddress.ToString();
                    }
                    else if (matrixMsgType == "HEHE")
                    {
                        ServerIP = ipPacket.SourceAddress.ToString();
                    }
                    else if (matrixMsgType == "HUGG")
                    {
                        HasGottenAllMatrixMessages = true;
                    }

                    pckLog.IsMatrixMessage = true;
                }

                pckLog.IsFromServer = IsFromServer(ipPacket.SourceAddress);
                pckLog.Time = time;
                pckLog.Data = new byte[udpPacket.Bytes.Length - 8];
                Array.Copy(udpPacket.Bytes, 8, pckLog.Data, 0, pckLog.Data.Length);

                // Filter to only firefall packets
                if (!HasGottenAllMatrixMessages || IsFromServer(ipPacket.SourceAddress) || IsFromClient(ipPacket.SourceAddress))
                {
                    Packets.Add(pckLog);
                }
            }
        }

        private bool IsMatrixPacket(byte[] Data)
        {
            if (Data.Length >= 12)
            {
                var id = BitConverter.ToUInt32(Data, 8);
                return id == 0;
            }

            return false;
        }

        private string GetMatrixMessageType(byte[] Data)
        {
            const int offset = 12;
            if (Data.Length >= offset)
            {
                var subBytes = new byte[4];
                Array.Copy(Data, offset, subBytes, 0, subBytes.Length);

                return System.Text.Encoding.Default.GetString(subBytes);
            }

            return null;
        }

        private bool IsFromServer(IPAddress IP)
        {
            return ServerIP == IP.ToString();
        }

        private bool IsFromClient(IPAddress IP)
        {
            return ClientIP == IP.ToString();
        }

        public List<PcapPacketLog> GetPacketBytes()
        {
            return Packets;
        }
    }
}

using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using DotOriko.Network.Packets;

namespace DotOriko.Network {

    internal class NetworkHandler {
        private bool isSocketRunning;
        private NetworkStream networkStream;
        private StreamWriter streamWriter;
        private StreamReader streamReader;
        private IPEndPoint remoteEndPoint;

        private UdpClient udpClient;
        private TcpClient tcpClient;

        private Thread tcpReaderThread;
        private Thread udpReaderThread;

        private PacketHandler packetHandler;

        public string clientUID {
            get; set;
        }

        public int ping {
            get; set;
        }

        public NetworkHandler () {
            packetHandler = new PacketHandler();
        }

        public void Connect (string ip, int port) {
            remoteEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);

            tcpClient = new TcpClient(ip, port);
            udpClient = new UdpClient(ip, port);

            networkStream = tcpClient.GetStream();
            streamWriter = new StreamWriter(networkStream);
            streamReader = new StreamReader(networkStream);

            isSocketRunning = true;

            tcpReaderThread = new Thread(new ThreadStart(ReceiveTcpData));
            tcpReaderThread.IsBackground = true;
            tcpReaderThread.Start();

            udpReaderThread = new Thread(new ThreadStart(ReceiveUdpData));
            udpReaderThread.IsBackground = true;
            udpReaderThread.Start();
        }

        private void OnReceive (string data, string protocol) {
            Debugger.Log("Received via <" + protocol + "> : " + data);
            packetHandler.HandleRawPacket(data);
        }

        private void ReceiveUdpData () {
            while (isSocketRunning) {
                try {
                    byte[] data = udpClient.Receive(ref remoteEndPoint);
                    string receivedText = Encoding.UTF8.GetString(data);
                    // To do smth with recieved data
                    OnReceive(receivedText, "upd");
                } catch (SocketException e) {
                } catch (Exception e) {
                    Debugger.Err(e.ToString());
                }
            }
        }

        private void ReceiveTcpData () {
            string receivedText = "";
            while (isSocketRunning) {
                try {
                    if (networkStream.DataAvailable) {
                        receivedText = streamReader.ReadLine();
                        // Handle data
                        OnReceive(receivedText, "tcp");
                    }
                } catch (Exception e) {
                    Debugger.Err(e.ToString());
                }
            }
            StopTcpReaderThread();
        }

        public void Send (PacketBase packet) {
            Send(packet, packet.GetProtocol());
        }

        public void Send (PacketBase packet, string protocol) {
            string data = packet.Encode();
            if (protocol == "tcp") {
                SendTcpData(data);
            } else {
                SendUdpData(data);
            }
        }

        private void SendUdpData (string msg) {
            try {
                byte[] data = Encoding.UTF8.GetBytes(msg);
                udpClient.Send(data, data.Length);
                Debugger.Log("Sending via <udp> " + msg);
            } catch (Exception e) {
                Debugger.Err(e.ToString());
            }
        }

        private void SendTcpData (string msg) {
            try {
                streamWriter.Write(msg + "\n");
                streamWriter.Flush();
                Debugger.Log("Sending via <tcp> " + msg);
            } catch (Exception e) {
                Debugger.Err(e.ToString());
            }
        }

        private void StopUdpReaderThread () {
            if (udpClient != null) {
                udpClient.Close();
                Debugger.Log("UDP thread has stopped");
            }
        }

        private void StopTcpReaderThread () {
            if (tcpClient != null) {
                streamWriter.Close();
                streamReader.Close();
                networkStream.Close();
                tcpClient.Close();
                Debugger.Log("TCP thread has stopped");
            }
        }

        public void Disconnect () {
            isSocketRunning = false;
            StopUdpReaderThread();
            // TODO send closing packet
        }
    }
}

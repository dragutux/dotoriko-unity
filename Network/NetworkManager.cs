///
/// DotOriko v1.0
/// Physics object controller
/// By Inlife 24.04.2016
/// 

using System;
using System.Net;
using System.IO;
using System.Collections;
using UnityEngine;

using DotOriko.Core;
using DotOriko.Network.Packets;

namespace DotOriko.Network {
    public class NetworkManager : DotOrikoSingleton<NetworkManager> {

        private NetworkHandler handler = new NetworkHandler();

        public void Connect(string ip, int port) {
            this.handler.Connect(ip, port);
//            this.handler.Send(new ConnectionRequest("asd", "123"));
        }

        public void Send(PacketBase packet, string protocol) {
            this.handler.Send(packet, protocol);
        }

        public void Send(PacketBase packet) {
            this.handler.Send(packet, packet.GetProtocol());
        }

        public void Disconnect() {
            this.handler.Disconnect();
        }

        protected override void OnReleaseResources() {
            this.handler.Disconnect();
        }
    }
}

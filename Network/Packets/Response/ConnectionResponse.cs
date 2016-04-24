using DotOriko.Core;
using DotOriko.Network.Packets.Request;
//using Newtonsoft.Json.Linq;

namespace DotOriko.Network.Packets.Response {

    public class ConnectionResponse : PacketBase {

        public ConnectionResponse () {
            type = PacketType.ConnectionResponse;
        }

        public override void Handle (string jPacket) {
//            JObject data = (JObject) jPacket["data"];
//            string uid = (string) data["uid"];
//            // GameManager.network.
//            GameManager.network.Send(new HandshakeRequest(uid), "udp");
        }
    }
}
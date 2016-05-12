using DotOriko.Core;
using DotOriko.Network.Packets.Request;
//using Newtonsoft.Json.Linq;

namespace DotOriko.Network.Packets.Response {

    public class PingResponse : PacketBase {

        public PingResponse () {
            type = PacketType.PingResponse;
        }

        public override void Handle (string jPacket) {
//            JObject data = (JObject) jPacket["data"];
//            long timestamp = (long) data["timestamp"];
//            int ping = (int) data["ping"];
//            GameManager.network.ping = ping;
//            GameManager.network.Send(new PingRequest(timestamp));
        }
    }
}
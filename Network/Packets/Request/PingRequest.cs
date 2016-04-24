using System.IO;
using System.Text;
//using Newtonsoft.Json;

namespace DotOriko.Network.Packets.Request {

    public class PingRequest : PacketBase {
        private long timestamp;

        public PingRequest (long timestamp) {
            this.type = PacketType.PingRequest;
            this.timestamp = timestamp;
        }

        public override string Encode () {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);

//            using (JsonWriter writer = new JsonTextWriter(sw)) {
//                writer.WriteStartObject();
//                writer.WritePropertyName("id");
//                writer.WriteValue(GetID());
//                writer.WritePropertyName("data");
//                writer.WriteStartObject();
//                writer.WritePropertyName("timestamp");
//                writer.WriteValue(timestamp);
//                writer.WriteEndObject();
//                writer.WriteEndObject();
//            }
            return sb.ToString();
        }
    }
}
using System.IO;
using System.Text;
//using Newtonsoft.Json;

namespace DotOriko.Network.Packets.Request {

    public class HandshakeRequest : PacketBase {
        private string uid;

        public HandshakeRequest (string uid) {
            type = PacketType.HandshakeRequest;
            this.uid = uid;
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
//                writer.WritePropertyName("uid");
//                writer.WriteValue(uid);
//                writer.WriteEndObject();
//                writer.WriteEndObject();
//            }
            return sb.ToString();
        }
    }
}
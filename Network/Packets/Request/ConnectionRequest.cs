using System.IO;
using System.Text;
//using Newtonsoft.Json;

namespace DotOriko.Network.Packets.Request {

    public class ConnectionRequest : PacketBase {
        private string username;
        private string password;

        public ConnectionRequest (string username, string password) {
            type = PacketType.ConnectionRequest;
            this.username = username;
            this.password = password;
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
//                writer.WritePropertyName("username");
//                writer.WriteValue(username);
//                writer.WritePropertyName("password");
//                writer.WriteValue(password);
//                writer.WriteEndObject();
//                writer.WriteEndObject();
//            }
            return sb.ToString();
        }
    }
}
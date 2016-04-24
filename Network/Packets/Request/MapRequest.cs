using System.IO;
using System.Text;
//using Game.Core;
//using Newtonsoft.Json;

namespace DotOriko.Network.Packets.Request {

    public class MapRequest : PacketBase {
        private string username;
        private string password;

        public MapRequest () {
            type = PacketType.ConnectionRequest;
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
//                writer.WriteValue(GameManager.network.clientUID);
//                writer.WriteEndObject();
//                writer.WriteEndObject();
//            }

            return sb.ToString();
        }
    }
}
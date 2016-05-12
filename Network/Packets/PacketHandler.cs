using System;
using System.Collections.Generic;
//using Newtonsoft.Json.Linq;

namespace DotOriko.Network.Packets {

    public class PacketHandler {
        private const string classPath = "Game.Network.Packets.Response.";
        private Dictionary<int, string> register;

        public PacketHandler () {
            register = new Dictionary<int, string>();
            fillRegister();
        }

        public bool HandleRawPacket (string data) {
//            JObject jPacket = JObject.Parse(data);
//            byte packetID = (byte) jPacket["id"];
//            if (register.ContainsKey(packetID)) {
//                //Debuger.Log("Trying to create packet from Json with id: " + packetID);
//                Type targetType = GetCustomType(classPath + register[packetID]);
//                //Debuger.Log("Register data with this id: " + register[packetID]);
//                var myObject = (PacketBase) Activator.CreateInstance(targetType);
//                myObject.Handle(jPacket);
//                return true;
//            }
            return false;
        }

        // Fill register with packets
        private void fillRegister () {
            foreach (var p in Enum.GetValues(typeof(PacketType))) {
                register.Add((int) p, p.ToString());
            }
        }

        // Some nifty method to find Type from string
        public static Type GetCustomType (string fullTypeName) {
            // Try to find type in an easy way.
            Type type = Type.GetType(fullTypeName);
            if (type != null) {
                return type;
            } else {
                // Complicated, but 99% resultative.
                foreach (var a in AppDomain.CurrentDomain.GetAssemblies()) {
                    type = a.GetType(fullTypeName);
                    if (type != null) {
                        return type;
                    }
                }
            }
            return null;
        }

        public void DecodePacket (string data) {
        }
    }
}
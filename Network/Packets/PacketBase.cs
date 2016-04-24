namespace DotOriko.Network.Packets {

    public abstract class PacketBase {
        protected PacketType type;
        protected string protocol = "tcp";

        // Virtual stuff
        public virtual string Encode () {
            Debugger.Log("Packet <" + GetID() + "><" + GetName() + "> went to it's Encoder!");
            return null;
        }

        public virtual void Handle (string jPacket) {
            Debugger.Log("Packet <" + GetID() + "><" + GetName() + "> went to it's handler!");
        }

        // Getters
        public int GetID () {
            return (int) this.type;
        }

        public string GetName () {
            return this.type.ToString();
        }

        public string GetProtocol () {
            return this.protocol;
        }
    }
}
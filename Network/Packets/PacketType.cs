namespace DotOriko.Network.Packets {

    public enum PacketType {
        //PacketBase = 1,

        ConnectionResponse = 1,
        PingResponse = 2,
        HandshakeResponse = 3,
        MapResponse = 10,

        ConnectionRequest = 501,
        PingRequest = 502,
        HandshakeRequest = 503,
        MapRequest = 510
    }
}
using System.Collections.Generic;
using DotOriko.Network.Packets;
//using DotOriko.Map.Data;
//using DotOriko.Utils.Vector3;
//using Newtonsoft.Json.Linq;

namespace DotOriko.Network.Packets.Response {

    public class MapResponse : PacketBase {

        public MapResponse () {
            this.type = PacketType.MapResponse;
        }

        public override void Handle (string jPacket) {
            // Get raw data section
//            JObject jData = (JObject) jPacket["data"];
//
//            // Fill M_Map object
//            M_Map _map = new M_Map();
//            _map.terrain = new M_Terrain();
//            _map.objects = new List<M_Object>();
//
//            // Get Terrain raw data
//            JToken jTerrain = jData["terrain"];
//            JArray jTerrainPos = (JArray) jTerrain["pos"];
//            JArray jTerrainScale = (JArray) jTerrain["scale"];
//            JArray jTerrainHeights = (JArray) jTerrain["heights"];
//
//            // Fill M_Terrain object
//            _map.terrain.name = (string) jTerrain["name"];
//            _map.terrain.pos = Vector3.ConvertFromJArray(jTerrainPos);
//            _map.terrain.scale = Vector3.ConvertFromJArray(jTerrainScale);
//            _map.terrain.heights = M_Terrain.GetHeightsFromJArray(jTerrainHeights);
//
//            //Get raw Objects data
//            JArray jObjects = (JArray) jData["objects"];
//
//            // Iterate each object in array
//            foreach (JObject obj in jObjects) {
//                // Get Object raw data
//                JArray jObjectPos = (JArray) obj["pos"];
//                JArray jObjectRot = (JArray) obj["rot"];
//                JArray jObjectScale = (JArray) obj["scale"];
//                JArray jObjectColliders = (JArray) obj["colliders"];
//
//                // Fill M_Object object
//                M_Object _object = new M_Object();
//                _object.name = (string) obj["name"];
//                _object.package = (string) obj["package"];
//                _object.state = (string) obj["state"];
//                _object.pos = Vector3.ConvertFromJArray(jObjectPos);
//                _object.rot = Vector3.ConvertFromJArray(jObjectRot);
//                _object.scale = Vector3.ConvertFromJArray(jObjectScale);
//                _object.colliders = new List<M_Collider>();
//
//                foreach (JObject jCollider in jObjectColliders) {
//                    // Get Collider raw data
//                    JArray jColliderPos = (JArray) obj["pos"];
//                    JArray jColliderRot = (JArray) obj["rot"];
//                    JArray jColliderSize = (JArray) obj["size"];
//
//                    // Fill M_Collider object
//                    M_Collider _collider = new M_Collider();
//                    _collider.type = (string) jCollider["type"];
//                    _collider.pos = Vector3.ConvertFromJArray(jColliderPos);
//                    _collider.rot = Vector3.ConvertFromJArray(jColliderRot);
//                    _collider.size = Vector3.ConvertFromJArray(jColliderSize);
//                    _object.colliders.Add(_collider);
//                }
//                // Add generated object to M_Object list
//                _map.objects.Add(_object);
//            }
        }
    }
}
///
/// DotOriko v1.0
/// Decal system with mesh generation
/// By NoxCaos 14.02.2016
/// 

using UnityEngine;
using System.Collections.Generic;
using DotOriko.Core;
using DotOriko.Utils;

namespace DotOriko.Meshes {
    public sealed class DecalSystem : DotOrikoSingleton<DecalSystem> {

        private Dictionary<string, GameObject> decals;

        protected override void OnInitialize() {
            base.OnInitialize();

            this.decals = new Dictionary<string, GameObject>();
        }

        public GameObject GenerateLine(List<Vector3> points, 
            string decalName, 
            Material lineMaterial,
            LayerMask mask) {

            var path = new GameObject();
            var filter = path.AddComponent<MeshFilter>();
            path.AddComponent<MeshRenderer>();
            path.name = decalName;
            path.GetComponent<Renderer>().material = lineMaterial;
            filter.mesh = this.GenerateMesh(points, mask);
            this.decals.Add(decalName, path);
            return path;
        }

        public GameObject SpawnDecal(GameObject prefab, 
            Vector3 pos, 
            Vector3 reflectionSide, 
            LayerMask mask,
            string decalName) {

            var dec = Instantiate(prefab, pos, Quaternion.identity) as GameObject;
            var filter = dec.GetComponent<MeshFilter>();
            var newMesh = filter.mesh.Clone();
            SetPointsOnSurface(newMesh.vertices, mask, reflectionSide);
            filter.mesh = newMesh;
            this.decals.Add(decalName, dec);
            return dec;
        }

        public void RemoveDecalByName(string name) {
            if (this.decals.ContainsKey(name)) {
                Destroy(this.decals[name]);
                this.decals.Remove(name);
            } else {
                Debug.LogWarning("[DotOriko] DecalSystem: No decal with name " + name);
            }
        }

        public Mesh GenerateMesh(List<Vector3> points, LayerMask mask) {
            var mesh = new Mesh();
            var normals = new List<Vector3>();
            var vts = ConvertPointsToVerts(points).ToArray();
            SetPointsOnSurface(vts, mask, Vector3.up);

            mesh.vertices = vts;
            mesh.triangles = GenerateTriangles(vts);
            mesh.normals = normals.ToArray();
            return mesh;
        }

        public static List<Vector3> GenerateVertSegment(Vector3 from, Vector3 to) {
            var verts = new List<Vector3>();
            int segments = Mathf.RoundToInt(Vector3.Distance(from, to));
            Vector3 vec = (to - from).normalized;
            for (int j = 0; j < segments; j++) {
                var p = from + (vec * j);

                var dir = (p - to).normalized;
                dir = Quaternion.Euler(0, -90, 0) * dir;

                if (j > 1) {
                    var v1 = verts[verts.Count - 2];
                    var v2 = verts[verts.Count - 1];
                    verts.Add(new Vector3(v1.x, v1.y, v1.z));
                    verts.Add(new Vector3(v2.x, v2.y, v2.z));
                }

                verts.Add(new Vector3(p.x, p.y, p.z) + dir);
                verts.Add(new Vector3(p.x, p.y, p.z) - dir);
            }

            return verts;
        }

        public static List<Vector3> ConvertPointsToVerts(List<Vector3> points) {
            var verts = new List<Vector3>();

            for (int i = 0; i < points.Count - 1; i++) {
                int segments = Mathf.RoundToInt(Vector3.Distance(points[i], points[i + 1]));
                Vector3 vec = (points[i + 1] - points[i]).normalized;
                var segment = new List<Vector3>();
                for (int j = 0; j < segments; j++) {
                    var p = points[i] + (vec * j);

                    var dir = Vector3.zero;
                    if (i + 1 < points.Count) dir = (p - points[i + 1]).normalized;
                    dir = Quaternion.Euler(0, -90, 0) * dir;

                    verts.Add(new Vector3(p.x, p.y, p.z) + dir);
                    verts.Add(new Vector3(p.x, p.y, p.z) - dir);
                    if (i > 0 && i < points.Count - 1) {
                        verts.Add(new Vector3(p.x, p.y, p.z) + dir);
                        verts.Add(new Vector3(p.x, p.y, p.z) - dir);
                    }
                }
            }
            return verts;
        }

        public static int[] GenerateTriangles(Vector3[] verts) {
            var tri = new List<int>();
            for (int i = 0; i < verts.Length; i += 4) {
                tri.Add(i);
                tri.Add(i + 1);
                tri.Add(i + 2);
                tri.Add(i + 3);
                tri.Add(i + 2);
                tri.Add(i + 1);
            }
            return tri.ToArray();
        }

        public static int[] GenerateTriangles(List<Vector3> verts) {
            return GenerateTriangles(verts.ToArray());
        }

        public static List<Vector3> SetPointsOnSurface(List<Vector3> points, LayerMask mask) {
            var list = new List<Vector3>();
            for (int i = 0; i < points.Count; i++) {
                var p = new Vector3();
                p.x = points[i].x;
                p.y = 1000;
                p.z = points[i].z;
                RaycastHit hit;
                if (Physics.Raycast(p, -Vector3.up, out hit, mask.value)) {
                    p.y = hit.point.y + .5f;
                } else p.y = points[i].y;
                list.Add(p);
            }
            return list;
        }

        public static List<Vector3> SetPointsOnSurface(Vector3[] points, LayerMask mask, Vector3 refSide) {
            var list = new List<Vector3>();
            for (int i = 0; i < points.Length; i++) {
                var p = new Vector3();
                p += refSide * 10;
                RaycastHit hit;
                if (Physics.Raycast(p, -Vector3.up, out hit, mask.value)) {
                    p.y = hit.point.y + .3f;
                } else p.y = points[i].y;
                list.Add(p);
            }
            return list;
        }
    }
}

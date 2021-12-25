using UnityEngine;

namespace Resources {
    public static class VEx {
        public static Vector3 Strip(this Vector3 vec, bool x, bool y, bool z) {
            var dx = x ? 0 : vec.x;
            var dy = y ? 0 : vec.y;
            var dz = z ? 0 : vec.z;

            return new Vector3(dx, dy, dz);
        }
    }
}
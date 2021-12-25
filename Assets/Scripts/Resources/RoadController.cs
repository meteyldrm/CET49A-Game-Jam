using UnityEngine;

namespace Resources {
    public class RoadController: MonoBehaviour {
        public GameObject Platform;

        [SerializeField] private GameObject car;
        private Transform carTransform;

        private Vector3 target;
        private int iteration = 0;

        void Start() {
            carTransform = car.transform;
            var position = carTransform.position;
            target = new Vector3(position.x, position.y, position.z + 10);
        }

        void Update()
        {
            if (carTransform.position.z > target.z) {
                var index = iteration % transform.childCount;
                var tf = gameObject.transform.GetChild(index).transform;

                var position = tf.position;
                position = new Vector3(position.x, position.y, position.z + 90);
                tf.position = position;
                target = new Vector3(target.x, target.y, target.z + 10);
                iteration++;
            }
        }
    }
}
using UnityEngine;

namespace Resources {
    public class RoadController: MonoBehaviour {
        public GameObject Platform;
        public GameObject generationPoint;

        [SerializeField] private GameObject car;
        private Transform carTransform;

        void Start() {
            carTransform = car.transform;
        }

        void Update()
        {
             if(carTransform.position.z + 120 > generationPoint.transform.position.z) // how many meters of road you want
             {
                 var vec = new Vector3(0, -1.01f, generationPoint.transform.position.z + 5);
                 Instantiate(Platform, vec, Platform.transform.rotation);
                 generationPoint.transform.position = new Vector3(generationPoint.transform.position.x, generationPoint.transform.position.y, generationPoint.transform.position.z + 10);
             }
         }
    }
}
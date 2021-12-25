using UnityEngine;

namespace Resources {
    public class RoadController {
        public GameObject Platform;
        public Vector3 generationPoint;

        [SerializeField] private GameObject car;
        private Transform carTransform;

        void Start() {
            carTransform = car.transform;
            generationPoint = carTransform.position;
        }

        // void Update()
        // {
        //     if(carTransform.position.z < generationPoint.position.z)
        //     {
        //         var vec = new Vector3(carTransform.position.x, carTransform.position.y, carTransform.position.z + 50);
        //         GameObject.Instantiate(Platform, carTransform.position - Vector3.up, carTransform.rotation);
        //         generationPoint.position = new Vector3(generationPoint.position.x, generationPoint.position.y, generationPoint.position.z + 10);
        //     }
        // }
    }
}
using System.Collections;
using UnityEngine;

namespace Obstacles {
    public class PedestrianObstacle : MonoBehaviour, IBaseObstacle {
        private bool hasPedestrian = false;
        
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void setCorrectChoice(int choice) {
            throw new System.NotImplementedException();
        }

        public bool makeChoice(int choice) {
            throw new System.NotImplementedException();
        }

        public void setInitialState(int state) {
            throw new System.NotImplementedException();
        }

        public void setStateAfterTime(int hasPedestrian, float time) {
            StartCoroutine(stateTimeCoroutine(hasPedestrian, time));
        }
        private IEnumerator stateTimeCoroutine(int hasPedestrian, float time) { 
            yield return new WaitForSeconds(time);

            setCorrectChoice(hasPedestrian);
        }

        private void cross(float speed) {
            
        }
    }
}

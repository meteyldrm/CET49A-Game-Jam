using UnityEngine;

namespace Obstacles {
    public class TurnObstacle : MonoBehaviour, IBaseObstacle
    {
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void setInitialState(int state) {
            throw new System.NotImplementedException();
        }

        public void setStateAfterTime(int state, float time) {
            throw new System.NotImplementedException();
        }

        public void setCorrectChoice(int choice) {
            throw new System.NotImplementedException();
        }

        public bool makeChoice(int choice) {
            throw new System.NotImplementedException();
        }
    }
}
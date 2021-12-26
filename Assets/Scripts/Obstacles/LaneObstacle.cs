using UnityEngine;

namespace Obstacles {
    public class LaneObstacle : MonoBehaviour, IBaseObstacle
    {
        
        public void setInitialState(int state) {
            return;
        }

        public void setStateAfterTime(int state, float time) {
            return;
        }

        public void setCorrectChoice(int choice) {
            return;
        }

        public bool makeChoice(int choice) {
            return true;
        }
    }
}

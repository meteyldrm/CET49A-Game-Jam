using UnityEngine;

namespace Obstacles {
    public class LaneObstacle : MonoBehaviour, IBaseObstacle
    {
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

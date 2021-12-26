using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Obstacles {
    public class LaneObstacle : MonoBehaviour, IBaseObstacle {

        public void setInitialState(int state) {
            Transform obs = gameObject.transform.GetChild(0);
            Transform coin = gameObject.transform.GetChild(1);
            if (state == 0) {
                var position1 = obs.position;
                var position2 = coin.position;
                position1 = new Vector3(-1.5f, position1.y, position1.z);
                position2 = new Vector3(1.5f, position2.y, position2.z);
                obs.position = position1;
                coin.position = position2;
            } else {
                var position1 = obs.position;
                var position2 = coin.position;
                position1 = new Vector3(1.5f, position1.y, position1.z);
                position2 = new Vector3(-1.5f, position2.y, position2.z);
                obs.position = position1;
                coin.position = position2;
            }
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

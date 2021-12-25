using System.Collections;
using UnityEngine;

namespace Obstacles {
    public class PedestrianObstacle : MonoBehaviour, IBaseObstacle {
        private bool hasPedestrian = true;

        public void setCorrectChoice(int choice) {
            if (choice == 0) {
                hasPedestrian = true;
            } else if (choice == 1) {
                hasPedestrian = false;
            }
        }

        public bool makeChoice(int choice) {
            if (hasPedestrian) {
                return choice == 0;
            } else {
                return choice != 0;
            }
        }

        public void setInitialState(int state) {
            setStateAfterTime(state, 0f);
        }

        public void setStateAfterTime(int state, float time) {
            StartCoroutine(stateTimeCoroutine(state, time));
        }
        private IEnumerator stateTimeCoroutine(int state, float time) { 
            yield return new WaitForSeconds(time);

            var walkTime = 2f;

            var delta = 0f;

            Transform child = gameObject.transform.GetChild(0);
            var initPos = child.position;
            var targetPos = new Vector3(-initPos.x, initPos.y, initPos.z);

            while (delta < walkTime) {
                child.position = Vector3.Lerp(initPos, targetPos, delta / walkTime);
                delta += Time.deltaTime;
                yield return null;
            }

            setCorrectChoice(state);
        }
    }
}

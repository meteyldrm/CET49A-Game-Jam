using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Obstacles {
    public class PedestrianObstacle : MonoBehaviour, IBaseObstacle {
        private bool hasPedestrian = true;

        private bool isOnRight = true;

        public void setCorrectChoice(int choice) {
            if (choice == 0) {
                hasPedestrian = true;
                transform.GetChild(0).gameObject.SetActive(true);
            } else if (choice == 1) {
                hasPedestrian = false;
                transform.GetChild(0).gameObject.SetActive(false);
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
            setCorrectChoice(state);
        }

        public void setStateAfterTime(int state, float time) {
            StartCoroutine(stateTimeCoroutine(state, time));
        }

        private void OnEnable() {
            var c = Random.Range(0, 2);
            
            Transform child = gameObject.transform.GetChild(0);
            var position = child.position;
            if (position.x > 0 && c == 1) {
                var initPos = new Vector3(-position.x, position.y, position.z);
                child.position = initPos;
                isOnRight = false;
            } else {
                isOnRight = true;
            }
        }

        private void OnDisable() {
            if (!isOnRight) {
                Transform child = gameObject.transform.GetChild(0);
                var position = child.position;
                var initPos = new Vector3(-position.x, position.y, position.z);
                child.position = initPos;
                isOnRight = true;
            }
        }

        private IEnumerator stateTimeCoroutine(int state, float time) {

            var walkTime = 2f;

            var delta = 0f;

            Transform child = gameObject.transform.GetChild(0);
            var initPos = child.position;
            
            if (!isOnRight) {
                time += 1.1f;
            }
            yield return new WaitForSeconds(time);
            
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

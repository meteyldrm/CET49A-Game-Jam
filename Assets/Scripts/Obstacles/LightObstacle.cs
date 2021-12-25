using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Obstacles {
    public class LightObstacle : MonoBehaviour, IBaseObstacle {
        private bool isRed = true;

        public void setCorrectChoice(int choice) {
            switch (choice) {
                case 0:
                    isRed = true;
                    break;
                case 1:
                    isRed = false;
                    break;
                default:
                    isRed = false;
                    break;
            }
        }

        public bool makeChoice(int choice) {
            switch (choice) {
                case 0: //Left pedal, brake
                    return isRed;
                case 1: //Right pedal, gas
                    return !isRed;
                default:
                    return false;
            }
        }

        public void setStateAfterTime(bool willBeRed, float time) {
            StartCoroutine(stateTimeCoroutine(willBeRed, time));
        }

        private IEnumerator stateTimeCoroutine(bool willBeRed, float time) { 
            yield return new WaitForSeconds(time);

            this.isRed = willBeRed;
            setCorrectChoice(willBeRed ? 0 : 1);
        }
    }
}

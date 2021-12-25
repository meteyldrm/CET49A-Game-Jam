using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Obstacles {
    public class LightObstacle : MonoBehaviour, IBaseObstacle {
        private bool isRed = true;
        
        [SerializeField] private Material lightPoleMat;
        
        
        [SerializeField] private Material redMaterial;
        [SerializeField] private Material redDimMaterial;
        [SerializeField] private Material greenMaterial;
        [SerializeField] private Material greenDimMaterial;

        public void setCorrectChoice(int choice) {
            setMaterial(choice);
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

        private void setMaterial(int mat) {
            foreach (Transform child in transform) {
                MeshRenderer mr = child.gameObject.GetComponent<MeshRenderer>();
                if (mat == 0) {
                    mr.materials = new[] { lightPoleMat, redMaterial, greenDimMaterial };
                } else {
                    mr.materials = new[] { lightPoleMat, redDimMaterial, greenMaterial };
                }
            }
        }

        private void OnEnable() {
            setMaterial(0);
        }

        public void setInitialState(int state) {
            this.isRed = state == 0;
            setCorrectChoice(state);
        }

        public void setStateAfterTime(int state, float time) {
            StartCoroutine(stateTimeCoroutine(state, time));
        }

        private IEnumerator stateTimeCoroutine(int state, float time) { 
            yield return new WaitForSeconds(time);

            this.isRed = state == 0;
            setCorrectChoice(state);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class LightUIAppearanceController : MonoBehaviour {
        private Image image;
        
        private bool _configured = false;
        
        private void OnEnable() {
            if (!_configured) {
                image = GetComponentInChildren<Image>();
                
                _configured = true;
            }
        }

        public void red() {
            image.color = Color.red;
        }
        
        public void green() {
            image.color = Color.green;
        }
        
        public void setStateAfterTime(bool willBeRed, float time) {
            StartCoroutine(stateTimeCoroutine(willBeRed, time));
        }

        private IEnumerator stateTimeCoroutine(bool willBeRed, float time) { 
            yield return new WaitForSeconds(time);

            if (willBeRed) {
                red();
            } else {
                green();
            }
        }
    }
}

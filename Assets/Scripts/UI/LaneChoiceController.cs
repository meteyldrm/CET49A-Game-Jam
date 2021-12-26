

using System.Collections;
using UnityEngine;
using UnityEngine.UI;
namespace UI {
    public class LaneChoiceController : MonoBehaviour {

        [SerializeField] private GameObject car;
        [SerializeField] private GameObject leftButton;
        [SerializeField] private GameObject rightButton;
        
        
        [SerializeField] private GameObject leftSignalButton;
        [SerializeField] private GameObject rightSignalButton;

        [SerializeField] private GameObject leftSignalLight;
        [SerializeField] private GameObject rightSignalLight;

        private CarController carController;

        private float buttonCooldown;

        private IEnumerator activeLightFlashCoroutine;

        private bool _configured = false;

        private void OnEnable() {
            if (!_configured) {
                carController = car.GetComponent<CarController>();
                
                leftButton.GetComponent<Button>().onClick.AddListener(left);
                leftSignalButton.GetComponent<Button>().onClick.AddListener(leftSignal);
                rightButton.GetComponent<Button>().onClick.AddListener(right);
                rightSignalButton.GetComponent<Button>().onClick.AddListener(rightSignal);
                
                _configured = true;
            }

            buttonCooldown = 0f;
        }
        
        //TODO: Check signal before committing to turn

        public void left() {
            if (buttonCooldown == 0) {
                StartCoroutine(buttonCooldownCoroutine(0.5f));
                carController.switchLane("left");
                carController.madeChoice();
            }
        }
        
        public void leftSignal() {
            if (activeLightFlashCoroutine != null) {
                StopCoroutine(activeLightFlashCoroutine);
                rightSignalLight.SetActive(false);
                leftSignalLight.SetActive(false);
            }

            activeLightFlashCoroutine = lightFlashCoroutine(leftSignalLight, 0.15f);
            StartCoroutine(activeLightFlashCoroutine);
            if (buttonCooldown == 0) {
                StartCoroutine(buttonCooldownCoroutine(0.5f));
            }
        }

        public void right() {
            if (buttonCooldown == 0) {
                StartCoroutine(buttonCooldownCoroutine(0.5f));
                carController.switchLane("right");
                carController.madeChoice();
            }
        }
        
        public void rightSignal() {
            if (activeLightFlashCoroutine != null) {
                StopCoroutine(activeLightFlashCoroutine);
                rightSignalLight.SetActive(false);
                leftSignalLight.SetActive(false);
            }

            activeLightFlashCoroutine = lightFlashCoroutine(rightSignalLight, 0.15f);
            StartCoroutine(activeLightFlashCoroutine);
            if (buttonCooldown == 0) {
                StartCoroutine(buttonCooldownCoroutine(0.5f));
            }
        }

        private IEnumerator buttonCooldownCoroutine(float time) {
            buttonCooldown = time;
            yield return new WaitForSeconds(time);
            buttonCooldown = 0;
        }

        private IEnumerator lightFlashCoroutine(GameObject led, float frequency) {
            var delta = 0f;
            int multiplier = 1;

            var timeDelta = 0f;
            
            while(timeDelta < 5f){
                if (multiplier == 1 && delta > frequency) {
                    led.SetActive(true);
                    multiplier = -1;
                } else if (multiplier == -1 && delta < -frequency) {
                    led.SetActive(false);
                    multiplier = 1;
                }

                timeDelta += Time.deltaTime;
                delta += Time.deltaTime * multiplier;
                yield return null;
            }
        }
    }
}
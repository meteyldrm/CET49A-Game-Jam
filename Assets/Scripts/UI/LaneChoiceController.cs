

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

        private CarController carController;

        private float buttonCooldown;

        private bool _configured = false;

        private void OnEnable() {
            if (!_configured) {
                carController = car.GetComponent<CarController>();
                
                leftButton.GetComponent<Button>().onClick.AddListener(left);
                rightButton.GetComponent<Button>().onClick.AddListener(right);
                
                _configured = true;
            }

            buttonCooldown = 0f;
        }

        public void left() {
            if (buttonCooldown == 0) {
                StartCoroutine(buttonCooldownCoroutine(0.5f));
                carController.switchLane("left");
                carController.madeChoice();
            }
        }
        
        public void leftSignal() {
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
            if (buttonCooldown == 0) {
                StartCoroutine(buttonCooldownCoroutine(0.5f));
            }
        }

        private IEnumerator buttonCooldownCoroutine(float time) {
            buttonCooldown = time;
            yield return new WaitForSeconds(time);
            buttonCooldown = 0;
        }
    }
}
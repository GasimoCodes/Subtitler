using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Gasimo.Subtitles.Samples
{
    public class Pauser : MonoBehaviour
    {
        private Button button;

        // Start is called before the first frame update
        void Start()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(TogglePause);
            button.GetComponentInChildren<Text>().text = "Pause";

        }

        // Toggle pause
        public void TogglePause()
        {
            Time.timeScale = Time.timeScale == 0 ? 1 : 0;

            if (Time.timeScale == 0)
            {
                button.GetComponentInChildren<Text>().text = "Play";
            }
            else
            {
                button.GetComponentInChildren<Text>().text = "Pause";
            }
        }

    }
}

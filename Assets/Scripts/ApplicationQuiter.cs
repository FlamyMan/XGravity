using UnityEngine;

namespace Assets.Scripts
{
    public class ApplicationQuitter : MonoBehaviour
    {
        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
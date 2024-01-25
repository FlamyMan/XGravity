using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class NextLevelEnterer : MonoBehaviour
    {
        public void NextLevel()
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.buildIndex + 1);
        }
    }
}

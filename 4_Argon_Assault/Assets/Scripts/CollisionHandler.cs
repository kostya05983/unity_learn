using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [Tooltip("In seconds")] [SerializeField]
    float levelLoadDelay = 1f;

    [Tooltip("FX prefab on player")] [SerializeField]
    GameObject deathFx;

    void OnTriggerEnter(Collider other)
    {
        StartDeathSequence();
        deathFx.SetActive(true);
        Invoke("ReloadScene", levelLoadDelay);
    }

    private void StartDeathSequence()
    {
        print("Player dying");
        SendMessage("OnPlayerDeath");
    }

    //String referenced
    private void ReloadScene()
    {
        SceneManager.LoadScene(1);
    }
}
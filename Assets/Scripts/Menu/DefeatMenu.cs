using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DefeatMenu : MonoBehaviour {
    [SerializeField] private Transform player;
    [SerializeField] private GameObject defeatScreen;
    [SerializeField] private GameObject healthBar;

    public void Start() {
        defeatScreen.SetActive(false);
    }

    public void Update() {
        Health playerHealth = player.GetComponent<Health>();
        if (playerHealth != null) {
            if (playerHealth.isDead()) {
                defeatScreen.SetActive(true);
                healthBar.SetActive(false);
            }
        }
    }

    public void PlayAgain() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void QuitGame() {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}

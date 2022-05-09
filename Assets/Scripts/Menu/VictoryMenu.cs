using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryMenu : MonoBehaviour {
    [SerializeField] private Transform lastEnemy;
    [SerializeField] private GameObject victoryScreen;
    [SerializeField] private GameObject healthBar;

    public void Start() {
        victoryScreen.SetActive(false);
    }

    public void Update() {
        Health enemyHealth = lastEnemy.GetComponent<Health>();
        if (enemyHealth != null) {
            if (enemyHealth.isDead()) {
                victoryScreen.SetActive(true);
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

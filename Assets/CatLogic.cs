using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CatLogic : MonoBehaviour
{
    public int score = 0;
    public int lives = 3;

    public TextMeshProUGUI scoreText;
    public GameObject[] hearts;
    public GameObject gameOverPanel;

    private Animator anim;
    private bool isDead = false;

    [Header("Звуки")]
    public AudioClip eatGoodSound;
    public AudioClip eatBadSound;
    public AudioClip gameOverSound;
    public AudioSource bgMusic; 

    private AudioSource audioSource;

    void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>(); 
        UpdateUI();
        Time.timeScale = 1f;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isDead) return;

        CatchItem foodScript = other.GetComponent<CatchItem>();

        if (foodScript != null && foodScript.wasCaught == false)
        {
            Destroy(other.gameObject);
            return;
        }

        if (other.gameObject.CompareTag("GoodFood"))
        {
            score += 10;
            Destroy(other.gameObject);
            UpdateUI();

            if (eatGoodSound != null) audioSource.PlayOneShot(eatGoodSound);

            anim.ResetTrigger("EatBad");
            anim.SetTrigger("EatGood");
        }
        else if (other.gameObject.CompareTag("BadFood"))
        {
            lives -= 1;
            Destroy(other.gameObject);
            UpdateUI();

            if (eatBadSound != null) audioSource.PlayOneShot(eatBadSound);

            if (lives <= 0)
            {
                GameOver();
            }
            else
            {
                anim.ResetTrigger("EatGood");
                anim.SetTrigger("EatBad");
            }
        }
    }

    void UpdateUI()
    {
        scoreText.text = "Score: " + score;

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < lives) hearts[i].SetActive(true);
            else hearts[i].SetActive(false);
        }
    }

    void GameOver()
    {
        isDead = true;
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;

        if (bgMusic != null) bgMusic.Stop();

        if (gameOverSound != null) audioSource.PlayOneShot(gameOverSound);

        anim.SetTrigger("Die");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
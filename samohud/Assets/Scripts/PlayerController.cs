using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    public float turnSpeed;
    public InputAction moveAction;
    private Vector2 moveInput;
    public GameObject obstacle;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public GameObject gameOverPanel;
    public GameObject youWonPanel;
    private bool isGameEnded = false;
    void Start()
    {
        moveAction.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameEnded == true)
        {
            return;
        }
        moveInput = moveAction.ReadValue<Vector2>();
        if (transform.position.x < -8f && transform.position.x < -7.99f)
        {
            transform.position = new Vector3(-8f, transform.position.y, transform.position.z);
        }
        moveInput = moveAction.ReadValue<Vector2>();
        if (transform.position.x > 8f && transform.position.x > 7.99f)
        {
            transform.position = new Vector3(8f, transform.position.y, transform.position.z);
        }
        if (transform.position.z >= 180f)
        {
            isGameEnded = true;

            youWonPanel.SetActive(true);
            Time.timeScale = 0f;
        }
        if (transform.position.z < -11f && transform.position.z < -10.99f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -11f);
        }
        transform.Translate(Vector3.forward * Time.deltaTime * speed * moveInput.y);
        if (moveInput.y != 0)
        {
            transform.Rotate(Vector3.up, Time.deltaTime * turnSpeed * moveInput.x);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameOverPanel.SetActive(true);

            Time.timeScale = 0f;
        }

    }
    
    public void RestartGame()
    {
        // 5. Zanim przeładujemy scenę, musimy przywrócić czas do normalności
        Time.timeScale = 1f;

        // 6. Przeładowujemy scenę
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}

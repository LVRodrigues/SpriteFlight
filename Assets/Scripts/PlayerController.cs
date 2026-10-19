using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public float trustForce = 1f;
    public float maxSpeed = 5f;
    public GameObject boosterFlame;
    public float scoreMultiplier = 10f;
    public int score = 0;
    public GameObject explosion;
    public PanelRenderer gameUI;

    private Rigidbody2D body;
    private float elapsedTime = 0f;
    private Label scoreLabel;
    private Button restartButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        gameUI.RegisterUIReloadCallback(OnUIReload);
    }

    private void OnUIReload(PanelRenderer renderer, VisualElement root, int version)
    {
        scoreLabel = root.Q<Label>("scoreLabel");
        restartButton = root.Q<Button>("restartButton");
        restartButton.style.display = DisplayStyle.None;
        restartButton.clicked += ReloadScene;
        UpdateScoreLabel();
    }

    private void OnDestroy()
    {
        if (gameUI != null)
        {
            gameUI.UnregisterUIReloadCallback(OnUIReload);
        }
    }

    private void UpdateScoreLabel()
    {
        if (scoreLabel != null)
        {
            scoreLabel.text = $"Score: {score}";
        }
    }

    // Update is called once per frame
    void Update()
    {
        accelerate();
        booster();
        showScore();
    }

    private void showScore()
    {
        elapsedTime += Time.deltaTime;
        score = Mathf.FloorToInt(elapsedTime * scoreMultiplier);
        UpdateScoreLabel();

    }

    private void booster()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            boosterFlame.SetActive(true);
        }
        else if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            boosterFlame.SetActive(false);
        }
    }

    private void accelerate()
    {
        if (Mouse.current.leftButton.isPressed)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.value);
            Vector2 direction = (mousePosition - transform.position).normalized;

            transform.up = direction;
            body.AddForce(direction * trustForce);

            if (body.linearVelocity.magnitude > maxSpeed)
            {
                body.linearVelocity = body.linearVelocity.normalized * maxSpeed;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
        Instantiate(explosion, transform.position, transform.rotation);
        restartButton.style.display = DisplayStyle.Flex;
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

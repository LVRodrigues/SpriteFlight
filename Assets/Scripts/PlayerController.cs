using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float trustForce = 1f;
    public float maxSpeed = 5f;
    public GameObject boosterFlame;
    public float scoreMultiplier = 10f;
    public int score = 0;

    private Rigidbody2D body;
    private float elapsedTime = 0f;
    private Text scoreLabel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        CreateScoreDisplay();
    }

    // Update is called once per frame
    void Update()
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

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            boosterFlame.SetActive(true);
        } else if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            boosterFlame.SetActive(false);
        }

        elapsedTime += Time.deltaTime;
        score = Mathf.FloorToInt(elapsedTime * scoreMultiplier);
        if (scoreLabel != null)
        {
            scoreLabel.text = "Score: " + score;
        }
    }

    private void CreateScoreDisplay()
    {
        GameObject canvasObject = new GameObject("Score Canvas");
        Canvas canvas = canvasObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvasObject.AddComponent<CanvasScaler>();
        canvasObject.AddComponent<GraphicRaycaster>();

        GameObject labelObject = new GameObject("Score");
        labelObject.transform.SetParent(canvasObject.transform, false);

        scoreLabel = labelObject.AddComponent<Text>();
        scoreLabel.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        scoreLabel.fontSize = 32;
        scoreLabel.color = Color.white;
        scoreLabel.alignment = TextAnchor.UpperCenter;
        scoreLabel.text = "Score: " + score;

        RectTransform labelTransform = scoreLabel.rectTransform;
        labelTransform.anchorMin = new Vector2(0.5f, 1f);
        labelTransform.anchorMax = new Vector2(0.5f, 1f);
        labelTransform.pivot = new Vector2(0.5f, 1f);
        labelTransform.anchoredPosition = new Vector2(0f, -20f);
        labelTransform.sizeDelta = new Vector2(300f, 50f);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private Transform StartPoint;
    private Transform SpawnPoint;
    private int score = 0;

    public GameObject pinPrefab;

    private Pin currentPin;
    private bool isGameOver = false;

    public Text scoreText;

    private Camera mainCamera;
    public float speed = 3;

    // Start is called before the first frame update
    void Start()
    {
        StartPoint = GameObject.Find("StartPoint").transform;
        SpawnPoint = GameObject.Find("SpawnPoint").transform;
        mainCamera = Camera.main;
        SpawnPin();
    }

    void SpawnPin()
    {
        currentPin = GameObject.Instantiate(pinPrefab, SpawnPoint.position, pinPrefab.transform.rotation).GetComponent<Pin>();
    }
    // Update is called once per frame
    void Update()
    {
        if (isGameOver) return;
        if (Input.GetMouseButtonDown(0))
        {
            score++;
            scoreText.text = score.ToString();
            currentPin.startFly();
            SpawnPin();
        }
    }

    public void GameOver()
    {

        if (isGameOver) return;
        GameObject.Find("Circle").GetComponent<RotateSelf>().enabled = false;
        StartCoroutine(GameOverAnimation());

        isGameOver = true;
    }

    IEnumerator GameOverAnimation()
    {
        while(true)
        {
            mainCamera.backgroundColor = Color.Lerp(mainCamera.backgroundColor, Color.red, speed * Time.deltaTime);
            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, 4, speed * Time.deltaTime);
            if (Mathf.Abs(mainCamera.orthographicSize - 4) < 0.01)
            {
                break;
            }
            yield return 0;

        }
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
}

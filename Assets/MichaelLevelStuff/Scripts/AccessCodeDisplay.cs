using UnityEngine;
using TMPro;

public class AccessCodeDisplay : MonoBehaviour
{
    public TextMeshProUGUI codeText;
    public TextMeshProUGUI countdownText;

    private float timer = 20f;
    private float changeTime = 20f;

    void Start()
    {
        GenerateCode();
        UpdateCountdown();
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            timer = changeTime;
            GenerateCode();
        }

        UpdateCountdown();
    }

    void GenerateCode()
    {
        int code = Random.Range(0, 10000);
        codeText.text = code.ToString("D4");
    }

    void UpdateCountdown()
    {
        countdownText.text = "Next code in: "
            + Mathf.CeilToInt(timer) + "s";
    }
}

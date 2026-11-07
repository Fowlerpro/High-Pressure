using UnityEngine;
using TMPro;

public class AccessCodeDisplay : MonoBehaviour
{
    public TextMeshProUGUI codeText;
    public TextMeshProUGUI countdownText;

    private float timer = 30f;
    private float changeTime = 30f;

    public string CurrentCode { get; private set; }

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
        CurrentCode = "";

        for (int i = 0; i < 4; i++)
        {
            int digit = Random.Range(1, 10);
            CurrentCode += digit.ToString();
        }

        codeText.text = CurrentCode;
    }

    void UpdateCountdown()
    {
        countdownText.text = "Next code in: "
            + Mathf.CeilToInt(timer) + "s";
    }
}

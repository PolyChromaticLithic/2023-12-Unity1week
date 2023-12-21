using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Speak : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI speakerObj;
    [SerializeField] TextMeshProUGUI textObj;

    public static Speak Instance { get; private set; }
    private Coroutine _showCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Show(SpeakData[] speakDatas)
    {
        
        foreach (var speakData in speakDatas)
        {
            speakerObj.text = speakData.speaker;
            textObj.text = speakData.text;
            Showing(speakData);
        }

    }

    private void Showing(SpeakData speakData)
    {
        // テキスト全体の長さ
        var length = textObj.text.Length;

        // １文字ずつ表示する演出
        for (var i = 0; i < length; i++)
        {
            // 徐々に表示文字数を増やしていく
            textObj.maxVisibleCharacters = i;
            if (Input.GetKey(KeyCode.Space))
            {
                break;
            }
        }
        textObj.maxVisibleCharacters = length;
    }
}

public class SpeakData
{
    public string speaker;
    public string text;

    public SpeakData(string speaker, string text)
    {
        this.speaker = speaker;
        this.text = text;
    }
}

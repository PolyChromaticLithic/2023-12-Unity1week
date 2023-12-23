using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UIElements;

public class Speak : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI speakerObj;
    [SerializeField] TextMeshProUGUI textObj;

    [SerializeField] RectTransform textPanel;


    public static Speak Instance { get; private set; }
    private Coroutine _showCoroutine;

    private bool isWaiting = false;

    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            textPanel.DOScaleX(0, 0);
        }
        else
        {
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isWaiting)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                isWaiting = false;
            }
        }
    }

    public void Show(SpeakData[] speakDatas)
    {
        if (_showCoroutine != null)
        {
            StopCoroutine(_showCoroutine);
        }
        _showCoroutine = StartCoroutine(_Show(speakDatas));
    }

    public IEnumerator _Show(SpeakData[] speakDatas)
    {
        var wait = new WaitForSeconds(0.05f);
        textPanel.DOScaleX(1, 0.2f);
        foreach (var speakData in speakDatas)
        {
            speakerObj.text = speakData.speaker;
            textObj.text = speakData.text;
            textObj.maxVisibleCharacters = 0;
            isWaiting = true;
            for (int i = 0; i < textObj.text.Length; i++)
            {
                Showing();
                if (!isWaiting)
                {
                    break;
                }
                yield return wait;
            }
            isWaiting = true;
            textObj.maxVisibleCharacters = textObj.text.Length;
            yield return new WaitWhile(() => { return isWaiting; });
        }
        textPanel.DOScaleX(0, 0.2f);
        Player.isInteracting = false;
        Player.canMove = true;
        yield break;
    }

    public void Showing()
    {
        textObj.maxVisibleCharacters = textObj.maxVisibleCharacters + 1;
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

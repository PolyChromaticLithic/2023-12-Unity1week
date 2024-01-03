using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor.Experimental.GraphView;

/// <summary>
/// Button の GameObject にアタッチして使う
/// </summary>
[RequireComponent(typeof(Button))]
public class KeyAssignToButton : MonoBehaviour
{
    /// <summary>このキーを押すとボタンが反応する</summary>
    [SerializeField] KeyCode _key = default;
    Button _button = default;

    void Start()
    {
        _button = GetComponent<Button>();
    }

    void Update()
    {
        if (Input.GetKeyDown(_key))
        {
            // ボタンを押した時の見た目の変化を起こす
            ExecuteEvents.Execute(_button.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerDownHandler);
            EventSystem.current.SetSelectedGameObject(this.gameObject);
            // クリックは離した時に成立するが、ボタンからの場合は押した時点で成立させる
            _button.onClick.Invoke();
        }
        else if (Input.GetKeyUp(_key))
        {
            ExecuteEvents.Execute(_button.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerUpHandler);
        }
    }
}
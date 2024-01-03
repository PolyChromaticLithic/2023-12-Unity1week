using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor.Experimental.GraphView;

/// <summary>
/// Button �� GameObject �ɃA�^�b�`���Ďg��
/// </summary>
[RequireComponent(typeof(Button))]
public class KeyAssignToButton : MonoBehaviour
{
    /// <summary>���̃L�[�������ƃ{�^������������</summary>
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
            // �{�^�������������̌����ڂ̕ω����N����
            ExecuteEvents.Execute(_button.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerDownHandler);
            EventSystem.current.SetSelectedGameObject(this.gameObject);
            // �N���b�N�͗��������ɐ������邪�A�{�^������̏ꍇ�͉��������_�Ő���������
            _button.onClick.Invoke();
        }
        else if (Input.GetKeyUp(_key))
        {
            ExecuteEvents.Execute(_button.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerUpHandler);
        }
    }
}
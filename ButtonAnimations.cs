using UnityEngine;
using UnityEngine.UI;

public class ButtonAnimations : MonoBehaviour
{
    Button btn;
    Vector3 upScale = new Vector3(1.2f, 1.2f, 1);
    private void Awake()
    {
        btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener(Anim);
    }
    void Anim()
    {
        LeanTween.scale(gameObject, upScale,0.1f);
        LeanTween.scale(gameObject, Vector3.one, 0.1f).setDelay(0.1f);
    }
}

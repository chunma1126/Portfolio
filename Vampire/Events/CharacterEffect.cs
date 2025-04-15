using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class CharacterEffect : MonoBehaviour
{
    [SerializeField] private UIDocument uiDocument;
    private VisualElement _root;
    private VisualElement _characterBox;
    private VisualElement _characterHead;
    public Sprite questionMark;


    private void OnEnable()
    {
        _root = uiDocument.rootVisualElement;

        _characterBox = _root.Q<VisualElement>("Character");
        _characterHead = _root.Q<VisualElement>("Character-Head");
    }

    public void Jump()
    {
        StartCoroutine(JumpCoroutine());
    }

    public void Wondering()
    {
        StartCoroutine(WonderingCoroutine());
    }

    public void OnZoomIn()
    {
        _characterBox.AddToClassList("zoom");
    }
    
    public void OnZoomOut()
    {
        _characterBox.RemoveFromClassList("zoom");
        _characterBox.RemoveFromClassList("zoomX2");
    }
    
    public void OnInvisible()
    {
        _characterBox.AddToClassList("Invisible");
    }
    
    public void OffInvisible()
    {
        _characterBox.RemoveFromClassList("Invisible");
    }

    public void ZoomX2()
    {
        _characterBox.AddToClassList("zoomX2");
    }
    
    private IEnumerator WonderingCoroutine()
    {
        _characterHead.RemoveFromClassList("hidden");
        _characterHead.AddToClassList("on");
        
        _characterHead.style.backgroundImage = new StyleBackground(Utility.SpriteToTexture(questionMark));
        yield return new WaitForSeconds(0.9f);
        
        _characterHead.RemoveFromClassList("on");
        _characterHead.AddToClassList("hidden");
    }


    private IEnumerator JumpCoroutine()
    {
        float jumpHeight = -95;
        float jumpDuration = 0.15f;

        Vector3 initialPosition = _characterBox.transform.position;
        Vector3 targetPosition = initialPosition + new Vector3(0, jumpHeight, 0);

        float elapsedTime = 0f;
        while (elapsedTime < jumpDuration)
        {
            _characterBox.transform.position =
                Vector3.Lerp(initialPosition, targetPosition, (elapsedTime / jumpDuration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _characterBox.transform.position = targetPosition;

        elapsedTime = 0f;
        while (elapsedTime < jumpDuration)
        {
            _characterBox.transform.position =
                Vector3.Lerp(targetPosition, initialPosition, (elapsedTime / jumpDuration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _characterBox.transform.position = initialPosition;
    }
    
    
    
}
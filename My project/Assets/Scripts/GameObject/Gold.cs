using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler {
    [SerializeField] private GameModel _gameModel;
    private RectTransform _rectTransform;
    private Vector3 _initialPosition;
    private Image _image;
    private Block _block;
    private void Awake() {
        _gameModel = FindObjectOfType<GameModel>();
        _rectTransform = GetComponent<RectTransform>();
        _initialPosition = _rectTransform.anchoredPosition;
        _image = GetComponent<Image>();
    }
    public void SetAssociatedBlock(Block block) 
    {
        _block = block;
    }
    public void OnBeginDrag(PointerEventData eventData) {
        _image.raycastTarget = false;
    }
    public void OnDrag(PointerEventData eventData) {
        if (!_gameModel.GetPlayerIsWin())
            _rectTransform.anchoredPosition += eventData.delta;
    }
    public void OnEndDrag(PointerEventData eventData) {
        _image.raycastTarget = true;
        if (!IsOverlappingWithGoldField())
            _rectTransform.anchoredPosition = _initialPosition;
    }
    private bool IsOverlappingWithGoldField() {
        Vector2 worldPosition = _rectTransform.position;
        Collider2D[] colliders = Physics2D.OverlapPointAll(worldPosition);
        foreach (var collider in colliders) {
            if (collider.CompareTag("GoldField")) {
                _gameModel.ChangeGoldCount();
                _block.SetGoldFlag(false);
                Destroy(gameObject);
                return true;
            }
        }
        return false;
    }
}
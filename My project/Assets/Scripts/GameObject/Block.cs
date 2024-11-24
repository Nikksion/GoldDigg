using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Block : MonoBehaviour, IPointerClickHandler, IBlock {
    [SerializeField] private Image _blockImage;
    [SerializeField] private Sprite[] _depthSprites;
    [SerializeField] private GameConfig _gameConfig;
    [SerializeField] private int _currentDepth;
    [SerializeField]private bool _hasGold = false;
    private void Awake() {
        _currentDepth = _gameConfig.BlockDepth;
    }
    public void Initialize() {
        UpdateBlockSprite();
    }
    public void OnPointerClick(PointerEventData eventData) {
        GameController gameController = FindObjectOfType<GameController>();
        if (gameController != null)
            gameController.Dig(this);
    }
    public bool Dig() {
        if (_currentDepth > 0) {
            _currentDepth--;
            Debug.Log("Block dug. Current depth: " + _currentDepth);
            UpdateBlockSprite();
        }
        if (_currentDepth <= 0) {
            Debug.Log("Block is already at the surface");
            _blockImage.enabled = false;
        }
        return true;
    }
    public int GetDepth() => _currentDepth;
    public bool GetIsGold() => _hasGold;
    public void SetIsGold(bool value) {
        _hasGold = value;
    }
    public void SetDepth(int value) {
        _currentDepth = value; 
    }
    public bool SetGoldFlag(bool flag) {
        return _hasGold = flag;
    }
    private void UpdateBlockSprite() {
        if (_currentDepth < _gameConfig.BlockDepth) {
            int rand = Random.Range(0, _depthSprites.Length);
            _blockImage.sprite = _depthSprites[rand];
        }
        if (_currentDepth <= 0) 
            _blockImage.enabled = false;
    }
}
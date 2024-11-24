using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class GameView : MonoBehaviour, IGameView {
    [SerializeField] private GameModel _gameModel;
    [SerializeField] private Image _winImage;
    private TextMeshProUGUI _shovelText;
    private TextMeshProUGUI _goldText;
    private TextMeshProUGUI _winText;
    private void Awake() {
        _shovelText = GameObject.FindGameObjectWithTag("ShovelUI").GetComponent<TextMeshProUGUI>();
        _goldText = GameObject.FindGameObjectWithTag("GoldUI").GetComponent<TextMeshProUGUI>();
        _winText = GameObject.FindGameObjectWithTag("WinUI").GetComponent<TextMeshProUGUI>();
        _winImage = GameObject.FindGameObjectWithTag("WinImage").GetComponent<Image>();
    }
    public void UpdateGoldBarCount(int currentGoldCount, int maxGoldCount) {
        _goldText.text = $"{currentGoldCount}/{maxGoldCount}";
    }
    public void UpdateShovelCount(int shovelCount) {
        _shovelText.text = shovelCount.ToString();
    }
    public void PlayerWin() {
        _winText.enabled = true;
        _winImage.enabled = true;
    }
}
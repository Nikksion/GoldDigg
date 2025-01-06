using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class GameView : MonoBehaviour, IGameView {
    [SerializeField] private GameModel _gameModel;
    [SerializeField] private GameController _gameController;
    [SerializeField] private GameObject _winBoard;
    [SerializeField] private GameObject _grid;
    [SerializeField] private GameObject _shovelImg;
    [SerializeField] private GameObject _goldImg;
    [SerializeField] private DatabaseManager _database;
    [SerializeField] private TMP_Text[] playerNames;
    [SerializeField] private TMP_Text[] playerScores;
    private TextMeshProUGUI _shovelText;
    private TextMeshProUGUI _goldText;
    private TextMeshProUGUI _scoreText;
    private void Awake() {
        _shovelText = GameObject.FindGameObjectWithTag("ShovelUI").GetComponent<TextMeshProUGUI>();
        _goldText = GameObject.FindGameObjectWithTag("GoldUI").GetComponent<TextMeshProUGUI>();
        _scoreText = GameObject.FindGameObjectWithTag("Score").GetComponent<TextMeshProUGUI>();
    }
    public void UpdateGoldBarCount(int currentGoldCount, int maxGoldCount) {
        _goldText.text = $"{currentGoldCount}/{maxGoldCount}";
    }
    public void UpdateShovelCount(int shovelCount) {
        _shovelText.text = shovelCount.ToString();
    }
    public void UpdateScoreCount(int score) {
        _scoreText.text = "Score " + score.ToString();
    }
    public void PlayerWin() {
        _winBoard.SetActive(true);
        _grid.SetActive(false);
        _shovelImg.SetActive(false);
        _goldImg.SetActive(false);
        LoadLeaderboard();
        if (_gameController.GetSaveFlag())
            _gameController.DisableSaveBytton();
    }
    public void LoadLeaderboard() {
        var topPlayers = _database.GetTopPlayers(5);

        for (int i = 0; i < topPlayers.Count; i++) {
            if (i < playerNames.Length) 
            {
                playerNames[i].text = topPlayers[i].Name;
                playerScores[i].text = topPlayers[i].Score.ToString();
            }
        }
    }
}

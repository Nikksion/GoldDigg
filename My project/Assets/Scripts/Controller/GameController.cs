using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameController : MonoBehaviour, IGameController {
    [SerializeField] private GameModel _gameModel;
    [SerializeField] private GameView _gameView;
    [SerializeField] private DatabaseManager _database;
    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private GameObject _saveButton;
    private SaveMeneger _saverLoader;
    private const string SCEN_NAME = "SampleScene";
    private bool _saveButtonFlag;
    private void Awake() {
        _gameModel = GetComponent<GameModel>();
        _saverLoader = gameObject.AddComponent<SaveMeneger>();
        _saveButtonFlag = false;
    }
    public void Dig(Block block) {
        if (!_gameModel.GetPlayerIsWin())
            _gameModel.Dig(block);
    }
    public void AddNewScore() {
        _database.AddPlayer(_inputField.text, _gameModel.GetPlayerScore());
        DisableSaveBytton();
        _saveButtonFlag = true;
        _gameView.LoadLeaderboard();
    }
    public void ResetGame() {
        _saverLoader.DeleteSaveFile();
        SceneManager.LoadScene(SCEN_NAME);
        Debug.Log("Game restarted");
    }
    public bool GetSaveFlag() => _saveButtonFlag;
    public void SetSaveFlag(bool saveFlag) => _saveButtonFlag = saveFlag;
    public void DisableSaveBytton() {
        _inputField.gameObject.SetActive(false);
        _saveButton.SetActive(false);
    }
}
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameController : MonoBehaviour, IGameController {
    [SerializeField] private GameModel _gameModel;
    private SaveMeneger _saverLoader;
    private const string SCEN_NAME = "SampleScene";
    private void Awake() {
        _gameModel = GetComponent<GameModel>();
        _saverLoader = gameObject.AddComponent<SaveMeneger>();
    }
    public void Dig(Block block) {
        if (!_gameModel.GetPlayerIsWin())
            _gameModel.Dig(block);
    }
    public void ResetGame() {
        _saverLoader.DeleteSaveFile();
        SceneManager.LoadScene(SCEN_NAME);
        Debug.Log("Game restarted");
    }
}
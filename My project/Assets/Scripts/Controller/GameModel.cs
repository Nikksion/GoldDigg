using System.Collections.Generic;
using UnityEngine;
public class GameModel : MonoBehaviour, IGameModel {
    [SerializeField] private GameObject _goldBar;
    [SerializeField] private Transform _transforms;
    [SerializeField] private GameConfig _gameConfig;
    [SerializeField] private DragAndDrop _gold;
    [SerializeField] private GameView _gameView;
    [SerializeField] private Block[] _blocks;
    private static int _shovelCount;
    private static int _currentGold;
    private int _goldNeededForWin;
    private float _goldDropChance;
    private bool _playerIsWin = false;
    private int _playerScore;
    private float _gameStartTime;
    private void Awake() {
        _shovelCount = _gameConfig.ShovelCount;
        _goldNeededForWin = _gameConfig.GoldNeededForWin;
        _goldDropChance = _gameConfig.GoldDropChance;
        _goldDropChance = _gameConfig.GoldDropChance;
        _currentGold = 0;
        _playerScore = 0;
        _gameStartTime = 0;
    }
    private void Start() {
        Initialize();
    }
    public void Initialize() {
        _gameView.UpdateGoldBarCount(_currentGold, _goldNeededForWin);
        _gameView.UpdateShovelCount(_shovelCount);
        _gameView.UpdateScoreCount(_playerScore);
        PlayerWin();
    }
    public bool Dig(Block block) {
        if (_shovelCount > 0) {
            if (block.Dig()) {
                ChangeShovelCount();
                SpawnGoldBar(block);
                GameStartFlag();
                return true;
            }
            else
                Debug.Log("Failed to dig the block");
        }
        else
            Debug.Log("No shovels left");
        return false;
    }
    public void SpawnGoldBar(Block block) {
        float rand = Random.value;
        Debug.Log("Gold drop chance: " + rand);
        if (rand <= _goldDropChance) {
            Debug.Log("Gold has appeared");
            block.SetGoldFlag(true);
            CreateGoldBar(block);
            AddPlayerScore(50);
        }
    }
    public void SpawnGoldOnBlocksAfterLoad() {
        foreach (var block in _blocks) {
            if (block.GetIsGold()) 
                CreateGoldBar(block);
        }
    }
    public void ChangeGoldCount() {
        _currentGold++;
        AddPlayerScore(25);
        _gameView.UpdateGoldBarCount(_currentGold, _goldNeededForWin);
        PlayerWin();
    }
    public void SetBlockData(List<BlockData> blockDataList) {
        int length = Mathf.Min(blockDataList.Count, _blocks.Length);
        for (int i = 0; i < length; i++) {
            _blocks[i].SetIsGold(blockDataList[i].hasGold);
            _blocks[i].SetDepth(blockDataList[i].depth);
            _blocks[i].Initialize();
        }
    }
    public int GetShovelCount() => _shovelCount;
    public int GetGoldCount() => _currentGold;
    public int GetPlayerScore() => _playerScore;
    public bool GetPlayerIsWin() => _playerIsWin;
    public Block[] GetBlocks() => _blocks;
    public void SetShovelCount(int count) => _shovelCount = count;
    public void SetGoldCount(int count) => _currentGold = count;
    public void SetPlayerScore(int count) => _playerScore = count;
    public void SetPlayerIsWin(bool isWin) => _playerIsWin = isWin;
    private void ChangeShovelCount() {
        _shovelCount--;
        _gameView.UpdateShovelCount(_shovelCount);
    }
    private void CreateGoldBar(Block block) {
        GameObject spawnedGoldBar = Instantiate(_goldBar, block.transform.position, Quaternion.identity, _transforms);
        DragAndDrop dragAndDrop = spawnedGoldBar.GetComponent<DragAndDrop>();
        if (dragAndDrop != null) {
            dragAndDrop.SetAssociatedBlock(block);
        }
    }
    private void PlayerWin() {
        if (_currentGold >= _goldNeededForWin) {
            RemoveAllGold();
            if(!_playerIsWin)
                AddPlayerScore(100 - (int)(Time.time - _gameStartTime));
            Debug.Log(100 - (int)(Time.time - _gameStartTime));
            _gameStartTime = 0;
            _gameView.PlayerWin();
            _playerIsWin = true;
            
            Debug.Log("Player wins");
        }
    }
    private void AddPlayerScore(int score) {
        if (score > 0) {
            _playerScore += score;
            _gameView.UpdateScoreCount(_playerScore);
        }
    }
    private void GameStartFlag() {
        if (_gameStartTime != 0)
            _gameStartTime = Time.time;
    }
    void RemoveAllGold() {
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("Gold");
        foreach (GameObject obj in objectsWithTag) {
            Destroy(obj);
        }
    }
}
using System.Collections.Generic;
using UnityEngine;
public class GameInitializer : MonoBehaviour {
    [SerializeField] private GameModel _gameModel;
    [SerializeField] private GameController _gameController;
    private SaveMeneger _saverLoader;
    private void Awake() {
        _gameModel = GetComponent<GameModel>();
        _saverLoader = gameObject.AddComponent<SaveMeneger>();
    }
    private void Start() {
        InitializeGame();    
    }
    private void OnApplicationQuit() {
        SaveGame();
    }
    private void OnApplicationPause(bool pauseStatus) {
        if (pauseStatus) {
            SaveGame();
        }
    }
    private void InitializeGame() {
        SaveData loadedData = _saverLoader.LoadGame();
        if (loadedData != null) {
            Debug.Log("Game data loaded");
            _gameModel.SetShovelCount(loadedData.shovelsCount);
            _gameModel.SetGoldCount(loadedData.goldCount);
            _gameModel.SetPlayerScore(loadedData.playerScore);
            _gameModel.SetPlayerIsWin(loadedData.playerIsWin);
            _gameController.SetSaveFlag(loadedData.saveButtonFlag);
            foreach (var blockData in loadedData.blocks)
                _gameModel.SetBlockData(loadedData.blocks);
            _gameModel.SpawnGoldOnBlocksAfterLoad();
            _gameModel.Initialize();
        }
        else
            Debug.Log("No saved data found. Starting a new game");
    }
    private void SaveGame() {
        SaveData saveData = new SaveData {
            shovelsCount = _gameModel.GetShovelCount(),
            goldCount = _gameModel.GetGoldCount(),
            playerIsWin = _gameModel.GetPlayerIsWin(),
            playerScore = _gameModel.GetPlayerScore(),
            saveButtonFlag = _gameController.GetSaveFlag()
        };
        saveData.blocks = new List<BlockData>();
        foreach (var block in _gameModel.GetBlocks()) {
            if (block == null) {
                Debug.LogWarning("Block is null, skipping...");
                continue;
            }
            BlockData blockData = new BlockData(block.GetDepth(), block.GetIsGold());
            saveData.blocks.Add(blockData);
        }
        _saverLoader.SaveGame(saveData);
        Debug.Log("Game data saved");
    }

}

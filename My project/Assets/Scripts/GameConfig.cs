using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "Game Configuration")]
public class GameConfig : ScriptableObject {
    [SerializeField] private int _shovelCount;
    [SerializeField] private int _blockDepth;
    [SerializeField] private int _goldNeededForWin;
    [SerializeField] private float _goldDropChance;

    public int ShovelCount => this._shovelCount;
    public int BlockDepth => this._blockDepth;
    public int GoldNeededForWin => this._goldNeededForWin;
    public float GoldDropChance => this._goldDropChance;
}
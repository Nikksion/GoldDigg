using System.Collections.Generic;
[System.Serializable]
public class SaveData {
    public List<BlockData> blocks;
    public int shovelsCount;
    public int goldCount;
    public int playerScore;
    public bool playerIsWin;
    public bool saveButtonFlag;

}
[System.Serializable]
public struct BlockData {
    public int depth;
    public bool hasGold;
    public BlockData(int depth, bool hasGold) {
        this.depth = depth;
        this.hasGold = hasGold;
    }
}
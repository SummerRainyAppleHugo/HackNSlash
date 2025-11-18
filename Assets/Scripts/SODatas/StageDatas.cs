using UnityEngine;

[CreateAssetMenu(fileName = "NewStage", menuName = "Game/Stage")]
public class StageDatas : ScriptableObject
{
    [Header("Identification")]
    public string stageName;
    public Sprite previewImage;

    [Header("Configuration")]
    public string sceneToLoad;
    public int xpReward = 400;
    public bool isLocked = false;
}

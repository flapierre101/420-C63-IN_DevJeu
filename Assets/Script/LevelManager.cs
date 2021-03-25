using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public enum Level
    {
        Invalid = -1,

        // Define value by hand
        PlateformerC63Level1 = 0,
        PlateformerC63Level2 = 100


        // Or add new at the end and never delete
    }

    public Level CurrentLevel { get; private set; } = Level.Invalid;
    public string LevelEntranceId { get; private set; } = "Default";
    public LevelEntrance LevelEntrance { get; private set; }
    public LevelEntrance[] LevelEntrances { get; private set; }
    public LevelExit[] LevelExits { get; private set; }

    public void Awake()
    {
        var sceneName = SceneManager.GetActiveScene().name;
        CurrentLevel = (Level)Enum.Parse(typeof(Level), sceneName, true);

        foreach (Level level in Enum.GetValues(typeof(Level)))
        {
            var scene = SceneManager.GetSceneByName(level.ToString());
            Debug.Assert(scene != null, "LevelManager : Scene is missing for " + level.ToString());
        }
    }

    public void GoToLevel(Level level, string levelEntranceId)
    {
        LevelEntranceId = levelEntranceId;

        if (level == CurrentLevel)
        {
            OnLevelStartCommon();
            SceneManager.LoadScene(level.ToString());
        }
        else
        {
            CurrentLevel = level;

            GameManager.Instance.Mario.gameObject.SetActive(false);
            SceneManager.LoadScene(level.ToString());
        }
        
    }

    public void OnLevelStart()
    {
        LevelEntrances = FindObjectsOfType<LevelEntrance>();
        LevelExits = FindObjectsOfType<LevelExit>();
        DebugCheckForErrors();

        GameManager.Instance.Camera.GetComponent<FollowObject>().TargetTransform = GameManager.Instance.Mario.transform;
        GameManager.Instance.Mario.gameObject.SetActive(true);

        OnLevelStartCommon();
    }

    private void OnLevelStartCommon()
    {
        LevelEntrance = FindLevelEntrance();   
        
        GameManager.Instance.Mario.OnLevelStart(LevelEntrance);
    }

    private LevelEntrance FindLevelEntrance()
    {
        for (int i = 0; i < LevelEntrances.Length; i++)
        {
            var levelEntrance = LevelEntrances[i];
            if (levelEntrance.Id == LevelEntranceId)
                return levelEntrance;
        }

        Debug.LogError("LevelManager : Could not find LevelEntrance for Id " + LevelEntranceId);
        return null;
    }

    private void DebugCheckForErrors()
    {
        for (int i = 0; i < LevelEntrances.Length; i++)
        {
            var id = LevelEntrances[i].Id;
            for (int j = i + 1; j < LevelEntrances.Length; j++)
            {
                if (id == LevelEntrances[j].Id)
                {
                    Debug.LogError("LevelManager : LevelEntrance duplicate found for Id " + id, LevelEntrances[j]);
                }
            }
        }

        for (int i = 0; i < LevelExits.Length; i++)
        {
            if (LevelExits[i].Level == Level.Invalid)
            {
                Debug.LogError("LevelManager : LevelExit has not been configured", LevelExits[i]);
            }
        }
    }
}

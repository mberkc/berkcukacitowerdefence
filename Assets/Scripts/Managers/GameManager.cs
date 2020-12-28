using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public enum GameState {
    None, Paused, Starting, Playing, Ending,
}

public class GameManager : MonoBehaviour {
#region Variables

    public static GameManager Instance;

    [Space(10)] [Header("VARIABLES")] [SerializeField]
    int wave;

    [SerializeField] GameState currentGameState;

    Spawner   _spawner;
    UIManager _uiManager;

    int kills;

    public bool isPlaying;

    bool isLoadingScene;

#endregion

    public int Wave {
        get {
            return wave;
        }
        set {
            wave = value;
            PlayerPrefs.SetInt(Strings.WAVE, wave);
        }
    }

#region Initializing

    void Awake() {
        if (Instance == null) {
            Instance   = this;
            _spawner   = Spawner.Instance;
            _uiManager = UIManager.Instance;
            DontDestroyOnLoad(gameObject);
        } else if (Instance != this) {
            Destroy(gameObject);
        }
        Wave             = PlayerPrefs.GetInt(Strings.WAVE, wave);
        currentGameState = GameState.None;
    }

    void Start() {
        SetScene();
    }

#endregion

#region Condition Handling & Other Functions

    void SetScene() {
        _spawner.InitializeSpawner();
        kills = 0;
        _uiManager.InitPanels();
        isPlaying      = false;
        isLoadingScene = false;
        _uiManager.SetWaveText(wave);
        Initialize();
    }

    void Initialize() {
        if (GetGameState() != GameState.Starting) {
            SetGameState(GameState.Starting);
            // Level Initialization
            ReadyToPlay();
        }
    }

    void ReadyToPlay() {
        SetGameState(GameState.Playing);
        isPlaying = true;
    }

    // Event subscribe
    public void Finish(bool success) {
        if (GetGameState() != GameState.Ending) {
            SetGameState(GameState.Ending);
            isPlaying = false;
        }
        if (success)
            Win();
        else
            Lose();
    }

    void Win() {
    }

    void Lose() {
    }

#endregion

#region GameState Handling

    public GameState GetGameState() {
        return currentGameState;
    }

    public void SetGameState(GameState gameState) {
        currentGameState = gameState;
    }

#endregion

#region Level Management

    public void RestartLevel() {
        if (GetGameState() != GameState.Ending)
            SetGameState(GameState.Ending);
        isPlaying = false;
        StopAllCoroutines();
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene() {
        if (!isLoadingScene) {
            isLoadingScene = true;
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(0);
            while (!asyncLoad.isDone)
                yield return null;
            SetScene();
        }
    }

#endregion
}
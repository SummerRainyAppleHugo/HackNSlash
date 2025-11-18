using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HM_StageSelectButton : MonoBehaviour
{
    [Header("Stage Configuration")]
    [SerializeField] private StageDatas _stageData;
    [Header("Trigger Box Configs")]
    [SerializeField] private bool _isPhysicalTrigger = false;
    [SerializeField] private LayerMask _playerLayer = 1 << 0; // Layer "Default" par défaut

    [Header("Events")]
    public UnityEvent OnStageUnlocked;
    public UnityEvent OnStageLocked;

    private Button _button;
    private Collider2D _collider;

    private void Awake()
    {
        // Initialisation des composants
        _button = GetComponent<Button>();
        _collider = GetComponent<Collider2D>();

        // Configuration automatique
        if (_isPhysicalTrigger)
        {
            ConfigurePhysicalTrigger();
        }
        else
        {
            ConfigureUIButton();
        }
    }

    private void ConfigureUIButton()
    {
        if (_button != null)
        {
            _button.onClick.AddListener(LoadAssignedStage);
            UpdateButtonAppearance();
        }

        // Désactive le collider si ce n'est pas un trigger physique
        if (_collider != null) _collider.enabled = false;
    }

    private void ConfigurePhysicalTrigger()
    {
        if (_collider != null)
        {
            _collider.isTrigger = true;
        }

        // Désactive le bouton si c'est un trigger physique
        if (_button != null) _button.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!_isPhysicalTrigger) return;
        if (((1 << other.gameObject.layer) & _playerLayer) != 0)
        {
            HM_Stage.Instance.ReturnToLobby(); //Utiliser un LoadScene fonctionne aussi
        }
    }

    public void LoadAssignedStage()
    {
        if (_stageData == null)
        {
            Debug.LogError("StageData non assigné!", this);
            return;
        }

        if (_stageData.isLocked)
        {
            Debug.Log($"Stage {_stageData.stageName} verrouillé!");
            OnStageLocked?.Invoke();
            return;
        }

        HM_Stage.Instance.LoadScene(_stageData.sceneToLoad);
    }

    public void UnlockStage()
    {
        if (_stageData != null)
        {
            _stageData.isLocked = false;
            OnStageUnlocked?.Invoke();
            UpdateButtonAppearance();
        }
    }

    private void UpdateButtonAppearance()
    {
        if (_button != null && _stageData != null)
        {
            if (_button.TryGetComponent(out Image buttonImage))
            {
                buttonImage.sprite = _stageData.previewImage;
                buttonImage.color = _stageData.isLocked ? Color.gray : Color.white;
            }
        }
    }
}

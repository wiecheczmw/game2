using UnityEngine.UIElements;
using UnityEngine;

public class HUDController
{
    // Paski
    private VisualElement _hpBar;
    private VisualElement _manaBar;
    private VisualElement _expBar;
    
    // Teksty wartości (Nowość)
    private Label _hpValLabel;
    private Label _manaValLabel;
    private Label _expValLabel;

    // Info gracza
    private Label _nameLabel;
    private Label _levelLabel;

    public HUDController(VisualElement root)
    {
        var playerInstance = root.Q<VisualElement>("PlayerInstance");
        if (playerInstance == null) return;

        // Szukamy Pasków
        _hpBar = playerInstance.Q<VisualElement>("BarHP");
        _manaBar = playerInstance.Q<VisualElement>("BarSP");
        _expBar = playerInstance.Q<VisualElement>("BarEXP");

        // Szukamy Tekstów Wartości (Nowość)
        _hpValLabel = playerInstance.Q<Label>("ValHP");
        _manaValLabel = playerInstance.Q<Label>("ValSP");
        _expValLabel = playerInstance.Q<Label>("ValEXP");

        // Szukamy Info
        _nameLabel = playerInstance.Q<Label>("PlayerName");
        _levelLabel = playerInstance.Q<Label>("LevelText");
    }

    public void UpdateHealth(float current, float max)
    {
        if (_hpBar != null)
        {
            float percent = Mathf.Clamp01(current / max) * 100f;
            _hpBar.style.width = Length.Percent(percent);
        }

        // Aktualizacja tekstu (np. "120 / 200")
        if (_hpValLabel != null)
        {
            _hpValLabel.text = $"{Mathf.RoundToInt(current)} / {Mathf.RoundToInt(max)}";
        }
    }

    public void UpdateMana(float current, float max)
    {
        if (_manaBar != null)
        {
            float percent = Mathf.Clamp01(current / max) * 100f;
            _manaBar.style.width = Length.Percent(percent);
        }

        if (_manaValLabel != null)
        {
            _manaValLabel.text = $"{Mathf.RoundToInt(current)} / {Mathf.RoundToInt(max)}";
        }
    }
    
    // Metoda dla EXP (opcjonalnie)
    public void UpdateExp(float current, float max)
    {
        if (_expBar != null)
        {
            float percent = Mathf.Clamp01(current / max) * 100f;
            _expBar.style.width = Length.Percent(percent);
        }
        
        if (_expValLabel != null)
        {
             float percent = (current / max) * 100f;
            _expValLabel.text = $"{percent:F1}%"; // np. "45.5%"
        }
    }

    public void SetPlayerInfo(string name, int level)
    {
        if (_nameLabel != null) _nameLabel.text = name;
        if (_levelLabel != null) _levelLabel.text = level.ToString();
    }
}
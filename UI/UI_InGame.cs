using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Maneja la interfaz de usuario del juego en tiempo real. Actualiza la barra de salud del jugador, muestra el n�mero de almas actuales y 
/// maneja el enfriamiento de las habilidades y objetos equipados. 
/// Tambi�n comprueba si se han pulsado las teclas correspondientes para activar las habilidades y objetos equipados.
/// </summary>
public class UI_InGame : MonoBehaviour {
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private Slider slider;

    [SerializeField] private Image dashImage;
    [SerializeField] private Image parryImage;
    [SerializeField] private Image crystalImage;
    [SerializeField] private Image swordImage;
    [SerializeField] private Image blackholeImage;
    [SerializeField] private Image flaskImage;

    private SkillManager skills;
    [Header("Almas info")]
    [SerializeField] private TextMeshProUGUI currentSouls;
    [SerializeField] private float soulsAmount;
    [SerializeField] private float soulsSpeed = 100;

    void Start() {
        if (playerStats != null)
            playerStats.onHealthChanged += UpdateHealthUI;

        skills = SkillManager.instance;
    }


    void Update() {
        UpdateSoulsUI();

        if (Input.GetKeyDown(KeyCode.LeftShift) && skills.Dash.DashUnlocked)
            SetCooldownOf(dashImage);

        if (Input.GetKeyDown(KeyCode.Q) && skills.Parry.parryUnlocked)
            SetCooldownOf(parryImage);

        if (Input.GetKeyDown(KeyCode.Mouse1) && skills.Sword.swordUnlocked)
            SetCooldownOf(swordImage);

        if (Input.GetKeyDown(KeyCode.R) && skills.Blackhole.blackholeUnlocked)
            SetCooldownOf(blackholeImage);

        if (Input.GetKeyDown(KeyCode.Alpha1) && Inventory.instance.GetEquipment(EquipmentType.Elixir) != null)
            SetCooldownOf(flaskImage);

        CheckCooldownOf(dashImage, skills.Dash.cooldown);
        CheckCooldownOf(parryImage, skills.Parry.cooldown);
        CheckCooldownOf(swordImage, skills.Sword.cooldown);
        CheckCooldownOf(blackholeImage, skills.Blackhole.cooldown);
        CheckCooldownOf(flaskImage, Inventory.instance.FlaskCooldown);
    }

    private void UpdateSoulsUI() {
        if (soulsAmount < PlayerManager.instance.GetSouls())
            soulsAmount += Time.deltaTime * soulsSpeed;
        else
            soulsAmount = PlayerManager.instance.GetSouls();

        currentSouls.text = ((int)soulsAmount).ToString();
    }

    private void UpdateHealthUI() {
        slider.maxValue = playerStats.GetMaxHealthValue();
        slider.value = playerStats.currentHealth;
    }


    private void SetCooldownOf(Image _image) {
        if (_image.fillAmount <= 0)
            _image.fillAmount = 1;
    }

    private void CheckCooldownOf(Image _image, float _cooldown) {
        if (_image.fillAmount > 0)
            _image.fillAmount -= 1 / _cooldown * Time.deltaTime;
    }


}

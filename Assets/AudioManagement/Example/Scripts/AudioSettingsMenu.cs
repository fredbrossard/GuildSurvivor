using Audio;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;
using Utils;
using Zenject;

public class AudioSettingsMenu : MonoBehaviour
{
    [Inject] AudioService m_audioService;

    [SerializeField] private UIDocument document;
    [SerializeField] private AudioSettingBind[] audioSettingBinds;

    private VisualElement m_root;
    private Button m_saveBtn;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_audioService.Play("Theme1");
        TaskUtils.OnSameThread(() => Bind());
    }

    private void OnDestroy()
    {
        if(m_saveBtn != null)
            m_saveBtn.clicked -= SaveVolumes;
    }

    private async Task Bind()
    {
        m_root = document.rootVisualElement;
        
        m_saveBtn = m_root.Q<Button>("save-btn");
        m_saveBtn.clicked += SaveVolumes;

        await new WaitUntil(() => m_audioService.IsInitialize);

        foreach (var bind in audioSettingBinds)
        {
            SliderInt slider = m_root.Q<SliderInt>(bind.sliderName);
            slider.value = (int)(m_audioService.GetVolume(bind.mixerGroup.name) * 100f);

            TextElement label = slider.Q<TextElement>("volume-text");
            label.text = slider.value.ToString();

            slider.RegisterValueChangedCallback(evt =>
            {
                slider.SetValueWithoutNotify((int)evt.newValue);
                m_audioService.SetVolume(bind.mixerGroup.name, ((float)evt.newValue / 100f));

                label.text = evt.newValue.ToString();
                Debug.Log("Slider changed value");
            });
        }
    }

    private void SaveVolumes()
    {
        foreach (var bind in audioSettingBinds)
            m_audioService.SaveVolumeByPlayerPref(bind.mixerGroup.name);

        Debug.Log("Volumes are saved");
    }
}

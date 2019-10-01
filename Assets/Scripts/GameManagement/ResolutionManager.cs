using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResolutionManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Dropdown  resolutionDropdown;
    Resolution[] resolutions;

    int currentResolutionIndex = 0;
    void Start()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            } 
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }
    public void SetResolution(int resolutionIndex)
    {
       Resolution resolution = resolutions[resolutionIndex];
       Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);

    }
   public void FullScreen(bool isFullScreen)
   {
       Screen.fullScreen = isFullScreen;
   }
}

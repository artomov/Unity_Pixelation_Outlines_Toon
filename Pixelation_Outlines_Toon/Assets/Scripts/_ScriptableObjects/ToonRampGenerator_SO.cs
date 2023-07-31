using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** <summary>
Generates ramp textures based on gradients and saves it to a folder.
Requires an external monitoring script which would invoke texture generation
</summary> **/

[CreateAssetMenu(fileName = "ToonRampGenerator_SO", menuName = "SciptableObjects/ToonRampGenerator")]
public class ToonRampGenerator_SO : ScriptableObject
{
    [Header("Gradients")]
    [SerializeField] private Gradient _mainRampGradient;
    [SerializeField] private Gradient _additionalRampGradient;

    [Space]
    [Header("Where to save? (note that folder should exist)")]
    [SerializeField] private string folderName;
    [SerializeField] private string fileNameModifier;

    [Space]
    [Header("Update Ramps? \n(unchecks automatically when updated)")]
    [SerializeField] private bool _shouldUpdateRamps;
    public bool ShouldUpdateRamps // property
    {
        get => _shouldUpdateRamps;
        set {_shouldUpdateRamps = value;}
    }

    private Texture2D _mainRampTexture;
    private Texture2D _additionalRampTexture;

    private int _TextureWidth = 100;

    public void GenerateRampTextures()
    {
        _mainRampTexture = new Texture2D(_TextureWidth, 1);
        _additionalRampTexture = new Texture2D(_TextureWidth, 1);

        _mainRampTexture.filterMode = FilterMode.Point;
        _additionalRampTexture.filterMode = FilterMode.Point;

        Color currentGradColor = new Color();
        for (int i = 0; i < _TextureWidth; i++)
        {
            currentGradColor = _mainRampGradient.Evaluate((float)(i + 1) / (float)_TextureWidth);
            _mainRampTexture.SetPixel(i, 0, currentGradColor);

            currentGradColor = _additionalRampGradient.Evaluate((float)(i + 1) / (float)_TextureWidth);
            _additionalRampTexture.SetPixel(i, 0, currentGradColor);
        }
        _mainRampTexture.Apply();
        _additionalRampTexture.Apply();

        SaveTextures();
    }

    private void SaveTextures()
    {
        string mainRampFileName = Application.dataPath + $"/{folderName}" + $"/{fileNameModifier}_MainRamp.PNG";
        string additionalRampFileName = Application.dataPath + $"/{folderName}" + $"/{fileNameModifier}_AdditionalRamp.PNG";

        byte[] mainRampBytes = _mainRampTexture.EncodeToPNG();
        byte[] additionalRampBytes = _additionalRampTexture.EncodeToPNG();

        System.IO.File.WriteAllBytes(mainRampFileName, mainRampBytes);
        System.IO.File.WriteAllBytes(additionalRampFileName, additionalRampBytes);
    }
}

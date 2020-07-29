using UnityEngine;
using Klak.Chromatics;

[ExecuteInEditMode, RequireComponent(typeof(Camera))]
public class GradientOverlay : MonoBehaviour
{
    #region Editable properties

    [Space]
    [SerializeField] CosineGradient _gradient = CosineGradient.DefaultGradient;
    [SerializeField, Range(0, 1)] float _opacity = 1;

    [Space]
    [SerializeField, Range(-180, 180)] float _direction = 0;
    [SerializeField, Range(0.01f, 2)] float _frequency = 0.5f;
    [SerializeField, Range(0.01f, 1)] float _scrollSpeed = 0.5f;

    [Space]
    [SerializeField, Range(0, 1)] float _noiseStrength = 1;
    [SerializeField, Range(0.01f, 1)] float _noiseAnimation = 0.5f;

    #endregion

    #region Private members

    [SerializeField, HideInInspector] Shader _shader = null;

    Material _material;
    float _scroll;
    float _noiseOffset;

    #endregion

    #region MonoBehaviour functions

    void OnDestroy()
    {
        if (Application.isPlaying)
            Destroy(_material);
        else
            DestroyImmediate(_material);
    }

    void Update()
    {
        if (!Application.isPlaying) return;

        _scroll += _scrollSpeed * Time.deltaTime;
        _noiseOffset += _noiseAnimation * Time.deltaTime;
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (_material == null)
        {
            _material = new Material(_shader);
            _material.hideFlags = HideFlags.DontSave;
        }

        _material.SetMatrix("_Gradient", _gradient);
        _material.SetFloat("_Opacity", _opacity);

        var rad = Mathf.Deg2Rad * _direction;
        var dir = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
        _material.SetVector("_Direction", dir);

        _material.SetFloat("_Frequency", _frequency);
        _material.SetFloat("_Scroll", _scroll);

        _material.SetFloat("_NoiseStrength", _noiseStrength);
        _material.SetFloat("_NoiseAnimation", _noiseOffset);

        Graphics.Blit(source, destination, _material, 0);
    }

    #endregion
}

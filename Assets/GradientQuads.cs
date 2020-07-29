using UnityEngine;
using Klak.Chromatics;

[ExecuteInEditMode]
public class GradientQuads : MonoBehaviour
{
    [SerializeField] CosineGradient _grad = CosineGradient.DefaultGradient;
    [SerializeField] Material _material = null;
    [SerializeField] int _quadCount = 16;

    [SerializeField, HideInInspector] Mesh _mesh = null;

    MaterialPropertyBlock _block;

    public void Update()
    {
        if (_block == null) _block = new MaterialPropertyBlock();

        for (var i = 0; i < _quadCount; i++)
        {
            var pos = transform.TransformPoint(Vector3.right * (i * 1.05f));

            _block.SetColor("_Color", _grad.Evaluate(i / (_quadCount - 1.0f)));

            Graphics.DrawMesh
              (_mesh, pos, Quaternion.identity,
               _material, gameObject.layer, null, 0, _block);
        }
    }
}

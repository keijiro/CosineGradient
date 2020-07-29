float3 CosineGradient(float3 A, float3 B, float3 C, float3 D, float t)
{
    return saturate(A + B * cos(C * t + D));
}

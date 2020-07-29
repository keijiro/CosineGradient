float3 CosineGradient(float3 A, float3 B, float3 C, float3 D, float t)
{
    return saturate(A + B * cos(C * t + D));
}

float3 CosineGradient(float4x3 grad, float t)
{
    const float tau = 6.283185307179586476925;
    return CosineGradient(grad._11_12_13,
                          grad._21_22_23,
                          grad._31_32_33 * tau,
                          grad._41_42_43 * tau, t);
}

float3 CosineGradient(float4x4 grad, float t)
{
    const float tau = 6.283185307179586476925;
    return CosineGradient(grad._11_12_13,
                          grad._21_22_23,
                          grad._31_32_33 * tau,
                          grad._41_42_43 * tau, t);
}

void ThreeColorGradient_half(
    half3 TopColor,
    half3 MiddleColor,
    half3 BottomColor,
    half MiddlePosition,
    half2 UV,
    out half3 OutColor
)
{

    half is_top  = step(MiddlePosition, UV.y);

    half3 col1 = lerp(BottomColor, MiddleColor, is_top);
    half3 col2 = lerp(MiddleColor, TopColor, is_top);

    half percentage = (UV.y - (is_top * MiddlePosition))/
        (is_top + (-2 * is_top + 1) * MiddlePosition);

    OutColor = lerp(col1, col2, percentage);
}
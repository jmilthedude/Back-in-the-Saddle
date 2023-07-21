using UnityEngine;
using UnityEngine.UI;

public class AlphaImage : Image
{
    [SerializeField] private float alphaHitThreshold;

    private void Update()
    {
        // ReSharper disable once CompareOfFloatsByEqualityOperator
        if (alphaHitThreshold != alphaHitTestMinimumThreshold)
        {
            alphaHitTestMinimumThreshold = alphaHitThreshold;
        }
    }
}
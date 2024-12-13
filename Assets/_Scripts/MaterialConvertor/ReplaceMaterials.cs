using UnityEngine;
using DG.Tweening;

public class ReplaceMaterials : MonoBehaviour
{
    public float fadeDuration = 1f;
    public Material targetMaterial;
    public float semiTransparentAlpha = 0.5f;

    void Start()
    {
        if (targetMaterial == null)
        {
            Debug.LogError("no material.");
            return;
        }

        if (targetMaterial.HasProperty("_Color"))
        {
            Color initialColor = targetMaterial.color;
            targetMaterial.DOColor(new Color(initialColor.r, initialColor.g, initialColor.b, 0), fadeDuration).OnComplete(() =>
            {
                Debug.Log("Materyal fade out .");

                //Invoke("FadeToSemiTransparent", 22f);
            });
        }
        else
        {
            Debug.LogError(" materyal '_Color' ");
        }
    }
    public void FadeToSemiTransparent()
    {
        if (targetMaterial.HasProperty("_Color"))
        {
            Color initialColor = targetMaterial.color;
            targetMaterial.DOColor(new Color(initialColor.r, initialColor.g, initialColor.b, semiTransparentAlpha), fadeDuration).OnComplete(() =>
            {
                Debug.Log("Materyal half transparant");
            });
        }
        else
        {
            Debug.LogError(" materyal '_Color'");
        }
    }
}

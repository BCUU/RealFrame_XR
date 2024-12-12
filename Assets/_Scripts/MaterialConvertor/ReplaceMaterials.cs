using UnityEngine;
using DG.Tweening;

public class ReplaceMaterials : MonoBehaviour
{
    // Fade s�resi i�in h�z parametresi
    public float fadeDuration = 1f;
    // Fade out uygulanacak materyal
    public Material targetMaterial;

    void Start()
    {
        if (targetMaterial == null)
        {
            Debug.LogError("Hedef materyal atanmad�. L�tfen bir materyal atay�n.");
            return;
        }

        // E�er materyal bir shader'da "_Color" �zelli�ine sahipse, fade out uygula
        if (targetMaterial.HasProperty("_Color"))
        {
            Color initialColor = targetMaterial.color;
            targetMaterial.DOColor(new Color(initialColor.r, initialColor.g, initialColor.b, 0), fadeDuration).OnComplete(() =>
            {
                Debug.Log("Materyal fade out tamamland�.");
            });
        }
        else
        {
            Debug.LogError("Hedef materyal '_Color' �zelli�ine sahip de�il. Fade out uygulanamad�.");
        }
    }
}

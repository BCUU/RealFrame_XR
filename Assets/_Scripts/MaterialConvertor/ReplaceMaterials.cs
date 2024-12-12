using UnityEngine;
using DG.Tweening;

public class ReplaceMaterials : MonoBehaviour
{
    // Fade süresi için hýz parametresi
    public float fadeDuration = 1f;
    // Fade out uygulanacak materyal
    public Material targetMaterial;

    void Start()
    {
        if (targetMaterial == null)
        {
            Debug.LogError("Hedef materyal atanmadý. Lütfen bir materyal atayýn.");
            return;
        }

        // Eðer materyal bir shader'da "_Color" özelliðine sahipse, fade out uygula
        if (targetMaterial.HasProperty("_Color"))
        {
            Color initialColor = targetMaterial.color;
            targetMaterial.DOColor(new Color(initialColor.r, initialColor.g, initialColor.b, 0), fadeDuration).OnComplete(() =>
            {
                Debug.Log("Materyal fade out tamamlandý.");
            });
        }
        else
        {
            Debug.LogError("Hedef materyal '_Color' özelliðine sahip deðil. Fade out uygulanamadý.");
        }
    }
}

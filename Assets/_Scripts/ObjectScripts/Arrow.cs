using UnityEngine;
using DG.Tweening; // DoTween kütüphanesini kullanmayý unutma

public class Arrow : MonoBehaviour
{
    private bool isStuck = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (!isStuck && collision.gameObject.CompareTag("Untagged"))
        {
            isStuck = true;

            // Hareketi durdur
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.isKinematic = true;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            // Çarpma noktasýnda okun pozisyonunu ayarla
            ContactPoint contact = collision.contacts[0];
            transform.position = contact.point;

            // Dinamik olarak okun yönünü ayarla
            Vector3 incomingDirection = rb.velocity.normalized; // Okun çarpma yönü
            Vector3 normal = contact.normal;                   // Çarpma yüzeyinin normali
            Quaternion targetRotation = Quaternion.LookRotation(-normal, Vector3.up);
            transform.rotation = targetRotation;

            // Titreþim efekti
            DoShakeEffect();
        }
    }

    private void DoShakeEffect()
    {
        // Titreþim hareketi (örneðin pozisyon üzerinde küçük hareket)
        transform.DOShakePosition(0.2f, 0.1f, 10, 90, false, true)
            .OnComplete(() =>
            {
                // Efekt sonrasý pozisyonu stabilize et (çarpma noktasýnda kalmasý için)
                transform.position = transform.position;
            });
    }
}

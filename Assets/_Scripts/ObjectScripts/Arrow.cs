using UnityEngine;
using DG.Tweening; 

public class Arrow : MonoBehaviour
{
    private bool isStuck = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (!isStuck && collision.gameObject.CompareTag("Untagged"))
        {
            isStuck = true;

            Rigidbody rb = GetComponent<Rigidbody>();
            rb.isKinematic = true;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            ContactPoint contact = collision.contacts[0];
            transform.position = contact.point;

            
            Vector3 incomingDirection = rb.velocity.normalized; 
            Vector3 normal = contact.normal;                   
            Quaternion targetRotation = Quaternion.LookRotation(-normal, Vector3.up);
            transform.rotation = targetRotation;

           
            DoShakeEffect();
        }
    }

    private void DoShakeEffect()
    {
        transform.DOShakePosition(0.2f, 0.1f, 10, 90, false, true)
            .OnComplete(() =>
            {
                transform.position = transform.position;
            });
    }
}

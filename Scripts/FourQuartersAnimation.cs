
using UnityEngine;

public class FourQuartersAnimation : MonoBehaviour
{
    public Animator Animator;
    public AnimationAttribute AnimationAttribute;
    public Rigidbody CharacterRigidbody;

    // animation IDs
    private int AnimIDSpeed;
    private int AnimIDSpeedGain;
    private int AnimIDSpeedRight;

    // Start is called before the first frame update
    private void Start()
    {
        AssignAnimationIDs();
        Debug.Assert(Animator != null);
        Debug.Assert(AnimationAttribute != null);
        Debug.Assert(CharacterRigidbody != null);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 localVelocity = CharacterRigidbody.transform.InverseTransformVector(CharacterRigidbody.velocity);
        Vector2 horizontalVelocity;
        horizontalVelocity.x = localVelocity.x;
        horizontalVelocity.y = localVelocity.z;
        float forwardVelocity = localVelocity.z;
        float rightVelocity = localVelocity.x;
        float absVelocity = horizontalVelocity.magnitude;
        float speed = 0;
        if (absVelocity >= AnimationAttribute.SpeedStopThreshold)
        {
            // cosθ
            speed = Mathf.Abs(rightVelocity / absVelocity);
        }
        float velocityForwardSign = 1;
        if (Mathf.Abs(forwardVelocity) >= AnimationAttribute.SpeedStopThreshold)
        {
            velocityForwardSign = Mathf.Sign(forwardVelocity);
        }

        Animator.SetFloat(AnimIDSpeed, speed);
        // 後ろ移動の時はアニメーションを逆再生する
        Animator.SetFloat(AnimIDSpeedGain, velocityForwardSign * Mathf.Min(absVelocity / AnimationAttribute.maxSpeed, 1.0f));
        // 右後ろ・左後ろ移動の時は左右が逆になる
        Animator.SetFloat(AnimIDSpeedRight, velocityForwardSign * rightVelocity);
    }
    private void AssignAnimationIDs()
    {
        AnimIDSpeed = Animator.StringToHash("Speed");
        AnimIDSpeedGain = Animator.StringToHash("SpeedGain");
        AnimIDSpeedRight = Animator.StringToHash("SpeedRight");
    }
}

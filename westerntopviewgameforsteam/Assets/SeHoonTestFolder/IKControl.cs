namespace SeHoonStuff
{
    using UnityEngine;

    public class IKControl : MonoBehaviour
    {

        public Animator animator;

        public bool ikActive = false;
        public Transform rightHandObj = null;

        //this values are using for look at IK
        [SerializeField]private float lookWeight = 1f;
        [SerializeField] private float bodyWeight = 0.6f;
        [SerializeField]private float headWeight = 0.9f;
        [SerializeField]private float eyesWeight = 1f;
        [SerializeField]private float clampWeight = 1f;

        
        //
        private Vector3 targetPos;
        

        public Vector3 SetMousePos()
        {
            targetPos = Input.mousePosition;
            targetPos.z = 10f;
            targetPos = Camera.main.ScreenToWorldPoint(targetPos);
            targetPos.y = 1.5f;

            return targetPos;
        }

        //a callback for calculating IK
        void OnAnimatorIK()
        {
            if (animator)
            {
                //if the IK is active, set the position and rotation directly to the goal.
                if (ikActive)
                {



                    // Set the look target position, if one has been assigned

                    animator.SetLookAtWeight(lookWeight, bodyWeight, headWeight, eyesWeight, clampWeight);
                    animator.SetLookAtPosition(targetPos);

                    // Set the right hand target position and rotation, if one has been assigned
                    if (rightHandObj != null)
                    {
                        
                        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
                        animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
                        animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandObj.position);
                        animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandObj.rotation);
                    }

                }

                //if the IK is not active, set the position and rotation of the hand and head back to the original position
                else
                {
                    animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
                    animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
                    animator.SetLookAtWeight(0);
                }
            }
        }
    }
}

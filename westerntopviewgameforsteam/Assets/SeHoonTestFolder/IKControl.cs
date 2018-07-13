
using UnityEngine;

public class IKControl : MonoBehaviour
{

	public eGunType gunType = eGunType.Revolver; 


    //private Player player;
	private Actor actor;

    public Animator animator;

    public bool ikActive = false;
    public Transform rightHandObj = null;
    public Transform rightElbowObj = null;

	public Transform leftHandObj = null;



    //this values are using for look at IK
    [SerializeField]private float lookWeight = 1f;
    [SerializeField] private float bodyWeight = 0.6f;
    [SerializeField]private float headWeight = 0.9f;
    [SerializeField]private float eyesWeight = 1f;
    [SerializeField]private float clampWeight = 1f;

        
    //
    private Vector3 targetPos;

        

    private void Awake()
    {
        //player = GetComponent<Player>();
		actor = GetComponent<Actor>();
	}



    //a callback for calculating IK
    void OnAnimatorIK()
    {
        if (animator)
        {
            //if the IK is active, set the position and rotation directly to the goal.
            if (ikActive)
            {

				targetPos = actor.TargetPos();
			// Set the look target position, if one has been assigned

			//if ((targetPos - transform.position).sqrMagnitude >= 2)
			//{
			//	animator.SetLookAtWeight(lookWeight, bodyWeight, headWeight, eyesWeight, clampWeight);


			//	animator.SetLookAtPosition(targetPos);
			//}	
                // Set the right hand target position and rotation, if one has been assigned


                if (rightHandObj != null)
                {
                        
                    animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
                    animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
                    animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandObj.position);
                    animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandObj.rotation);

                //      animator.SetIKHintPositionWeight(AvatarIKHint.RightElbow, 1);
                //    animator.SetIKHintPosition(AvatarIKHint.RightElbow, rightElbowObj.position);
                }


				if(gunType == eGunType.Rifle || gunType == eGunType.Shotgun)
				{
					animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
					animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
					animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandObj.position);
					animator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandObj.rotation);
				}
				else if(gunType == eGunType.Revolver)
				{
					leftHandObj = null;
					animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0);
					animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0);
				}


            }

            //if the IK is not active, set the position and rotation of the hand and head back to the original position
            else
            {
                animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
                animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
                animator.SetLookAtWeight(0);

                    //animator.SetIKHintPositionWeight(AvatarIKHint.RightElbow, 0);
                    
            }
        }
    }
}


namespace SeHoonStuff
{
    using UnityEngine;

    public class Player : MonoBehaviour
    {
        public enum MovementType
        {
            Type1,Type2
        }

        public MovementType moveType = MovementType.Type1;
        private Animator anim;
        private Vector3 targetPos;

        float rotateAngle = 0f;
        Vector3 rotateVector;
        private void Awake()
        {
            anim = GetComponent<Animator>();

            if (moveType == MovementType.Type1)
                anim.SetInteger("MoveType", 0);
            else if(moveType == MovementType.Type2)
                anim.SetInteger("MoveType", 1);



        }

        private float GetrotateAngle()
        {
            Vector3 forwordDir = transform.forward.normalized;
            Vector3 changedDIr = (targetPos - transform.position).normalized;
            
            rotateAngle = Quaternion.FromToRotation(forwordDir, changedDIr).eulerAngles.y;
            if (rotateAngle > 180f)
                rotateAngle -= 360f;

            return rotateAngle/180f;
        }

        public Vector3 SetMousePos()
        {
            targetPos = Input.mousePosition;
            targetPos.z = 10f;
            targetPos = Camera.main.ScreenToWorldPoint(targetPos);
            targetPos.y = 1.5f;

            return targetPos;
        }

        void FixedUpdate()
        {

            if(moveType == MovementType.Type1)
            {
                float x = Input.GetAxis("Horizontal");
                float y = Input.GetAxis("Vertical");
                Vector3 dir = new Vector3(x, 0, y);

				Vector3 moveDir = transform.InverseTransformDirection(dir);

                //TODO: change this value 
                
               // dir = Quaternion.FromToRotation(dir, transform.forward) * dir;
                transform.Rotate(new Vector3(0, rotateAngle * 10f * Time.fixedDeltaTime, 0));



                //Quaternion rot = Quaternion.FromToRotation(dir, transform.forward);
                
                //dir = rot * dir;
                //Debug.Log(dir);
                
                //actual moving is working with root anim
                UpdateAnim(moveDir.x, moveDir.z);
            }
            else if(moveType == MovementType.Type2)
            {
                float y = Input.GetAxis("Vertical");
                float rotation = Input.GetAxis("Horizontal");
                UpdateAnim(rotation,y);
            }



        }
        


        void UpdateAnim(float x, float y)
        {
            if(moveType == MovementType.Type1)
            {
                SetMousePos();
                anim.SetFloat("X", x, 0.1f, Time.deltaTime);
                anim.SetFloat("Y", y, 0.1f, Time.deltaTime);
                anim.SetFloat("rotation", GetrotateAngle());
            }
            else if (moveType == MovementType.Type2)
            {
                anim.SetFloat("rotation", x);
                anim.SetFloat("Y", y, 0.1f, Time.deltaTime);
            }
        }



    }

}

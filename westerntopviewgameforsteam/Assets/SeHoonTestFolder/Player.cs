namespace SeHoonStuff
{
    using UnityEngine;

    public class Player : MonoBehaviour
    {

        private Animator anim;
        private Vector3 targetPos;

        float rotateAngle = 0f;

        private void Awake()
        {
            anim = GetComponent<Animator>();
           
        }

        private float GetrotateAngle()
        {
            Vector3 forwordDir = transform.forward;
            Vector3 changedDIr = (targetPos - transform.position);

            float rotateAngle = Quaternion.FromToRotation(forwordDir, changedDIr).eulerAngles.y;
            Debug.Log(rotateAngle);


            return rotateAngle;
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

            GetrotateAngle();
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");

            UpdateAnim(x, y);




        }
        


        void UpdateAnim(float x, float y)
        {

            SetMousePos();

            anim.SetFloat("X", x, 0.1f, Time.deltaTime);
            anim.SetFloat("Y", y, 0.1f, Time.deltaTime);
        }



    }

}

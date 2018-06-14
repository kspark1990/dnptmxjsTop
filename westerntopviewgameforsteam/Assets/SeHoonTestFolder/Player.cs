namespace SeHoonStuff
{
    using UnityEngine;

    public class Player : MonoBehaviour
    {

        private Animator anim;
        private IKControl IK;
        

        private void Awake()
        {
            anim = GetComponent<Animator>();
            IK = GetComponent<IKControl>();
        }

        void FixedUpdate()
        {
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");

            UpdateAnim(x, y);




        }



        void UpdateAnim(float x, float y)
        {

            IK.SetMousePos();
            anim.SetFloat("X", x, 0.1f, Time.deltaTime);
            anim.SetFloat("Y", y, 0.1f, Time.deltaTime);
        }


    }

}

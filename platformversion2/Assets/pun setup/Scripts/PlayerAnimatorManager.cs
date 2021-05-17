using UnityEngine;

namespace Photon.Pun.Demo.PunBasics
{
	public class PlayerAnimatorManager : MonoBehaviourPun 
	{
        #region Private Fields

        [SerializeField]
		private float directionDampTime = 0.25f;
		float vone;
		float co1;
		float co2;

		[SerializeField]
		private float jmult= 1f;
		private float jval = 0.7f;


		private bool jumpbool = false;
		private float jumpfloat;

		Animator animator;
		CharacterController controller;
		Rigidbody rigidbody;
		[SerializeField]
		private float jumpHeight = 100f;
		#endregion

		#region MonoBehaviour CallBacks

		/// <summary>
		/// MonoBehaviour method called on GameObject by Unity during initialization phase.
		/// </summary>
		void Start () 
	    {
	        animator = GetComponent<Animator>();
			controller= GetComponent<CharacterController>();
			rigidbody= GetComponent<Rigidbody>();

		}

		/// <summary>
		/// MonoBehaviour method called on GameObject by Unity on every frame.
		/// </summary>
		void Update()
		{

			// Prevent control is connected to Photon and represent the localPlayer
			if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
			{
				return;
			}

			// failSafe is missing Animator component on GameObject
			if (!animator)
			{
				return;
			}

			// deal with Jumping
			AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

			// only allow jumping if we are running.
			if (stateInfo.IsName("Base Layer.Run"))
			{
				// When using trigger parameter
				if (Input.GetButtonDown("Jump"))
				{
					jumpbool = true;
					animator.SetTrigger("Jump");
					vone = PlayerPrefs.GetFloat("varone", 1);
					jval = Mathf.Lerp(0.5f,1.5f,vone / 600);
					//controller.Move(new Vector3(0, (vone - 0) / (300 - 0) * (300 - 100) + 100, 0) * Time.deltaTime);

				}

			}

			if (jumpbool == true)
            {
				jumpfloat += 0.031f;
				controller.Move(new Vector3(0.0f, jumpfloat));
				if (jumpfloat >= jmult*jval)
                {
					jumpbool = false;
					jumpfloat = 0.0f;
				}

			}


			//When entering the Jump state in the Animator, output the message in the console
			if (stateInfo.IsName("Jump"))
			{

			}

			// deal with movement
			float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

			// prevent negative Speed.
            if( v < 0 )
            {
                v = 0;
            }

			// set the Animator Parameters

			co1=PlayerPrefs.GetFloat("co1");
			co2=PlayerPrefs.GetFloat("co2");
			
			animator.SetFloat( "Direction", h);

			animator.SetFloat("Speed", (h * h + v * v));

			//rigidbody.AddForce(h, 0, v, ForceMode.Impulse);

			if (stateInfo.IsName("Run"))
			{
				//this.transform.Rotate(0.0f, -Mathf.Atan(co1 / co2)/5, 0.0f, Space.World);
			}
		}

		#endregion

	}
}
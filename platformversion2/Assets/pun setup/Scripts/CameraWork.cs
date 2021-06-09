// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CameraWork.cs" company="Exit Games GmbH">
//   Part of: Photon Unity Networking Demos
// </copyright>
// <summary>
//  Used in PUN Basics Tutorial to deal with the Camera work to follow the player
// </summary>
// <author>developer@exitgames.com</author>
// --------------------------------------------------------------------------------------------------------------------

using UnityEngine;

namespace Photon.Pun.Demo.PunBasics
{
	/// <summary>
	/// Camera work. Follow a target
	/// </summary>
	public class CameraWork : MonoBehaviour
	{
		#region Private Fields
		private Vector3 offpos;

	    [Tooltip("The distance in the local x-z plane to the target")]
	    [SerializeField]
	    private float distance = 7.0f;
	    
	    [Tooltip("The height we want the camera to be above the target")]
	    [SerializeField]
	    private float height = 3.0f;
	    
	    [Tooltip("Allow the camera to be offseted vertically from the target, for example giving more view of the sceneray and less ground.")]
	    [SerializeField]
	    private Vector3 centerOffset = Vector3.zero;

	    [Tooltip("Set this as false if a component of a prefab being instanciated by Photon Network, and manually call OnStartFollowing() when and if needed.")]
	    [SerializeField]
	    private bool followOnStart = false;

	    [Tooltip("The Smoothing for the camera to follow the target")]
	    [SerializeField]
	    private float smoothSpeed = 0.125f;

		[Tooltip("Rotation Multiplier")]
		[SerializeField]
		private float r_mult = 0.05f;


		[Tooltip("Character GameObject to control")]
		[SerializeField]
		private GameObject character;

		[Tooltip("Character GameObject to control")]
		[SerializeField]
		private GameObject referencer;

		[Tooltip("Character GameObject to control")]
		[SerializeField]
		private GameObject referencer2;

		Animator animator;

		private float xx;

		private float yy;
		static Vector2 coo;
		private float zoomval=1.0f;
		private float cval;

		// cached transform of the target
		Transform cameraTransform;

		// maintain a flag internally to reconnect if target is lost or camera is switched
		bool isFollowing;
		
		// Cache for camera offset
		Vector3 cameraOffset = Vector3.zero;
		private bool otherbool;
		
		
        #endregion

        #region MonoBehaviour Callbacks

        /// <summary>
        /// MonoBehaviour method called on GameObject by Unity during initialization phase
        /// </summary>
        void Start()
		{
			animator = GetComponent<Animator>();
			cameraOffset.z = -distance;
			// Start following the target if wanted.
			if (followOnStart)
			{
				OnStartFollowing();
			}
		}


		void LateUpdate()
		{
			// The transform target may not destroy on level load, 
			// so we need to cover corner cases where the Main Camera is different everytime we load a new scene, and reconnect when that happens
			if (cameraTransform == null && isFollowing)
			{
				OnStartFollowing();
			}

			// only follow is explicitly declared
			if (isFollowing) {
				FollowTak2();
			}
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Raises the start following event. 
		/// Use this when you don't know at the time of editing what to follow, typically instances managed by the photon network.
		/// </summary>
		public void OnStartFollowing()
		{	      
			cameraTransform = Camera.main.transform;
			referencer.transform.position = Camera.main.transform.position;
			referencer.transform.rotation= Camera.main.transform.rotation;
			isFollowing = true;
			// we don't smooth anything, we go straight to the right camera shot
			Cut();
		}
		
		#endregion

		#region Private Methods

		/// <summary>
		/// Follow the target smoothly
		/// </summary>
		void Follow()
		{
			cameraOffset.z = -distance;
			cameraOffset.y = height;
			
		    cameraTransform.position = Vector3.Lerp(cameraTransform.position, this.transform.position +this.transform.TransformVector(cameraOffset), smoothSpeed*Time.deltaTime);

		    cameraTransform.LookAt(this.transform.position + centerOffset);
		    
	    }


		void FollowTak()
		{
			//x drehung um achse, y ist oben unten

			xx = (Input.mousePosition.x - (Screen.width) / 2)/ (Screen.width / 2);
			yy = (Input.mousePosition.y - Screen.height / 2) / (Screen.height / 2);
			xx *=10;
			yy *= height*-1;
			//Debug.Log(xx);
			//Debug.Log(yy);

			cameraOffset.z = -distance;
			cameraOffset.y = height;
			//cameraOffset.y += yy;
			//cameraOffset.x += xx;


			cameraTransform.position = Vector3.Lerp(cameraTransform.position, this.transform.position + this.transform.TransformVector(cameraOffset)+new Vector3(xx,yy,0), smoothSpeed * Time.deltaTime);

			cameraTransform.LookAt(this.transform.position + centerOffset);

		}


		void FollowTak2()
		{
			Vector3 drip = cameraTransform.transform.eulerAngles;
			
			//x drehung um achse, y ist oben unten
			//xx = (Input.mousePosition.x - (Screen.width) / 2) / (Screen.width / 2);
			xx = Input.GetAxis("Mouse X");
			yy = (Input.mousePosition.y - Screen.height / 2) / (Screen.height / 2);
			xx *= r_mult;


			yy *= height * -1;
			
			//Debug.Log(yy);
			offpos =  cameraOffset;
			
			Vector2 offp = new Vector2(offpos.x*Mathf.Cos(xx)- offpos.z*Mathf.Sin(xx), offpos.x * Mathf.Sin(xx) + offpos.z * Mathf.Cos(xx));
			Vector3 rotpos = new Vector3(1.0f,0.0f,0.0f);
			Vector2 charrotp = new Vector2(rotpos.x * Mathf.Cos(xx) - rotpos.z * Mathf.Sin(xx), rotpos.x * Mathf.Sin(xx) + rotpos.z * Mathf.Cos(xx));
            //Debug.Log("val xx: "+ xx + "val offp: " + offp+"val offpos: "+ offpos);

            //Debug.Log(drip);

            cameraOffset.y = height;
			cameraOffset.z = offp.y;
			cameraOffset.x = offp.x;
			PlayerPrefs.SetFloat("co1", offp.x);
			PlayerPrefs.SetFloat("co2", offp.y);
			referencer.transform.position = this.transform.position + (cameraOffset) + new Vector3(0, yy, 0);
			referencer.transform.LookAt(this.transform.position + centerOffset);
            cameraTransform.position = referencer.transform.position;

            //cameraTransform.position = Vector3.Lerp(cameraTransform.position, this.transform.position + this.transform.TransformVector(cameraOffset) + new Vector3(0, yy, 0), smoothSpeed * Time.deltaTime);
            //cameraTransform.position = Vector3.Lerp(cameraTransform.position, referencer.transform.position, smoothSpeed * Time.deltaTime);
            cameraTransform.LookAt(this.transform.position + centerOffset);
            //cameraTransform.LookAt(this.transform.position + centerOffset);
            zoomval += Input.GetAxis("Mouse ScrollWheel")/-2.0f;
            //Debug.Log("scroolwheel:"+ Input.GetAxis("Mouse ScrollWheel"));
            zoomval = Mathf.Clamp(zoomval, -1.15f, 0.0f);
			cval = Mathf.Lerp(cval, zoomval,10.0f * Time.deltaTime);
	
            cameraTransform.position = cval * (referencer.transform.position-(this.transform.position+centerOffset))+ referencer.transform.position;


			Vector3 drip2 = cameraTransform.transform.eulerAngles;

            //character.transform.rotation = Quaternion.Euler(0, cameraTransform.transform.rotation.eulerAngles.y,0);
            //character.transform.Rotate(0.0f, -drip.y + drip2.y, 0.0f, Space.World);

            //character.transform.Rotate(0.0f, -Mathf.Atan(offp.x / offp.y) / 2, 0.0f, Space.World);


            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Run") || animator.GetAnimatorTransitionInfo(0).IsName("Idle 0 -> Base Layer.Run"))
            {
                if (otherbool)
                {
                    referencer2.transform.Rotate(0.0f, -drip.y + drip2.y, 0.0f, Space.World);
                    character.transform.rotation = referencer2.transform.rotation;
                    otherbool = false;
                }
                else
                {
                    referencer2.transform.rotation = character.transform.rotation;
                    referencer2.transform.Rotate(0.0f, -drip.y + drip2.y, 0.0f, Space.World);
                    character.transform.rotation = referencer2.transform.rotation;
                }

            }
            else
            {
                referencer2.transform.Rotate(0.0f, -drip.y + drip2.y, 0.0f, Space.World);
                otherbool = true;
            }




        }


		void Cut()
		{
			cameraOffset.z = -distance;
			cameraOffset.y = height;

			cameraTransform.position = this.transform.position + this.transform.TransformVector(cameraOffset);

			cameraTransform.LookAt(this.transform.position + centerOffset);
		}
		#endregion
	}
}
using UnityEngine;
using System.Collections;

namespace Pacman {

	public class PacmanController : MonoBehaviour {

		public float MoveSpeed;
		public Stage CurrentStage;

		float mTimer;
		Vector3 mNextPosition;
		Vector3 mNextDirection;
		Vector3 mMoveDirection;

		// Use this for initialization
		void Start () {
			Debug.Log("Pacman - Start");
			mNextPosition = transform.position;
			mTimer = 0f;
		}
		
		// Update is called once per frame
		void Update () {
			float h = Input.GetAxisRaw("Horizontal");
			float v = Input.GetAxisRaw("Vertical");
			if (h > 0) mNextDirection = new Vector3(1, 0, 0);
			else if (h < 0) mNextDirection = new Vector3(-1, 0, 0);
			else if (v > 0) mNextDirection = new Vector3(0, 0, 1);
			else if (v < 0) mNextDirection = new Vector3(0, 0, -1);

			if (mTimer > 0) {
				mTimer -= Time.deltaTime;
				transform.position += mMoveDirection * MoveSpeed * CurrentStage.CellSize * Time.deltaTime;
			}
			else {
				transform.position = mNextPosition;
				int layerMask = 1 << 8;
				
				if (!Physics.Raycast(transform.position, mNextDirection, 1.5f, layerMask)) {
					mMoveDirection = mNextDirection;
				}
				
				bool isCollided = false;
				if (Physics.Raycast(transform.position, mMoveDirection, 1.5f, layerMask)) {
					isCollided = true;
				}

				if (!isCollided) { 
					mNextPosition = transform.position + (mMoveDirection * CurrentStage.CellSize);
					mTimer = CurrentStage.CellSize / (MoveSpeed * CurrentStage.CellSize);
				}
			}
		}

		void OnTriggerEnter(Collider other) {
			if (other.gameObject.tag == PacmanConstants.TAG_PACDOT) {
				Destroy(other.gameObject);
			}
		}
	}
}
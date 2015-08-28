using UnityEngine;
using System.Collections;

namespace Pacman {

	public class PacmanController : MonoBehaviour {

		public enum PacmanMoveDirection {
			North,
			South,
			East,
			West
		}

		public float MoveSpeed;
		public PacmanMoveDirection CurrentDirection;
		public Stage CurrentStage;

		PacmanMoveDirection NextDirection;
		float mTimer;
		Vector3 mNextPosition;
		Vector3 mMoveDirection;

		// Use this for initialization
		void Start () {
			Debug.Log("Pacman - Start");
			mNextPosition = transform.position;
			mTimer = 0.001f;
		}
		
		// Update is called once per frame
		void Update () {

			float h = Input.GetAxisRaw("Horizontal");
			float v = Input.GetAxisRaw("Vertical");
			if (h > 0) NextDirection = PacmanMoveDirection.East;
			else if (h < 0) NextDirection = PacmanMoveDirection.West;
			else if (v > 0) NextDirection = PacmanMoveDirection.North;
			else if (v < 0) NextDirection = PacmanMoveDirection.South;

			if (mTimer > 0) {
				mTimer -= Time.deltaTime;
				transform.position += mMoveDirection * MoveSpeed * CurrentStage.CellSize * Time.deltaTime;
			}
			
			if (mTimer <= 0) {
				transform.position = mNextPosition;
				Vector3 nextDir = Vector3.zero;
				if (NextDirection == PacmanMoveDirection.East) {
					nextDir = new Vector3(1, 0, 0);
				}
				else if (NextDirection == PacmanMoveDirection.West) {
					nextDir = new Vector3(-1, 0, 0);
				}
				else if (NextDirection == PacmanMoveDirection.North) {
					nextDir = new Vector3(0, 0, 1);
				}
				else if (NextDirection == PacmanMoveDirection.South) {
					nextDir = new Vector3(0, 0, -1);
				}

				RaycastHit hit;
				if (!Physics.Raycast( transform.position, nextDir, out hit, 1.5f, 1 << PacmanConstants.LAYER_WALL)) {
					mNextPosition = transform.position + (nextDir * CurrentStage.CellSize);
					mMoveDirection = nextDir;
					CurrentDirection = NextDirection;
					mTimer = 1 / MoveSpeed;
				}
				else {
					//Debug.Log(string.Format("Cannot go to next direction {0} hit {1}", nextDir, hit.collider.gameObject.name));
					if (!Physics.Raycast( transform.position, mMoveDirection, out hit, 1.5f, 1 << PacmanConstants.LAYER_WALL)) {
						mNextPosition = transform.position + (mMoveDirection * CurrentStage.CellSize);
						mTimer = 1 / MoveSpeed;
					}
					else {
						//Debug.Log(string.Format("Also cannot go to old direction {0} hit {1}. Then STOP!", mMoveDirection, hit.collider.gameObject.name));
					}
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
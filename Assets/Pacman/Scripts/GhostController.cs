using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace Pacman {

	public class GhostController : MonoBehaviour {

		public float MoveSpeed;
		public Stage CurrentStage;

		List<StageCell> mPath;
		int mPathIndex;

		Vector3 mNextPosition;
		Vector3 mMoveDirection;
		float mTimer;

		void Start() {
			mPath = new List<StageCell>();
			mNextPosition = transform.position;
			mTimer = 0.01f;
			mMoveDirection = Vector3.zero;
			mPathIndex = 0;
		}

		void Update() {
			if (mTimer > 0) {
				mTimer -= Time.deltaTime;
				transform.position += mMoveDirection * MoveSpeed * CurrentStage.CellSize * Time.deltaTime;
			}

			if (mTimer <= 0) {
				transform.position = mNextPosition;

				int nextPathIndex = mPathIndex + 1;
				if (nextPathIndex < mPath.Count) {
					mPathIndex = nextPathIndex;
					StageCell nextCell = mPath[nextPathIndex];
					mNextPosition = nextCell.Position;
					Vector3 diff = mNextPosition - transform.position;
					mMoveDirection = diff.normalized;
					mTimer = 1 / MoveSpeed;
				}
			}
		}

		void UpdatePathToPacman() {

		}

	}

}
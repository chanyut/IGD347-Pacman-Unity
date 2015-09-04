using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace Pacman {

	public class GhostController : MonoBehaviour {

		public enum GhostModeEnum {
			Scatter,
			Frightened,
			Chase
		}

		public float MoveSpeed;
		public Stage CurrentStage;

		public GhostModeEnum GhostMode;
		public float ScatterTime;
		public float ChasingTime;

		List<StageCell> mPath;
		int mPathIndex;

		Vector3 mNextPosition;
		Vector3 mMoveDirection;
		float mTimer;

		void Start() {

		}

		void Update() {

		}

		void UpdatePathToPacman() {

		}

		void OnDrawGizmos() {

		}
	}

}
using UnityEngine;
using UnityEngine.UI;

using System.Collections;


namespace Pacman {

	public class PacmanUIController : MonoBehaviour {

		public Text TxtScore;
		public Text TxtHighScore;

		void LateUpdate() {
			TxtScore.text = Game.Instance.Score.ToString();
			TxtHighScore.text = Game.Instance.HighScore.ToString();
		}
	}

}
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Pacman {

	public class Stage : MonoBehaviour {

		public int CellSize;
		public Vector3 BottomLeftCellPosition;
		public Vector3 TopRightCellPosition;

		StageCell[,] mCells;
		int mNumberOfColumns;
		int mNumberOfRows;

		void Awake() {
			int bottomLeftCell_x = Mathf.FloorToInt(BottomLeftCellPosition.x);
			int bottomLeftCell_z = Mathf.FloorToInt(BottomLeftCellPosition.z);
			
			int topRightCell_x = Mathf.FloorToInt(TopRightCellPosition.x);
			int topRightCell_z = Mathf.FloorToInt(TopRightCellPosition.z);

			mNumberOfColumns = ((topRightCell_x - bottomLeftCell_x) / CellSize) + 1;
			mNumberOfRows = ((topRightCell_z - bottomLeftCell_z) / CellSize) + 1;

			// Create all cells
			mCells = new StageCell[mNumberOfColumns, mNumberOfRows];
			for (int c=0; c<mNumberOfColumns; c++) {
				for (int r=0;r<mNumberOfRows; r++) {
					StageCell cell = new StageCell();
					cell.Position = new Vector3(bottomLeftCell_x + (c * CellSize), 0, bottomLeftCell_z + (r * CellSize));
					cell.North = null;
					cell.South = null;
					cell.West = null;
					cell.East = null;

					mCells[c, r] = cell;
				}
			}

			// Link cells
			for (int c=0; c<mNumberOfColumns; c++) {
				for (int r=0; r<mNumberOfRows; r++) {
					StageCell cell = mCells[c, r];

					// link north cell
					if (r + 1 < mNumberOfRows) {
						StageCell northCell = mCells[c, r + 1];
						bool canConnect = CheckCellConnection(cell, northCell);
						if (canConnect) {
							cell.North = northCell;
						}
					}

					// link south cell
					if (r - 1 >= 0) {
						StageCell southCell = mCells[c, r - 1];
						bool canConnect = CheckCellConnection(cell, southCell);
						if (canConnect) {
							cell.South = southCell;
						}
					}

					// link west cell
					if (c - 1 >= 0) {
						StageCell westCell = mCells[c - 1, r];
						bool canConnect = CheckCellConnection(cell, westCell);
						if (canConnect) {
							cell.West = westCell;
						}
					}

					// link east cell
					if (c + 1 < mNumberOfColumns) {
						StageCell eastCell = mCells[c + 1, r];
						bool canConnect = CheckCellConnection(cell, eastCell);
						if (canConnect) {
							cell.East = eastCell;
						}
					}
				}
			}
		}

		bool CheckCellConnection(StageCell fromCell, StageCell toCell) {
			Vector3 origin = fromCell.Position;
			Vector3 destination = toCell.Position;

			Vector3 diff = destination - origin;
			Vector3 direction = diff.normalized;
			float distance = diff.magnitude;

			RaycastHit hit;
			if (Physics.Raycast(origin, direction, out hit, distance, 1 << PacmanConstants.LAYER_WALL)) {
				return false;
			}
			else {
				return true;
			}
		}

		void OnDrawGizmos() {
			if (mCells == null) {
				return;
			}

			Gizmos.color = Color.green;
			for (int c=0; c<mNumberOfColumns; c++) {
				for (int r=0; r<mNumberOfRows; r++) {
					StageCell cell = mCells[c, r];
					if (cell.North != null) Gizmos.DrawLine(cell.Position, cell.North.Position);
					if (cell.South != null) Gizmos.DrawLine(cell.Position, cell.South.Position);
					if (cell.West != null) Gizmos.DrawLine(cell.Position, cell.West.Position);
					if (cell.East != null) Gizmos.DrawLine(cell.Position, cell.East.Position);
				}
			}
		}

		List<StageCell> FindFindShortestPath(StageCell fromCell, StageCell toCell) {
			return null;
		}

	}

}
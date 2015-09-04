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

			UpdateCellNeightbours();
		}

		void UpdateCellNeightbours() {
			for (int c=0; c<mNumberOfColumns; c++) {
				for (int r=0; r<mNumberOfRows; r++) {
					StageCell cell = mCells[c, r];
					
					// link north cell
					if (r + 1 < mNumberOfRows) {
						StageCell northCell = mCells[c, r + 1];
						if (CheckCellConnection(cell, northCell)) {
							cell.North = northCell;
						}
					}
					
					// link south cell
					if (r - 1 >= 0) {
						StageCell southCell = mCells[c, r - 1];
						if (CheckCellConnection(cell, southCell)) {
							cell.South = southCell;
						}
					}
					
					// link east cell
					if (c + 1 < mNumberOfRows) {
						StageCell eastCell = mCells[c + 1, r];
						if (CheckCellConnection(cell, eastCell)) {
							cell.East = eastCell;
						}
					}
					
					// link west cell
					if (c - 1 >= 0) {
						StageCell westCell = mCells[c - 1, r];
						if (CheckCellConnection(cell, westCell)) {
							cell.West = westCell;
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

			if (Physics.Raycast(origin, direction, distance, 1 << PacmanConstants.LAYER_WALL)) {
				return false;
			}
			else {
				return true;
			}
		}

		public int GetNumberOfColumns() {
			return mNumberOfColumns;
		}

		public int GetNumberOfRows() {
			return mNumberOfRows;
		}

		public StageCell GetStageCellAtPosition(Vector3 position) {
			return null;
		}

		public List<StageCell> FindShortestPath(StageCell fromCell, StageCell toCell) {
			return null;
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
	}

}
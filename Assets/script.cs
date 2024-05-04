using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class MNode { 
	public int x;
	public int y;
	public int player;

}


public class MovesAndScores {
	
	public int score;
	public MNode point;

	public MovesAndScores(int score, MNode point) {
		this.score = score;
		this.point = point;
	}

}



public class script : MonoBehaviour {
	public GameObject xprefab;
    public GameObject oprefab;
    MNode[,] grid = new MNode[3,3];
    public List<MovesAndScores> rootsChildrenScores;
    public GameObject ui;

    private const int PLAYER_X = 1;
    private const int PLAYER_O = 2;

	private int currentPlayer = PLAYER_X; // Biến để theo dõi người chơi hiện tại

	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (!isGameOver()) {
        // Người chơi 1 đặt nước đi
		if(currentPlayer == PLAYER_X){
			if (Input.GetKeyDown("q") && isValidMove(0, 0)) {
            MakeMove(0, 0, PLAYER_X);
            // MakeComputerMove();
			currentPlayer = PLAYER_O; // Chuyển lượt đến người chơi O
        }
        if (Input.GetKeyDown("w") && isValidMove(0, 1)) {
            MakeMove(0, 1, PLAYER_X);
            // MakeComputerMove();
			currentPlayer = PLAYER_O; // Chuyển lượt đến người chơi O
        }
        if (Input.GetKeyDown("e") && isValidMove(0, 2)) {
            MakeMove(0, 2, PLAYER_X);
            // MakeComputerMove();
			currentPlayer = PLAYER_O; // Chuyển lượt đến người chơi O
        }
        if (Input.GetKeyDown("a") && isValidMove(1, 0)) {
            MakeMove(1, 0, PLAYER_X);
            // MakeComputerMove();
			currentPlayer = PLAYER_O; // Chuyển lượt đến người chơi O
        }
        if (Input.GetKeyDown("s") && isValidMove(1, 1)) {
            MakeMove(1, 1, PLAYER_X);
            // MakeComputerMove();
			currentPlayer = PLAYER_O; // Chuyển lượt đến người chơi O
        }
        if (Input.GetKeyDown("d") && isValidMove(1, 2)) {
            MakeMove(1, 2, PLAYER_X);
            // MakeComputerMove();
			currentPlayer = PLAYER_O; // Chuyển lượt đến người chơi O
        }
        if (Input.GetKeyDown("z") && isValidMove(2, 0)) {
            MakeMove(2, 0, PLAYER_X);
            // MakeComputerMove();
			currentPlayer = PLAYER_O; // Chuyển lượt đến người chơi O
        }
        if (Input.GetKeyDown("x") && isValidMove(2, 1)) {
            MakeMove(2, 1, PLAYER_X);
            // MakeComputerMove();
			currentPlayer = PLAYER_O; // Chuyển lượt đến người chơi O
        }
        if (Input.GetKeyDown("c") && isValidMove(2, 2)) {
            MakeMove(2, 2, PLAYER_X);
            // MakeComputerMove();
			currentPlayer = PLAYER_O; // Chuyển lượt đến người chơi O
        }
		}
        

        // Người chơi 2 đặt nước đi
		else if(currentPlayer == PLAYER_O){
				if (Input.GetKeyDown("i") && isValidMove(0, 0)) {
            MakeMove(0, 0, PLAYER_O);
			currentPlayer = PLAYER_X; // Chuyển lượt đến người chơi X
        }
        if (Input.GetKeyDown("o") && isValidMove(0, 1)) {
            MakeMove(0, 1, PLAYER_O);
			currentPlayer = PLAYER_X; // Chuyển lượt đến người chơi X
        }
        if (Input.GetKeyDown("p") && isValidMove(0, 2)) {
            MakeMove(0, 2, PLAYER_O);
			currentPlayer = PLAYER_X; // Chuyển lượt đến người chơi X
        }
        if (Input.GetKeyDown("j") && isValidMove(1, 0)) {
            MakeMove(1, 0, PLAYER_O);
			currentPlayer = PLAYER_X; // Chuyển lượt đến người chơi X
        }
        if (Input.GetKeyDown("k") && isValidMove(1, 1)) {
            MakeMove(1, 1, PLAYER_O);
			currentPlayer = PLAYER_X; // Chuyển lượt đến người chơi X
        }
        if (Input.GetKeyDown("l") && isValidMove(1, 2)) {
            MakeMove(1, 2, PLAYER_O);
			currentPlayer = PLAYER_X; // Chuyển lượt đến người chơi X
        }
        if (Input.GetKeyDown("b") && isValidMove(2, 0)) {
            MakeMove(2, 0, PLAYER_O);
			currentPlayer = PLAYER_X; // Chuyển lượt đến người chơi X
        }
        if (Input.GetKeyDown("n") && isValidMove(2, 1)) {
            MakeMove(2, 1, PLAYER_O);
			currentPlayer = PLAYER_X; // Chuyển lượt đến người chơi X
        }
        if (Input.GetKeyDown("m") && isValidMove(2, 2)) {
            MakeMove(2, 2, PLAYER_O);
			currentPlayer = PLAYER_X; // Chuyển lượt đến người chơi X
        }
		}
        
    }
	}
	void MakeMove(int x, int y, int player) {
        Instantiate ((player == PLAYER_X) ? xprefab : oprefab, new Vector3 (x, 1, y), Quaternion.identity);
        MNode node = new MNode ();
        node.x = x;
        node.y = y;
        node.player = player;
        grid[x, y] = node;
    }

    void MakeComputerMove() {
        callMinimax (0, PLAYER_O);
        MNode best = returnBestMove ();
        if(isValidMove(best.x, best.y)){
            MakeMove(best.x, best.y, PLAYER_O);
        }
    }

	public bool hasOWon(){
		//check to see if the positions you're about to check actually exist in the grid
		if (grid [0, 0] != null && grid [1, 1] != null && grid [2, 2] != null) {
			//check to see if there is a diagonal win for the O player
			if (grid [0, 0].player == grid [1, 1].player && grid [0, 0].player == grid [2, 2].player && grid [0, 0].player == 1)
				return true;
		}
		if (grid [0, 2] != null && grid [1, 1] != null && grid [2, 0] != null) {
			//diagonal win
			if(grid [0, 2].player == grid [1, 1].player && grid [0, 2].player == grid [2, 0].player && grid [0, 2].player == 1)
				return true;
		}
		//Column Wins
		for (int i = 0; i < 3; ++i) {
			if(grid[i,0] != null && grid[i,1] != null && grid[i,2] != null) {
				if(grid[i,0].player == grid[i,1].player && grid[i,0].player == grid[i,2].player && grid[i,0].player == 1)
					return true;
			}
			if(grid[0,i] != null && grid[1,i] != null && grid[2,i] != null) {
				if(grid[0,i].player == grid[1,i].player && grid[0,i].player == grid[2,i].player && grid[0,i].player == 1)
					return true;
			}
		}
		return false; //there are no winning solutions on the board for O
	}

	public bool hasXWon() {
		if (grid [0, 0] != null && grid [1, 1] != null && grid [2, 2] != null) {
			if (grid [0, 0].player == grid [1, 1].player && grid [0, 0].player == grid [2, 2].player && grid [0, 0].player == 2)
				return true;
		}
		if (grid [0, 2] != null && grid [1, 1] != null && grid [2, 0] != null) {
			if(grid [0, 2].player == grid [1, 1].player && grid [0, 2].player == grid [2, 0].player && grid [0, 2].player == 2)
				return true;
		}
		
		for (int i = 0; i < 3; ++i) {
			if(grid[i,0] != null && grid[i,1] != null && grid[i,2] != null) {
				if(grid[i,0].player == grid[i,1].player && grid[i,0].player == grid[i,2].player && grid[i,0].player == 2)
					return true;
			}
			if(grid[0,i] != null && grid[1,i] != null && grid[2,i] != null) {
				if(grid[0,i].player == grid[1,i].player && grid[0,i].player == grid[2,i].player && grid[0,i].player == 2)
					return true;
			}
		}
		return false; //there are no winning solutions on the board for X
	}

	//game is over is someone has won, or board is full
	public bool isGameOver() {
		Text text = ui.GetComponent<Text>();
		
		if (getMoves ().Capacity == 0) {
			text.text = "Hoà!!!!";
			return true;
		}
		if (hasOWon ()) {
			text.text = "Layer X win";
			return true;
		}
		if (hasXWon ()) {
			text.text = "Layer O win";
			return true;
		}
		return false;
	}

	//returns a list of MNodes, each MNode being a position that is empty and available
	List<MNode> getMoves() {
		List<MNode> result = new List<MNode>();
		for(int row = 0; row < 3; row++) {
			for(int col = 0; col < 3; col++) {
				if(grid[row,col] == null) {
					MNode newNode = new MNode();
					newNode.x = row;
					newNode.y = col;
					result.Add(newNode); //if the space is empty, add it to the list of results
				}
			}
		}
		return result;
	}

	//gets the new best move and returns it as an MNode
	public MNode returnBestMove() {
		int MAX = -100000;
		int best = -1;

		//iterates through rootsChildrenScores to get the best move
		for (int i = 0; i < rootsChildrenScores.Count; i++) { 
			//also makes sure that the position in the grid is not occupied
			if (MAX < rootsChildrenScores[i].score && isValidMove(rootsChildrenScores[i].point.x, rootsChildrenScores[i].point.y)) {
				MAX = rootsChildrenScores[i].score;
				best = i;
			}
		}
		if(best > -1)
			return rootsChildrenScores[best].point;
		MNode blank = new MNode();
		blank.x = 0;
		blank.y = 0;
		return blank;
	}

	//returns true if the location is not currently occupied, returns false otherwise
	public bool isValidMove(int x, int y) {
		if (grid [x, y] == null)
			return true;
		return false;
	}

	//returns the minimum element of the list passed to it
	public int returnMin(List<int> list) {
		int min = 100000;
		int index = -1;
		for (int i = 0; i < list.Count; ++i) {
			if (list[i] < min) {
				min = list[i];
				index = i;
			}
		}
		return list[index];
	}

	//returns the maximum element of the list passed to it
	public int returnMax(List<int> list) {
		int max = -100000;
		int index = -1;
		for (int i = 0; i < list.Count; ++i) {
			if (list[i] > max) {
				max = list[i];
				index = i;
			}
		}
		return list[index];
	}

	//calls the minimax function with a given depth and turn
	public void callMinimax(int depth, int turn){
		rootsChildrenScores = new List<MovesAndScores>();
		minimax(depth, turn);
	}

	public int minimax(int depth, int turn) {
		
		if (hasXWon()) return +1; //+1 for a player win
		if (hasOWon()) return -1; //-1 for a computer win
		
		List<MNode> pointsAvailable = getMoves();
		if (pointsAvailable.Capacity == 0) return 0; 
		
		List<int> scores = new List<int>(); 
		
		for (int i = 0; i < pointsAvailable.Count; i++) {
			MNode point = pointsAvailable[i];

			//Select the highest from the minimax call on X's turn
			if (turn == 1) { 
				MNode x = new MNode();
				x.x = point.x;
				x.y = point.y;
				x.player = 2;
				grid[point.x,point.y] = x;

				int currentScore = minimax(depth + 1, 2);
				scores.Add(currentScore);
				
				if (depth == 0) {
					MovesAndScores m = new MovesAndScores(currentScore, point);
					m.point = point;
					m.score = currentScore;
					rootsChildrenScores.Add(m);
				}
				
			} 
			//Select the lowest from the minimax call on O's turn
			else if (turn == 2) {
				MNode o = new MNode();
				o.x = point.x;
				o.y = point.y;
				o.player = 1;
				grid[point.x,point.y] = o;
				int currentScore = minimax(depth+1,1);
				scores.Add(currentScore);
			}
			grid[point.x, point.y] = null; //reset the point
		}
		return turn == 1 ? returnMax(scores) : returnMin(scores);
	}
}
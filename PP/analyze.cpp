/*Priya Patel
 * analyze.cpp
 * Description: Analyzing and unioning the sets of same color character using disjointsets and board header files
 */

#include<iostream>
#include "disjointset.h"
using namespace std;

//Prototypes
void analyze_board(vector<vector<int> >board, Disjointset *ds);

int main()
{
	Disjointset *ds;
	vector < vector <int> > board;
	//dummy code to read in the value of board
	for(int i = 0; i < 5; i++)
	{
		for(int j = 0; j < 5; j++)
		{
			board[i][j] = 0;
		}
	}
	int row = (int)board.size();
	int col = (int)board[0].size();

	ds = new Disjointset (row*col);

	analyze_board(board, ds);
	return 0;
}
void analyze_board(vector<vector<int> > board, Disjointset *ds)
{
	int rows, cols;
	int index, left_index, right_index, top_index, bottom_index; 
	int current_leader, other_leader;
	
	//initialize rows and cols
	rows = board.size();
	cols = board[0].size();

	vector<int> v(rows * cols, 1);				//helper to store sizes of the set at indices
	
	//Iterate through the board
	for(int i = 0; i < rows; i++)
	{
		for(int j = 0; j < cols; j++)
		{
			//calculating the index value of the element in the links and v of vector of int type for disjoint sets
			index = i * (cols) + j;
			left_index = index - 1;			
			right_index = index + 1;
			top_index = index + cols;
			bottom_index = index - cols;

			//check if the element at index is not empty
			if(board[i][j] != -1)
			{
				//check if the bottom index is a valid index
				if(i+1 < rows) 
				{
					//if element at index is same as element at left index 
					if(board[i][j] == board[i+1][j])
					{
						//find the leaders(set id) of both the sets.
						current_leader = ds->Find(index);
						other_leader = ds->Find(bottom_index); 

						//if they are disjoint, union them and sum-up the size of new leader
						if(current_leader != other_leader)
						{
							ds->Union(current_leader, other_leader);
							v[ds->Find(current_leader)] = v[current_leader] + v[other_leader];
						}
					}
				}
				//check if the top index is a valid index
				if(i-1 >= 0) 
				{
					//if element at index is same as element at left index 
					if(board[i][j] == board[i-1][j])
					{
						//find the leaders(set id) of both the sets.
						current_leader = ds->Find(index);
						other_leader = ds->Find(top_index); 

						//if they are disjoint, union them and sum-up the size of new leader
						if(current_leader != other_leader)
						{
							ds->Union(current_leader, other_leader);
							v[ds->Find(current_leader)] = v[current_leader] + v[other_leader];
						}
					}
				}
				//check if the left index is a valid index
				if(j-1 >= 0) 
				{
					//if element at index is same as element at left index 
					if(board[i][j] == board[i][j-1])
					{
						//find the leaders(set id) of both the sets.
						current_leader = ds->Find(index);
						other_leader = ds->Find(left_index); 

						//if they are disjoint, union them and sum-up the size of new leader
						if(current_leader != other_leader)
						{
							ds->Union(current_leader, other_leader);
							v[ds->Find(current_leader)] = v[current_leader] + v[other_leader];
						}
					}
				}	
				//check if the right index is a valid index
				if(j+1 < cols) 
				{
					//if element at index is same as element at left index 
 					if(board[i][j] == board[i][j+1])
					{
						//find the leaders(set id) of both the sets.
						current_leader = ds->Find(index);
						other_leader = ds->Find(right_index); 

						//if they are disjoint, union them and sum-up the size of new leader
						if(current_leader != other_leader)
						{
							ds->Union(current_leader, other_leader);
							v[ds->Find(current_leader)] = v[current_leader] + v[other_leader];
						}
					}
				}
			}
		}
	}
}

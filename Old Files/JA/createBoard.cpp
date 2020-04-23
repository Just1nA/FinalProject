//g++ -std=c++11 createBoard.cpp -o createBoard  
#include <iostream>
#include <stdlib.h>
#include <time.h>
#include <vector>

using namespace std;

int main()
{
    int row, col;
    row = 18; col = 18;

    vector <char> colors;
    vector < vector <char> > board;

    //Create empty board
    board.resize(row, vector<char> (col, '0'));
    colors.resize(6);

    colors[0] = 'r'; colors[1] = 'g'; colors[2] = 'B';
    colors[3] = 'y'; colors[4] = 'p'; colors[5] = 'b';

	for (size_t i = 0; i < board.size(); ++i)
	{
		for (size_t j = 0; j < board[i].size(); ++j)
		{	
            //Start off with random colors
            if (i <= 6)
            {
			    int random = rand() % colors.size();
			    board[i][j] = colors[random];
            }
		}
	}   

	for (size_t i = 0; i < board.size(); ++i)
	{
		for (size_t j = 0; j < board[i].size(); ++j)
		{	
            cout << board[i][j] << " ";
		}
        cout << endl;
	}

    return 0;
}
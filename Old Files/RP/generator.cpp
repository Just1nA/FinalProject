/* Ria Patel 
 * Bubble Color Generator
 * 
 * Description: 
 * This is a simple program just randomly generates a color for 
 * the bubbles. Will be updated to where the board is taken 
 * into account as well as points and graphics (hopefully). 
 * 
 */ 

#include <iostream> 
#include <vector> 
#include <random> 
#include <time.h> 

using namespace std; 

int main(int argc, char *argv[])
{
    char color; 
    string bubble_color, input; 
    char letters[] = "rgbypB"; // Array of colors

    /* Initialize seed for random generation (based on time) */
    srand(time(0));
    
    cout << "Welcome to Bubble Color Generator 4000!\nPlease press any letter to generate a color for the bubble.\nPress 'q' or 'Q' to quit\n";
    while (cin >> input)
    {
        /* To quit the program... */ 
        if (input[0] == 'q' || input[0] == 'Q') break; 

        color = letters[rand() % 6]; // Generates the random color

        switch(color)
        {
            case 'r':
                bubble_color = "RED";
                break;
            case 'g':
                bubble_color = "GREEN";
                break;
            case 'b':
                bubble_color = "BLUE";
                break;
            case 'y': 
                bubble_color = "YELLOW";
                break;
            case 'p':
                bubble_color = "PURPLE"; 
                break; 
            case 'B': 
                bubble_color = "DARK BLUE"; 
                break; 
        }

        cout << "Bubble color: " << bubble_color << endl;
    }

    return 0; 
}
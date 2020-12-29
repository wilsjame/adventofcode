#include <iostream>
#include <string>
#include <vector>
using namespace std;

#define MAXN 10 
#define print(x) cerr << #x << " is " << x << endl;

typedef struct Board Board;
struct Board {
    int ID;
    char b[MAXN][MAXN];

};

// Rotate 90 degree clockwise: [r, c] -> [c, n - 1 - r]
Board rotate(Board b) {
    Board nb;
    
    nb.ID = b.ID;
    for (int r = 0; r < MAXN; r++) 
        for (int c = 0; c < MAXN; c++) 
            nb.b[c][MAXN - 1 - r] = b.b[r][c];
    return nb;
}

// Reflect board horizontally: [r, c] -> [r, n - 1 - c]
Board reflect(Board b) {
    Board nb;

    nb.ID = b.ID;
    for (int r = 0; r < MAXN; r++) 
        for (int c = 0; c < MAXN; c++) 
            nb.b[r][MAXN - 1 - c] = b.b[r][c];
    return nb;
}

Board rdboard(int n) {
    Board b;

    b.ID = n;
    for (int r = 0; r < MAXN; r++) 
        for (int c = 0; c < MAXN; c++) 
            cin >> b.b[r][c];
    return b;
}

// debugging
void printb(Board b) {
    print(b.ID);
    for (int r = 0; r < MAXN; r++) {
        for (int c = 0; c < MAXN; c++) {
            cerr << b.b[r][c] << " ";
        }
        cerr << endl;
    }
    return;
}

int main() {
    freopen("input.txt", "r", stdin);
    string S;
    vector<Board> A;
    while (getline(cin, S)) {
        if (S.find("Tile") != string::npos) {
            S.erase(0, S.find(' ') + 1);
            int ID = stoi(S);
            Board b = rdboard(ID);
            A.push_back(b);
        }
    }

    // debug
    for (Board b : A) {
        printb(b);
        cerr << "rotated!" << endl;
        printb(rotate(b));
        cerr << "and reflected!" << endl;
        printb(reflect(rotate(b)));
    }

    return 0;
}

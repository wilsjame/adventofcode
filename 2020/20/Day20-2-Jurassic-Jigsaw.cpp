//TODO
// combine board utility functions
// create a function to try all rotations + flip
// clean up variable names 
// regex seamonster instead of hardcoding search window
// print jigsaw with seamonsters showing!
/*
   Steps ->
   Find corners
   Pick one as the top left corner
   Rotate and flip until it is 
   Fill top row from top left to top right
   Fill columns down from the completed top row
   Trim borders and copy tiles into vector<vector<char>> 
   Rotate and flip jigsaw until seamonsters found
*/
#include <iostream>
#include <string>
#include <vector>
#include <cmath>
using namespace std;

#define MAXN 10 
#define print(x) cerr << #x << " is " << x << endl;

typedef struct Board Board;
struct Board {
    int ID;
    char b[MAXN][MAXN];
};

// seamonster window 3 X 20, 15 body pieces
// 0 1 2 3 4 5 6 7 8 9 10                19
// . . . . . . . . . . . . . . . . . . O .
// O . . . . O O . . . . O O . . . . O O O
// . O . . O . . O . . O . . O . . O . . .
int seamonster(int r, int c, vector<vector<char>> A) {
    int O = 0;
    vector<vector<char>> w;

    for (int i = r; i < r + 3; i++) {
        vector<char> row;
        for (int j = c; j < c + 20; j++) 
            row.push_back(A[i][j]);
        w.push_back(row);
    }

    O += w[0][18] == '#';

    O += w[1][0] == '#';
    O += w[1][5] == '#';
    O += w[1][6] == '#';
    O += w[1][11] == '#';
    O += w[1][12] == '#';
    O += w[1][17] == '#';
    O += w[1][18] == '#';
    O += w[1][19] == '#';

    O += w[2][1] == '#';
    O += w[2][4] == '#';
    O += w[2][7] == '#';
    O += w[2][10] == '#';
    O += w[2][13] == '#';
    O += w[2][16] == '#';

    return O == 15;
}

//// Use on solved jigsaw
// rotate 90 degree clockwise: [r, c] -> [c, n - 1 - r]
vector<vector<char>> rotateV(vector<vector<char>> b) {
    int N = b.size();
    vector<vector<char>> nb (N, vector<char> (N));

    for (int r = 0; r < N; r++) 
        for (int c = 0; c < N; c++) 
            nb[c][N - 1 - r] = b[r][c];
    return nb;
}
// reflect horizontally: [r, c] -> [r, n - 1 - c]
vector<vector<char>> reflectV(vector<vector<char>> b) {
    int N = b.size();
    vector<vector<char>> nb (N, vector<char> (N));

    for (int r = 0; r < N; r++) 
        for (int c = 0; c < N; c++) 
            nb[r][N - 1 - c] = b[r][c];
    return nb;
}

void printv(vector<vector<char>> b) {
    for (auto row : b) {
        for (auto a : row) 
            cerr << a;
        cerr << endl;
    }
}
////

//// Use on individual tiles
// rotate 90 degree clockwise: [r, c] -> [c, n - 1 - r]
Board rotate(Board b) {
    Board nb;
    
    nb.ID = b.ID;
    for (int r = 0; r < MAXN; r++) 
        for (int c = 0; c < MAXN; c++) 
            nb.b[c][MAXN - 1 - r] = b.b[r][c];
    return nb;
}
// reflect board horizontally: [r, c] -> [r, n - 1 - c]
Board reflect(Board b) {
    Board nb;

    nb.ID = b.ID;
    for (int r = 0; r < MAXN; r++) 
        for (int c = 0; c < MAXN; c++) 
            nb.b[r][MAXN - 1 - c] = b.b[r][c];
    return nb;
}
// return true if and only if boards match a side
bool matchN(Board b, Board bb) {
    vector<char> bN, bbS;
    for (int k = 0; k < MAXN; k++) 
        bN.push_back(b.b[0][k]);
    for (int k = 0; k < MAXN; k++) 
        bbS.push_back(bb.b[MAXN - 1][k]);
    return bN == bbS;
}
bool matchE(Board b, Board bb) {
    vector<char> bE, bbW;
    for (int k = 0; k < MAXN; k++) 
        bE.push_back(b.b[k][MAXN - 1]);
    for (int k = 0; k < MAXN; k++) 
        bbW.push_back(bb.b[k][0]);
    return bE == bbW;
}
bool matchS(Board b, Board bb) {
    vector<char> bS, bbN;
    for (int k = 0; k < MAXN; k++) 
        bS.push_back(b.b[MAXN - 1][k]);
    for (int k = 0; k < MAXN; k++) 
        bbN.push_back(bb.b[0][k]);
    return bS == bbN;
}
bool matchW(Board b, Board bb) {
    vector<char> bW, bbE;
    for (int k = 0; k < MAXN; k++) 
        bW.push_back(b.b[k][0]);
    for (int k = 0; k < MAXN; k++) 
        bbE.push_back(bb.b[k][MAXN - 1]);
    return bW == bbE;
}
// return non 'X' if and only if boards match any side
char matchA(Board b, Board bb) {
    char res = 'X';
    if (matchN(b, bb)) res = 'N'; 
    else if (matchE(b, bb)) res = 'E';
    else if (matchS(b, bb)) res = 'S';
    else if (matchW(b, bb)) res = 'W';
    return res;
}

Board rdboard(int n) {
    Board b;

    b.ID = n;
    for (int r = 0; r < MAXN; r++) 
        for (int c = 0; c < MAXN; c++) 
            cin >> b.b[r][c];
    return b;
}

void printb(Board b) {
    print(b.ID);
    for (int r = 0; r < MAXN; r++) {
        for (int c = 0; c < MAXN; c++) 
            cerr << b.b[r][c] << " ";
        cerr << endl;
    }
    return;
}
////

int main() {
    freopen("input1.txt", "r", stdin);
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

    int N = sqrt(A.size());
    vector<Board> corner;
    vector<vector<Board>> AA (N, vector<Board> (N));

    // find corners (really only need one to begin solving part 2)
    for (int t = 0; t < N * N; t++) {
        int mc = 0;
        Board b = A[t];
        for (int tt = 0; tt < N * N; tt++) {
            if (tt == t) continue;
            Board bb = A[tt];
            for (int i = 0; i < 4; i++) {
                if (matchA(b, bb) != 'X') mc++;
                bb = rotate(bb);
            }
            bb = reflect(bb);
            for (int i = 0; i < 4; i++) {
                if (matchA(b, bb) != 'X') mc++;
                bb = rotate(bb);
            }
        }
        if (mc == 2) corner.push_back(b);
    }

    // any corner can be top left so pick one
    Board topleft = corner[0];
    int k = 0;
    while (1) {
        bool S_m = false, E_m = false;;
        for (int t = 0; t < N * N; t++) {
            if (topleft.ID == A[t].ID) continue;
            Board bb = A[t];
            for (int i = 0; i < 4; i++) {
                char d = matchA(topleft, bb);
                if (d == 'S')
                    S_m = true;
                else if (d == 'E')
                    E_m = true;
                bb = rotate(bb);
            }
            bb = reflect(bb);
            for (int i = 0; i < 4; i++) {
                char d = matchA(topleft, bb);
                if (d == 'E') 
                    E_m = true;
                else if (d == 'S') 
                    S_m = true;
                bb = rotate(bb);
            }
        }
        if (S_m && E_m) 
            break;

        if (++k % 5) topleft = reflect(topleft);
        topleft = rotate(topleft);
    }

    // fill first row
    AA[0][0] = topleft;
    for (int col = 1; col < N; col++) {
        Board to_left = AA[0][col - 1];
        for (int t = 0; t < N * N; t++) {
            Board b = A[t];
            if (to_left.ID == b.ID) continue;
            for (int i = 0; i < 4; i++) {
                char d = matchA(to_left, b);
                if (d == 'E')
                    AA[0][col] = b;
                b = rotate(b);
            }
            b = reflect(b);
            for (int i = 0; i < 4; i++) {
                char d = matchA(to_left, b);
                if (d == 'E')
                    AA[0][col] = b;
                b = rotate(b);
            }
        }
    }

    // fill columns
    for (int col = 0; col < N; col++) {
        for (int row = 1; row < N; row++) {
            Board above = AA[row - 1][col];
            for (int t = 0; t < N * N; t++) {
                Board b = A[t];
                if (above.ID == b.ID) continue;
                for (int i = 0; i < 4; i++) {
                    char d = matchA(above, b);
                    if (d == 'S') 
                       AA[row][col] = b; 
                    b = rotate(b);
                }
                b = reflect(b);
                for (int i = 0; i < 4; i++) {
                    char d = matchA(above, b);
                    if (d == 'S') 
                       AA[row][col] = b; 
                    b = rotate(b);
                }
            }
        }
    }

    // remove borders from each tile 
    // combine into one large piece i.e. solved jigsaw
    vector<vector<char>> sol;
    for (int row = 0; row < N; row++) {
        for (int rb = 1; rb < MAXN - 1; rb++) {
            vector<char> sr;
            for (int col = 0; col < N; col++) {
                Board b = AA[row][col];
                for (int cb = 1; cb < MAXN - 1; cb++) {
                    sr.push_back(b.b[rb][cb]);
                }
            }
            sol.push_back(sr);
        }
    }

    // rotate and reflect jigsaw until seamonster found
    int monster_cnt = 0;
    while (!monster_cnt) {
        for (int row = 0; row + 3 < sol.size(); row++) 
            for (int col = 0; col + 20 < sol.size(); col++) 
                if (seamonster(row, col, sol)) 
                    monster_cnt++;

        if (++k % 5) sol = reflectV(sol);
        sol = rotateV(sol);
    }

    int roughness = 0;
    for (auto row : sol) 
        for (auto a : row) 
            roughness += a == '#';
    print(roughness - monster_cnt * 15);

    return 0;
}

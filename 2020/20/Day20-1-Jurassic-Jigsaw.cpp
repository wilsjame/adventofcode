#include <iostream>
#include <string>
#include <vector>
#include <cmath>
using namespace std;

#define MAXN 10 
#define ll long long
#define print(x) cerr << #x << " is " << x << endl;

typedef struct Board Board;
struct Board {
    int ID;
    char b[MAXN][MAXN];
};

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
    int N = sqrt(A.size());

    ll ans = 1;
    for (int t = 0; t < N * N; t++) {
        int mc = 0;
        Board b = A[t];
        for (int tt = 0; tt < N * N; tt++) {
            if (tt == t) continue;
            Board bb = A[tt];
            for (int i = 0; i < 4; i++) {
                mc += matchA(b, bb) != 'X';
                bb = rotate(bb);
            }
            bb = reflect(bb);
            for (int i = 0; i < 4; i++) {
                mc += matchA(b, bb) != 'X';
                bb = rotate(bb);
            }
        }
        if (mc == 2) ans *= b.ID;
    }
    print(ans);

    return 0;
}
